using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{
    #region Variables

    [SerializeField] private int gridWidth = 9;
    [SerializeField] private int gridHeight = 9;
    [SerializeField] private int mineCount = 10;
    [SerializeField] private TileBase tilePrefab;
    
    public TileBase selectedTile;
    public static Action OnFail;
    
    private List<Vector2Int> mineRestrictedTiles = new List<Vector2Int>();
    private List<Vector2Int> minePositions = new List<Vector2Int>();
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

    private void OnEnable()
    {
        InputManager.OnFirstClicked += SetSelectedTile;
        InputManager.OnClickedTile += ExplodeTiles;
    }

    private void OnDisable()
    {
        InputManager.OnFirstClicked -= SetSelectedTile;        
        InputManager.OnClickedTile -= ExplodeTiles;
    }

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
                newTile.SetTileType("Empty", this);
                newTile.transform.position = new Vector3(x * 0.4f, y * 0.4f, 0);
            }
        }
    }

    private void SetSelectedTile(TileBase tile)
    {
        selectedTile = tile;
        CheckMineRestrictedTiles();   
    }
    
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
                minePositions.Add(new Vector2Int(xPosition, yPosition));
                tiles[xPosition, yPosition].SetTileType("Mine", this);
                i++;
            }
        }
        
        GenerateNumbers();
    }

    private void GenerateNumbers()
    {
        for (int i = 0; i < minePositions.Count; i++)
        {
            for (int j = 0; j < directions.Count; j++)
            {
                Vector2Int targetTilePosition = new Vector2Int(minePositions[i].x, minePositions[i].y) + directions[j];
                try
                {
                    if (tiles[targetTilePosition.x, targetTilePosition.y] != null)
                    {
                        if (tiles[targetTilePosition.x, targetTilePosition.y].Type != TileBase.TileType.Mine)
                        {
                            tiles[targetTilePosition.x, targetTilePosition.y].Type = TileBase.TileType.Number;
                            var mineCount = CheckAllDirectionsForMines(targetTilePosition);
                            tiles[targetTilePosition.x, targetTilePosition.y].SetTileType(mineCount.ToString(), this);
                        }
                    }
                }
                catch (Exception e)
                {
                    continue;
                }
            }
        }
        ExplodeTiles(selectedTile);
    }

    private int CheckAllDirectionsForMines(Vector2Int position)
    {
        int mineCount = 0;
        
        for (int i = 0; i < directions.Count; i++)
        {
            Vector2Int targetTilePosition = position + directions[i];
            try
            {
                if (tiles[targetTilePosition.x, targetTilePosition.y] != null)
                {
                    if (tiles[targetTilePosition.x, targetTilePosition.y].Type == TileBase.TileType.Mine)
                    {
                        mineCount++;
                    }
                }
            }
            catch (Exception e)
            {
                continue;
            }
        }
        return mineCount;
    }

    private void ExplodeTiles(TileBase tile)
    {
        if (tile.Type == TileBase.TileType.Mine)
        {
            OnFail?.Invoke();
            return;
        }
        
        if (tile.Opened)
            return;

        tile.OnTileClicked();

        if (tile.Type == TileBase.TileType.Number)
            return;
            
        
        for (int i = 0; i < directions.Count; i++)
        {
            var tilePosition = tile.GetTilePosition();
            var newTilePosition = tilePosition + directions[i];
            try
            {
                if (tiles[newTilePosition.x, newTilePosition.y] != null)
                {
                    ExplodeTiles(tiles[newTilePosition.x, newTilePosition.y]);
                }
            }
            catch (Exception e)
            {
                continue;
            }
        }
    }

    #endregion
}
