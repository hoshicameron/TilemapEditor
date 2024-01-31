using System;
using System.Collections.Generic;

[Serializable]
public class TilemapData 
{
    public string key; 
    public List<TileInfo> tiles = new();
}