using UnityEngine;

public class ReplayBtn : CustomButton
{
    [SerializeField] Canvas canvas;

    protected override void ClickedHandle() {
        if (canvas != null) {
            canvas.enabled = false;
            GameManager.Instance.gameState = GameState.Play;
        }

        GameManager.Instance.LoadLevel();
    }
}
