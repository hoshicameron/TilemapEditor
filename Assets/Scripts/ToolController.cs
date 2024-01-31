using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class ToolController : Singleton<ToolController>
{
    private List<Tilemap> tilemaps;

    private void Start()
    {
        tilemaps = new List<Tilemap>();
        var maps = FindObjectsOfType<Tilemap>().ToList();

        maps.ForEach(map => {
            if (map.name.Contains("Tilemap_")) {
                tilemaps.Add(map);
            }
        });

        tilemaps.Sort((a, b) =>
            b.GetComponent<TilemapRenderer>().sortingOrder.CompareTo(a.GetComponent<TilemapRenderer>().sortingOrder));
        Debug.Log(tilemaps.Count);
    }

    public void Eraser(Vector3Int[] positions, out BuildingHistoryStep historyStep) 
    {
        var items = new List<BuildingHistoryItem>();
        
        foreach (Vector3Int position in positions) {
            BuildingHistoryItem item = null;
            
            tilemaps.Any(map => {
                if (map.HasTile(position)) {
                    item = new BuildingHistoryItem(map, map.GetTile(position), null, position);
                    map.SetTile(position, null);
                    return true;
                }

                return false;
            });

            // we don't save any values where nothing had happened
            if (item != null) {
                items.Add(item);
            }
        }

        historyStep = new BuildingHistoryStep(items.ToArray());
    }
}
