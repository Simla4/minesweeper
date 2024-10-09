using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBase : MonoBehaviour
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

    #endregion
}
