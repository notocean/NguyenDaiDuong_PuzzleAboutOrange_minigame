using System;
using UnityEngine;

public enum InputType {
    Left, Right, Up, Down
}

public class InputHandler : MonoBehaviour {
    public Action<InputType> OnInput;

    Vector2 startTouchPos;
    Vector2 endTouchPos;
    bool isSwiping = false;

    [SerializeField] float swipeThreshold = 50f;

    private void Update() {
        if (GameManager.Instance.gameState == GameState.Play) {
            HandleTouchInput();
        }
    }

    void HandleTouchInput() {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        switch (touch.phase) {
            case TouchPhase.Began:
                isSwiping = true;
                startTouchPos = touch.position;
                break;
            case TouchPhase.Moved:
                if (isSwiping) {
                    endTouchPos = touch.position;
                    DetectSwipe();
                }
                break;
            case TouchPhase.Ended:
                isSwiping = false;
                break;
        }
    }

    void DetectSwipe() {
        Vector2 delta = endTouchPos - startTouchPos;

        if (delta.magnitude < swipeThreshold) return;

        isSwiping = false;
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y)) {
            if (delta.x > 0) OnInput?.Invoke(InputType.Right);
            else OnInput?.Invoke(InputType.Left);
        }
        else {
            if (delta.y > 0) OnInput?.Invoke(InputType.Up);
            else OnInput?.Invoke(InputType.Down);
        }
    }
}
