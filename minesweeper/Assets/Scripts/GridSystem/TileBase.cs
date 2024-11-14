using System;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;


public class TileBase : MonoBehaviour
{
    #region Variables

    [SerializeField] private SpriteRenderer tileImg;
    [SerializeField] private TileData tileData;
    
    private Sprite defaultSprite;
    private Sprite openedSprite;
    private Vector2Int tilePosition;
    private TileType type;
    public bool flagged = false;
    public bool opened = false;
    
    public bool Opened { get => opened; set => opened = value; }
    public bool Flagged { get => flagged;  set => flagged = value; }
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
        Debug.Log(tilePosition + ". tile clicked");
        ExplodeTile();
    }


    public void OnTileHeld()
    {
        Debug.Log(tilePosition + ". tile holded");
        
        var tileSpriteDictionary = tileData.GetTileSpriteDictionary();

        if (!flagged)
        {
            var sprite = GetTileSpriteDictionary("Flag");
            
            if (sprite != null)
            {
                tileImg.sprite = sprite;
                flagged = true;
            }
        }
        else
        {
            var sprite = GetTileSpriteDictionary("Unknown");

            if (sprite != null)
            {
                tileImg.sprite = sprite;
                flagged = false;
            }
        }
    }

    private void ExplodeTile()
    {
        if (openedSprite != null)
        {
            tileImg.sprite = openedSprite;
            opened = true;
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

    private Sprite GetTileSpriteDictionary(String tileType)
    {
        var tileSpriteDictionary = tileData.GetTileSpriteDictionary();
            
        if (tileSpriteDictionary.TryGetValue(tileType, out Sprite sprite))
        {
            return sprite;
        }

        return null;
    }

    public void SetTileType(String tileType, GridManager caller)
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

    public enum  TileType
    {
        Empty,
        Mine,
        Number
    }
}
