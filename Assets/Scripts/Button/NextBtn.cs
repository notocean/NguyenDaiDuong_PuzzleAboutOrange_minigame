using Unity.VisualScripting;
using UnityEngine;

public class NextBtn : CustomButton
{
    [SerializeField] Canvas canvas;

    protected override void ClickedHandle() {
        if (LevelDataManager.Instance.IncreaseLevel()) {
            canvas.enabled = false;
            GameManager.Instance.gameState = GameState.Play;
            GameManager.Instance.LoadLevel();
        }
        else GameManager.Instance.ChangeScene(0);
    }
}
