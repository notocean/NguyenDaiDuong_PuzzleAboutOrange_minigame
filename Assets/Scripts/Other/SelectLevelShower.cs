using UnityEngine;

public class SelectLevelShower : MonoBehaviour
{
    [SerializeField] GameObject levelPrefab;

    private void Awake() {
        ShowLevel();
    }

    private void ShowLevel() {
        foreach (LevelData levelData in LevelDataManager.Instance.levelDataList) {
            GameObject level = Instantiate(levelPrefab, transform);
            LevelInfor levelInfor = level.GetComponent<LevelInfor>();
            if (levelInfor != null) {
                levelInfor.Init(levelData);
            }
        }
    }
}
