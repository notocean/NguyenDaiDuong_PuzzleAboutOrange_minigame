using UnityEngine;

public class GridElement : MonoBehaviour
{
    GridSystem gridSystem;

    SpriteRenderer spriteRenderer;

    private void Awake() {
        gridSystem = GetComponentInParent<GridSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        AdjustScale();
    }

    private void AdjustScale() {
        float width = spriteRenderer.size.x;
        float height = spriteRenderer.size.y;
        float size = Mathf.Max(width, height);

        float scale = gridSystem.GetCellSize() / size;
        transform.localScale = new Vector2(scale, scale);
    }
}
