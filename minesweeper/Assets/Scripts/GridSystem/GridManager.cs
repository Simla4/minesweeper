using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{
    #region Variables

    [SerializeField] private int gridWidth = 9;
    [SerializeField] private int gridHeight = 9;
    [SerializeField] private int mineCount = 10;
    [SerializeField] private TileBase tilePrefab;
    
    public TileBase selectedTile;
    public List<Vector2Int> mineRestrictedTiles;
    private TileBase[,] tiles;

    private List<Vector2Int> directions = new List<Vector2Int>
    {
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(1, -1),
        new Vector2Int(1, 1),
        new Vector2Int(-1, 1),
        new Vector2Int(-1, -1)
    };

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
                newTile.Type = TileBase.TileType.Empty;

                newTile.transform.position = new Vector3(x * 0.4f, y * 0.4f, 0);
            }
        }
    }
    
    [Button]
    private void CheckMineRestrictedTiles()
    {
        for (int i = 0; i < directions.Count; i++)
        {
            Vector2Int selectedTilePosition = selectedTile.GetTilePosition();
            Vector2Int targetTilePosition = selectedTilePosition + directions[i];

            try
            {
                if (tiles[targetTilePosition.x, targetTilePosition.y] != null)
                {
                    mineRestrictedTiles.Add(targetTilePosition);
                }
            }
            catch (Exception e)
            {
                continue;
            }
        }
        
        GenerateMines();
    }

    private void GenerateMines()
    {
        int i = 0;
        while (i < mineCount)
        {
            int xPosition = Random.Range(0, gridWidth);
            int yPosition = Random.Range(0, gridHeight);
            if (tiles[xPosition, yPosition].Type == TileBase.TileType.Empty &&
                !mineRestrictedTiles.Contains(new Vector2Int(xPosition, yPosition)))
            {
                tiles[xPosition, yPosition].Type = TileBase.TileType.Mine;
                i++;
                Debug.Log("X: " + xPosition + " Y: " + yPosition + " position include mine.");
            }
        }
        
        GenerateNumbers();
    }

    private void GenerateNumbers()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                CheckAllDirections(new Vector2Int(x, y));
                tiles[x, y].Type = TileBase.TileType.Number;
                Debug.Log("X: " + x + " Y: " + y + " position include number.");
            }
        }
    }

    private int CheckAllDirections(Vector2Int position)
    {
        int mineCount = 0;
        
        for (int i = 0; i < directions.Count; i++)
        {
            Vector2Int targetTilePosition = position + directions[i];
            mineCount++;
        }
        Debug.Log("mine count: " + mineCount);
        return mineCount;
    }

    #endregion
}
