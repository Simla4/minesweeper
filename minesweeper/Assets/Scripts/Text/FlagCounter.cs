using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FlagCounter : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    
    private TextMeshProUGUI mineCountTxt;
    private int mineCount;

    private void Start()
    {
        mineCountTxt = GetComponent<TextMeshProUGUI>();
        SetMineCount();
    }

    private void OnEnable()
    {
        TileBase.OnButtonHeld += ChangeMineCount;
    }

    private void OnDisable()
    {
        TileBase.OnButtonHeld -= ChangeMineCount;
    }


    private void SetMineCount()
    {
        mineCount = gridManager.MineCount;
        mineCountTxt.text = mineCount.ToString("00");
    }

    private void ChangeMineCount(bool isFlagged)
    {
        if (isFlagged)
        {
            mineCount--;
            if (mineCount < 0)
            {
                mineCount = 0;
            }
        }
        else
        {
            mineCount++;
        }
        
        mineCountTxt.text = mineCount.ToString("00");
    }
}
