using UnityEngine;
using UnityEngine.UI;

public abstract class CustomButton : MonoBehaviour
{
    protected Button btn;

    protected virtual void Awake() {
        btn = GetComponent<Button>();
    }

    protected virtual void Start() {
        btn.onClick.AddListener(ClickedHandle);
    }

    protected abstract void ClickedHandle();
}
