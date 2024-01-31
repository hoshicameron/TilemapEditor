using UnityEngine;
using UnityEngine.UI;

public class BuildingButtonHandler : MonoBehaviour {
    [SerializeField] BuildingObjectBase item;
    Button button;

    

    private void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClicked);
    }

    public BuildingObjectBase Item {
        set {
            item = value;
        }
    }

    private void ButtonClicked() {
        Debug.Log("Button was clicked: " + item.name);
        BuildingCreator.Instance.ObjectSelected(item);
    }

}