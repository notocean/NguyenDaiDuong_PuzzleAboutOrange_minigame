using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] Canvas canvas;

    private void Awake() {
        ShowTutorial();
    }

    private void ShowTutorial() {
        if (LevelDataManager.Instance.isTutorial) {
            canvas.enabled = true;
            LevelDataManager.Instance.isTutorial = false;
            GameManager.Instance.gameState = GameState.Pause;
        }
    }
}
