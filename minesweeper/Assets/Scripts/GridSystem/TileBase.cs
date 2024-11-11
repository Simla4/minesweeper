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
            if (tileSpriteDictionary.TryGetValue("Flag", out Sprite sprite))
            {
                tileImg.sprite = sprite;
                flagged = true;
            }
        }
        else
        {
            if (tileSpriteDictionary.TryGetValue("Unknown", out Sprite sprite))
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

    // public void OnPointerClick(PointerEventData eventData)
    // {
    //     OnTileClicked();
    // }

    public void SetTileType(String tileType, GridManager caller)
    {
        if (caller != null)
        {
            var tileSpriteDictionary = tileData.GetTileSpriteDictionary();
            
            if (tileSpriteDictionary.TryGetValue(tileType, out Sprite sprite))
            {
                openedSprite = sprite;
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
