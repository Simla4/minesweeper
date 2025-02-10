using Unity.Netcode; // NGO Kütüphanesini ekleyelim
using UnityEngine;
using System;

public class TileBase : NetworkBehaviour
{
    #region Variables

    [SerializeField] private SpriteRenderer tileImg;
    [SerializeField] private TileData tileData;

    private Sprite defaultSprite;
    private Sprite openedSprite;
    private Vector2Int tilePosition;
    private NetworkVariable<bool> flagged = new NetworkVariable<bool>(false);
    private NetworkVariable<bool> opened = new NetworkVariable<bool>(false);
    
    public bool Opened { get => opened.Value; set => opened.Value = value; }
    public bool Flagged { get => flagged.Value; set => flagged.Value = value; }

    private TileType type;
    public TileType Type { get => type; set => type = value; }

    #endregion

    private void OnEnable()
    {
        GridManager.OnFail += OnFail;
    }

    private void OnDisable()
    {
        GridManager.OnFail -= OnFail;
    }

    #region Other Methods

    public void SetTilePosition(Vector2Int tilePosition, GridManager caller)
    {
        if (caller != null)
        {
            this.tilePosition = tilePosition;
        }
    }

    public Vector2Int GetTilePosition()
    {
        return tilePosition;
    }

    public void OnTileClicked()
    {
        if (IsServer) // Sadece server'da patlamayı tetikle
        {
            Debug.Log(tilePosition + ". tile clicked");
            ExplodeTileClientRpc();
        }
    }

    public void OnTileHeld()
    {
        if (IsOwner) // Sadece kendi tıklayan oyuncu flag ekleyebilsin
        {
            Debug.Log(tilePosition + ". tile holded");

            FlagTileServerRpc(); // İstemci isteği server'a gönderir
        }
    }

    [ServerRpc]
    private void FlagTileServerRpc()
    {
        if (!flagged.Value)
        {
            var sprite = GetTileSpriteDictionary("Flag");

            if (sprite != null)
            {
                tileImg.sprite = sprite;
                flagged.Value = true;
            }
        }
        else
        {
            var sprite = GetTileSpriteDictionary("Unknown");

            if (sprite != null)
            {
                tileImg.sprite = sprite;
                flagged.Value = false;
            }
        }
    }

    [ClientRpc]
    private void ExplodeTileClientRpc()
    {
        if (openedSprite != null)
        {
            tileImg.sprite = openedSprite;
            opened.Value = true;
        }
    }

    public void OnFail()
    {
        if (type == TileType.Mine)
        {
            var newSprite = GetTileSpriteDictionary("Exploded");
            if (newSprite != null)
            {
                tileImg.sprite = newSprite;
            }
        }
        else
        {
            tileImg.sprite = openedSprite;
        }
    }

    private Sprite GetTileSpriteDictionary(string tileType)
    {
        var tileSpriteDictionary = tileData.GetTileSpriteDictionary();
            
        if (tileSpriteDictionary.TryGetValue(tileType, out Sprite sprite))
        {
            return sprite;
        }

        return null;
    }

    public void SetTileType(string tileType, GridManager caller)
    {
        if (caller != null)
        {
            var newSprite = GetTileSpriteDictionary(tileType);
            if (newSprite != null)
            {
                openedSprite = newSprite;
            }
        }
    }

    #endregion

    public enum TileType
    {
        Empty,
        Mine,
        Number
    }
}
