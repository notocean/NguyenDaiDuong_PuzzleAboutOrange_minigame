using UnityEngine;

public class EndCanvas : MonoBehaviour
{
    [SerializeField] GameObject winFrame;
    [SerializeField] GameObject loseFrame;
    [SerializeField] Transform starContainer;
    [SerializeField] GameObject winStarPrefab;
    [SerializeField] GameObject loseStarPrefab;

    public void Init(bool isWin, int starCount = 0) {
        foreach (Transform child in starContainer) {
            Destroy(child.gameObject);
        }


        if (isWin) {
            winFrame.SetActive(true);
            loseFrame.SetActive(false);
            int i = 0;
            while (i < starCount) {
                Instantiate(winStarPrefab, starContainer);
                i++;
            }
            while (i < 3) {
                Instantiate(loseStarPrefab, starContainer);
                i++;
            }
        }
        else {
            winFrame.SetActive(false);
            loseFrame.SetActive(true);
            for (int i = 0; i < 3; i++) {
                Instantiate(loseStarPrefab, starContainer);
            }
        }
    }
}
