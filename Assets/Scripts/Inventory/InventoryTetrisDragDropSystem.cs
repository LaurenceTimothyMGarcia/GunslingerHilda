using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTetrisDragDropSystem : MonoBehaviour {

    public static InventoryTetrisDragDropSystem Instance { get; private set; }



    [SerializeField] private List<InventoryTetris> inventoryTetrisList;

    private InventoryTetris draggingInventoryTetris;
    private PlacedObject draggingPlacedObject;
    private Vector2Int mouseDragGridPositionOffset;
    private Vector2 mouseDragAnchoredPositionOffset;
    private PlacedObjectTypeSO.Dir dir;


    private void Awake() {
        Instance = this;
    }

    private void Start() {
        foreach (InventoryTetris inventoryTetris in inventoryTetrisList) {
            inventoryTetris.OnObjectPlaced += (object sender, PlacedObject placedObject) => {

            };
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            dir = PlacedObjectTypeSO.GetNextDir(dir);
        }

        if (draggingPlacedObject != null) {
            // Calculate target position to move the dragged item
            RectTransformUtility.ScreenPointToLocalPointInRectangle(draggingInventoryTetris.GetItemContainer(), Input.mousePosition, null, out Vector2 targetPosition);
            targetPosition += new Vector2(-mouseDragAnchoredPositionOffset.x, -mouseDragAnchoredPositionOffset.y);

            // Apply rotation offset to target position
            Vector2Int rotationOffset = draggingPlacedObject.GetPlacedObjectTypeSO().GetRotationOffset(dir);
            targetPosition += new Vector2(rotationOffset.x, rotationOffset.y) * draggingInventoryTetris.GetGrid().GetCellSize();

            // Snap position
            targetPosition /= 10f;// draggingInventoryTetris.GetGrid().GetCellSize();
            targetPosition = new Vector2(Mathf.Floor(targetPosition.x), Mathf.Floor(targetPosition.y));
            targetPosition *= 10f;

            // Move and rotate dragged object
            draggingPlacedObject.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(draggingPlacedObject.GetComponent<RectTransform>().anchoredPosition, targetPosition, Time.deltaTime * 20f);
            draggingPlacedObject.transform.rotation = Quaternion.Lerp(draggingPlacedObject.transform.rotation, Quaternion.Euler(0, 0, -draggingPlacedObject.GetPlacedObjectTypeSO().GetRotationAngle(dir)), Time.deltaTime * 15f);
        }
    }

    public void StartedDragging(InventoryTetris inventoryTetris, PlacedObject placedObject) {
        // Started Dragging
        draggingInventoryTetris = inventoryTetris;
        draggingPlacedObject = placedObject;

        Cursor.visible = false;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(inventoryTetris.GetItemContainer(), Input.mousePosition, null, out Vector2 anchoredPosition);
        Vector2Int mouseGridPosition = inventoryTetris.GetGridPosition(anchoredPosition);

        // Calculate Grid Position offset from the placedObject origin to the mouseGridPosition
        mouseDragGridPositionOffset = mouseGridPosition - placedObject.GetGridPosition();

        // Calculate the anchored poisiton offset, where exactly on the image the player clicked
        mouseDragAnchoredPositionOffset = anchoredPosition - placedObject.GetComponent<RectTransform>().anchoredPosition;

        // Save initial direction when started draggign
        dir = placedObject.GetDir();

        // Apply rotation offset to drag anchored position offset
        Vector2Int rotationOffset = draggingPlacedObject.GetPlacedObjectTypeSO().GetRotationOffset(dir);
        mouseDragAnchoredPositionOffset += new Vector2(rotationOffset.x, rotationOffset.y) * draggingInventoryTetris.GetGrid().GetCellSize();
    }

    public void StoppedDragging(InventoryTetris fromInventoryTetris, PlacedObject placedObject) {
        draggingInventoryTetris = null;
        draggingPlacedObject = null;

        Cursor.visible = true;

        // Remove item from its current inventory
        fromInventoryTetris.RemoveItemAt(placedObject.GetGridPosition());

        InventoryTetris toInventoryTetris = null;

        // Find out which InventoryTetris is under the mouse position
        foreach (InventoryTetris inventoryTetris in inventoryTetrisList) {
            Vector3 screenPoint = Input.mousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(inventoryTetris.GetItemContainer(), screenPoint, null, out Vector2 anchoredPosition);
            Vector2Int placedObjectOrigin = inventoryTetris.GetGridPosition(anchoredPosition);
            placedObjectOrigin = placedObjectOrigin - mouseDragGridPositionOffset;

            if (inventoryTetris.IsValidGridPosition(placedObjectOrigin)) {
                toInventoryTetris = inventoryTetris;
                break;
            }
        }

        // Check if it's on top of a InventoryTetris
        if (toInventoryTetris != null) {
            Vector3 screenPoint = Input.mousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(toInventoryTetris.GetItemContainer(), screenPoint, null, out Vector2 anchoredPosition);
            Vector2Int placedObjectOrigin = toInventoryTetris.GetGridPosition(anchoredPosition);
            placedObjectOrigin = placedObjectOrigin - mouseDragGridPositionOffset;

            bool tryPlaceItem = toInventoryTetris.TryPlaceItem(placedObject.GetPlacedObjectTypeSO() as ItemTetrisSO, placedObjectOrigin, dir);

            if (tryPlaceItem) {
                // Item placed!
            } else {
                // Cannot drop item here!
                // Drop on original position
                fromInventoryTetris.TryPlaceItem(placedObject.GetPlacedObjectTypeSO() as ItemTetrisSO, placedObject.GetGridPosition(), placedObject.GetDir());
            }
        } else {
            // Not on top of any Inventory Tetris!
            // Cannot drop item here!
            // Drop on original position
            fromInventoryTetris.TryPlaceItem(placedObject.GetPlacedObjectTypeSO() as ItemTetrisSO, placedObject.GetGridPosition(), placedObject.GetDir());
        }
    }


}