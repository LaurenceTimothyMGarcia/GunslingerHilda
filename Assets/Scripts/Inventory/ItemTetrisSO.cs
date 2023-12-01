// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// [CreateAssetMenu()]
// public class ItemTetrisSO : PlacedObjectTypeSO {



//     public static void CreateVisualGrid(Transform visualParentTransform, ItemTetrisSO itemTetrisSO, float cellSize) {
//         Transform visualTransform = Instantiate(InventoryTetrisAssets.Instance.gridVisual, visualParentTransform);

//         // Create background
//         Transform template = visualTransform.Find("Template");
//         template.gameObject.SetActive(false);

//         for (int x = 0; x < itemTetrisSO.width; x++) {
//             for (int y = 0; y < itemTetrisSO.height; y++) {
//                 Transform backgroundSingleTransform = Instantiate(template, visualTransform);
//                 backgroundSingleTransform.gameObject.SetActive(true);
//             }
//         }

//         visualTransform.GetComponent<GridLayoutGroup>().cellSize = Vector2.one * cellSize;

//         visualTransform.GetComponent<RectTransform>().sizeDelta = new Vector2(itemTetrisSO.width, itemTetrisSO.height) * cellSize;

//         visualTransform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

//         visualTransform.SetAsFirstSibling(); 
//     }


// }
