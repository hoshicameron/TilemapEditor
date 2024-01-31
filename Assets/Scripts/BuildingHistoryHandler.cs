using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingHistoryHandler : MonoBehaviour {
    [SerializeField] Button undoButton, redoButton;
    

    private void Awake() {
        undoButton.onClick.AddListener(Undo);
        redoButton.onClick.AddListener(Redo);
    }

    private void Update() {
        SetInteractable(BuildingHistory.Instance.CanUndo, undoButton);
        SetInteractable(BuildingHistory.Instance.CanRedo, redoButton);
    }

    private void SetInteractable(bool interactable, Button button) {
        if (button.interactable != interactable) {
            button.interactable = interactable;
        }
    }

    private void Undo() {
        BuildingHistory.Instance.UndoStep();
    }

    private void Redo() {
        BuildingHistory.Instance.RedoStep();
    }

}
