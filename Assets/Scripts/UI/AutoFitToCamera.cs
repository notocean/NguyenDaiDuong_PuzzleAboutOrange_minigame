using UnityEngine;

// Tự động điều chỉnh background phủ toàn bộ screen
[RequireComponent(typeof(SpriteRenderer))]
public class AutoFitToCamera : MonoBehaviour 
{
    SpriteRenderer bg;

    private void Awake() {
        bg = GetComponent<SpriteRenderer>();
        FitToCamera();
    }

    private void FitToCamera() {
        float orthographicSize = Camera.main.orthographicSize;

        // Tỷ lệ background
        float wBackground = bg.bounds.size.x;
        float hBackground = bg.bounds.size.y;
        float ratioBg = wBackground / hBackground;

        // Tỷ lệ screen
        float ratioScreen = (float)Screen.width / Screen.height;

        float scale = transform.localScale.x;
        if (ratioScreen > ratioBg) {
            scale = (ratioScreen * orthographicSize * 2) / wBackground * scale;
        }
        else {
            scale = (orthographicSize * 2) / hBackground * scale;
        }

        transform.localScale = new Vector3(scale, scale, scale);
    }
}