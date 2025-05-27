using UnityEngine;
using UnityEngine.UI;

public class LevelInfor : CustomButton
{
    [SerializeField] Image levelImage;
    [SerializeField] RectTransform starContainer;
    [SerializeField] GameObject lockObj;
    [SerializeField] GameObject winStarPrefab;
    [SerializeField] GameObject loseStarPrefab;

    LevelData leveData;

    public void Init(LevelData levelData) {
        this.leveData = levelData;

        if (levelData.isActive) {
            lockObj.SetActive(false);

            int i = 0;
            while (i < levelData.starCount) {
                Instantiate(winStarPrefab, starContainer);
                i++;
            }
            while (i < 3) {
                Instantiate(loseStarPrefab, starContainer);
                i++;
            }
        }
        else {
            levelImage.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }

    protected override void ClickedHandle() {
        if (leveData.isActive) {
            if (LevelDataManager.Instance.SetLevelIndex(leveData.index)) {
                GameManager.Instance.ChangeScene(1);
            }
        }
    }
}
