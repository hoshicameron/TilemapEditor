using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Buildable", menuName = "LevelBuilding/Create Buildable")]
public class BuildingObjectBase : ScriptableObject {
    [SerializeField] private BuildingCategory category;
    [SerializeField] private UICategory uiCategory;
    [SerializeField] private TileBase tileBase;
    [SerializeField] private PlaceType placeType;
    [SerializeField] private bool usePlacementRestrictions;
    [SerializeField] private List<BuildingCategory> placementRestrictions;

    public List<BuildingCategory> PlacementRestrictions => usePlacementRestrictions ? placementRestrictions : category.PlacementRestrictions;

    public TileBase TileBase => tileBase;

    public PlaceType PlaceType => placeType == PlaceType.None ? category.PlaceType : placeType;

    public BuildingCategory Category => category;

    public UICategory UICategory => uiCategory;
}