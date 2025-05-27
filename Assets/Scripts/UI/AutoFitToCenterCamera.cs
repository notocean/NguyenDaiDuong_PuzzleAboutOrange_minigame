using UnityEngine;

public class AutoFitToCenterCamera : MonoBehaviour {
    [SerializeField] RectTransform rectTrans;

    private void Start() {
        FitToCenter();
    }

    private void FitToCenter() {
        Vector3 center = Camera.main.ScreenToWorldPoint(rectTrans.position);
        center.z = 0;
        transform.position = center;
    }
}
