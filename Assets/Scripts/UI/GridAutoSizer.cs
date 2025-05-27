using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Tự động điều chỉnh các kích thước các element trong grid
[RequireComponent(typeof(GridLayoutGroup))]
public class GridAutoSizer : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    GridLayoutGroup gridLayout;

    private void Awake() {
        gridLayout = GetComponent<GridLayoutGroup>();
    }

    private void Start() {
        StartCoroutine(AdjustCellSize());
    }

    IEnumerator AdjustCellSize() {
        yield return new WaitUntil(() => canvas.enabled);

        RectTransform rectTransform = GetComponent<RectTransform>();

        float width = rectTransform.rect.width;
        float cellSize = (width - gridLayout.spacing.x * (gridLayout.constraintCount - 1)) / gridLayout.constraintCount;

        gridLayout.cellSize = new Vector2(cellSize, cellSize);
    }
}
