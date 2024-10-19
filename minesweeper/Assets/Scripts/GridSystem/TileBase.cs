using UnityEngine;
using UnityEngine.EventSystems;

public class TileBase : MonoBehaviour, IPointerClickHandler
{
    #region Variables

    [SerializeField] private GameObject tileBorder;

    private Vector2Int tilePosition;

    private string tileType;

    #endregion

    #region Other Methods

    public void SetTilePosition(Vector2Int tilePosition)
    {
        this.tilePosition = tilePosition;
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
        Debug.Log("Simla");
    }

    #endregion
}
