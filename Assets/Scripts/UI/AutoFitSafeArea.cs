using UnityEngine;

// Các UI tự động hiển thị tại safe area
[RequireComponent(typeof(RectTransform))]
public class AutoFitSafeArea : MonoBehaviour
{
    RectTransform rectTransform;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        FitSafeArea();
    }

    private void FitSafeArea() {
        Rect safeArea = Screen.safeArea;

        // Tính phần bị cắt (cutout) ở các cạnh
        float left = safeArea.x;
        float bottom = safeArea.y;
        float right = Screen.width - (safeArea.x + safeArea.width);
        float top = Screen.height - (safeArea.y + safeArea.height);

        rectTransform.offsetMin = new Vector2(left, bottom);
        rectTransform.offsetMax = new Vector2(-right, -top);
    }
}
