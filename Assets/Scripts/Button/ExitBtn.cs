using UnityEngine;

public class ExitBtn : CustomButton
{
    protected override void ClickedHandle() {
        Application.Quit();
    }
}
