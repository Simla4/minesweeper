using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBase : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject tileBorder;

    private string tileType;

    #endregion

    #region Other Methods

    public void OnTileClicked()
    {
        Debug.Log(gameObject.transform.name + " clicked");
        ShowTileBorder();
    }


    public void OnTileHeld()
    {
        Debug.Log(gameObject.transform.name + " holded");
    }

    private void ShowTileBorder()
    {
        tileBorder.SetActive(true);
    }

    #endregion
}
