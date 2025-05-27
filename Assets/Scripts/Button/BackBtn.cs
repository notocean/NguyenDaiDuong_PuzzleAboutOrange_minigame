using UnityEngine;

public class BackBtn : CustomButton
{
    [SerializeField] Canvas canvas;

    protected override void ClickedHandle() {
        canvas.enabled = false;
        GameManager.Instance.gameState = GameState.Play;
    }
}
