using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class SaveHandler : Singleton<SaveHandler> 
{
    private Dictionary<string, Tilemap> tilemaps = new();
    private Dictionary<TileBase, BuildingObjectBase> tileBaseToBuildingObject = new();
    private Dictionary<string, TileBase> nameToTileBase = new();

    [SerializeField] private string filename = "tilemapData.json";

    private void Start() 
    {
        InitTilemaps();
        InitTileReferences();
    }
    
    private void InitTilemaps() 
    {
        var maps = FindObjectsOfType<Tilemap>();
        
        foreach (var map in maps) {
            tilemaps.Add(map.name, map);
        }
    }

    private void InitTileReferences() 
    {
        var buildables = Resources.LoadAll<BuildingObjectBase>("Scriptables/Buildables");

        foreach (BuildingObjectBase buildable in buildables) {
            if (!tileBaseToBuildingObject.ContainsKey(buildable.TileBase)) {
                tileBaseToBuildingObject.Add(buildable.TileBase, buildable);
                nameToTileBase.Add(buildable.name, buildable.TileBase);
            } else {
                Debug.LogError(
                    $"TileBase {buildable.TileBase.name} " +
                               $"is already in use by {tileBaseToBuildingObject[buildable.TileBase].name}");
            }
        }
    }

    

    public void OnSave() 
    {
        var data = new List<TilemapData>();

        foreach (var mapObj in tilemaps) 
        {
            var mapData = new TilemapData { key = mapObj.Key };
            var boundsForThisMap = mapObj.Value.cellBounds;

            for (var x = boundsForThisMap.xMin; x < boundsForThisMap.xMax; x++) 
                for (var y = boundsForThisMap.yMin; y < boundsForThisMap.yMax; y++) {
                    var pos = new Vector3Int(x, y, 0);
                    var tile = mapObj.Value.GetTile(pos);

                    if (tile == null || !tileBaseToBuildingObject.ContainsKey(tile)) continue;
                    var guid = tileBaseToBuildingObject[tile].name;
                    var ti = new TileInfo(pos, guid);
                    mapData.tiles.Add(ti);
                }
            data.Add(mapData);
        }
        FileHandler.SaveToJSON(data, filename);
        Debug.Log(tileBaseToBuildingObject.Count);
    }

    public void OnLoad() 
    {
        var data = FileHandler.ReadListFromJSON<TilemapData>(filename);

        foreach (var mapData in data) 
        {
            if (!tilemaps.ContainsKey(mapData.key))
            {
                Debug.LogError($"Found saved data for tilemap called '{mapData.key}', but Tilemap does not exist in scene.");
                continue;
            }

            var map = tilemaps[mapData.key];
            
            map.ClearAllTiles();

            if (mapData.tiles is not { Count: > 0 }) continue;
            
            foreach (var tile in mapData.tiles) {

                if (nameToTileBase.ContainsKey(tile.guidForBuildable))
                    map.SetTile(tile.position, nameToTileBase[tile.guidForBuildable]);
                else 
                    Debug.LogError($"Reference {tile.guidForBuildable} could not be found.");
            }
        }
    }
}