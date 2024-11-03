using UnityEngine;
using UnityEngine.EventSystems;

public class TileBase : MonoBehaviour, IPointerClickHandler
{
    #region Variables

    [SerializeField] private GameObject tileBorder;

    private Vector2Int tilePosition;
    private TileType type;
    private bool flagged = false;
    private bool opened = false;
    
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
        ShowTileBorder();
    }


    public void OnTileHeld()
    {
        Debug.Log(tilePosition + ". tile holded");
    }

    private void ShowTileBorder()
    {
        tileBorder.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnTileClicked();
    }

    #endregion

    public enum  TileType
    {
        Empty,
        Mine,
        Numbers
    }
}
