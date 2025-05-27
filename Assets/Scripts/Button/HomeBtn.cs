public class HomeBtn : CustomButton
{
    protected override void ClickedHandle() {
        GameManager.Instance.gameState = GameState.Play;
        GameManager.Instance.ChangeScene(0);
    }
}
