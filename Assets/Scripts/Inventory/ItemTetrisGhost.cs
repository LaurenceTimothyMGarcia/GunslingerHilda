// //preview of position
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class ItemTetrisGhost : MonoBehaviour {

//     private RectTransform rectTransform;
//     private Transform visual;
//     private PlacedObjectTypeSO placedObjectTypeSO;

//     private void Awake() {
//         rectTransform = GetComponent<RectTransform>();
//     }

//     private void Start() {
//         RefreshVisual();

//         InventoryTetrisManualPlacement.Instance.OnSelectedChanged += Instance_OnSelectedChanged;
//     }

//     private void Instance_OnSelectedChanged(object sender, System.EventArgs e) {
//         RefreshVisual();
//     }

//     private void LateUpdate() {
//         Vector2 targetPosition = InventoryTetrisManualPlacement.Instance.GetCanvasSnappedPosition();

//         rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPosition, Time.deltaTime * 15f);
//         transform.rotation = Quaternion.Lerp(transform.rotation, InventoryTetrisManualPlacement.Instance.GetPlacedObjectRotation(), Time.deltaTime * 15f);
//     }

//     private void RefreshVisual() {
//         if (visual != null) {
//             Destroy(visual.gameObject);
//             visual = null;
//         }

//         PlacedObjectTypeSO placedObjectTypeSO = InventoryTetrisManualPlacement.Instance.GetPlacedObjectTypeSO();

//         if (placedObjectTypeSO != null) {
//             visual = Instantiate(placedObjectTypeSO.visual, transform);
//             visual.localPosition = Vector3.zero;
//             visual.localEulerAngles = Vector3.zero;
//         }
//     }

// }
