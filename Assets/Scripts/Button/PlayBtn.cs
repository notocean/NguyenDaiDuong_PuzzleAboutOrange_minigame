using UnityEngine;

public class PlayBtn : CustomButton
{
    [SerializeField] Canvas canvas;

    protected override void ClickedHandle() {
        canvas.enabled = true;
    }
}
