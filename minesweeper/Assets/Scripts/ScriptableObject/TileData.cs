using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileData", menuName = "ScriptableObjects/TileData", order = 1)]
public class TileData : ScriptableObject
{
    [System.Serializable]
    public class TileSpritePair
    {
        public string tileType;
        public Sprite sprite;
    }

    public List<TileSpritePair> tileSprites = new List<TileSpritePair>();
    public Dictionary<string, Sprite> GetTileSpriteDictionary()
    {
        var dictionary = new Dictionary<string, Sprite>();
        foreach (var pair in tileSprites)
        {
            if (!dictionary.ContainsKey(pair.tileType))
            {
                dictionary.Add(pair.tileType, pair.sprite);
            }
        }
        return dictionary;
    }
}
