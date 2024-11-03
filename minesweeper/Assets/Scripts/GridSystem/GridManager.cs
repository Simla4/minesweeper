using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    #region Variables

    [SerializeField] private int gridWidth = 9;
    [SerializeField] private int gridHeight = 9;
    [SerializeField] private int bombCount;
    [SerializeField] TileBase tilePrefab;

    private TileBase[,] tiles;

    #endregion

    #region Callbacks

    private void Start()
    {
        CreateGrid();
    }

    #endregion


    #region OtherMethods


    private void CreateGrid()
    {
        tiles = new TileBase[gridWidth, gridHeight];

        for(int x = 0; x < gridWidth; x++)
        {
            for(int y = 0; y < gridHeight; y ++)
            {
                TileBase newTile = Instantiate(tilePrefab);

                tiles[x, y] = newTile;
                newTile.SetTilePosition(new Vector2Int(x, y), this);

                newTile.transform.position = new Vector3(x * 0.4f, y * 0.4f, 0);
            }
        }
    }

    #endregion
}
