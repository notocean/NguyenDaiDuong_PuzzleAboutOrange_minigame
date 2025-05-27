using System.Collections;
using TMPro;
using UnityEngine;

public class TimePlayer : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] float time;
    [SerializeField] TMP_Text textTMP;

    float timer = 0;

    public void BeginCountDown() {
        StopAllCoroutines();
        StartCoroutine(CountDown(time));
    }

    IEnumerator CountDown(float second) {
        timer = second;

        while (timer > 0) {
            timer -= Time.deltaTime;
            textTMP.text = $"00 : {timer:00}";

            yield return null;
        }

        canvas.enabled = true;
        EndCanvas endCanvas = canvas.GetComponent<EndCanvas>();
        if (endCanvas != null) {
            endCanvas.Init(false);
        }
    }

    public int GetStar() {
        float ratio = timer / time;
        int starCount;

        if (ratio > 0.6f) starCount = 3;
        else if (ratio > 0.3) starCount = 2;
        else starCount = 1;

        return starCount;
    }

    private void OnEnable() {
        GameManager.Instance.OnBeginPlay += BeginCountDown;
    }

    private void OnDisable() {
        GameManager.Instance.OnBeginPlay -= BeginCountDown;
    }
}
