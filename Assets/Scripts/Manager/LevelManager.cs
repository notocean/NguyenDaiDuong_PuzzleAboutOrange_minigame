using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    LevelData levelData;
    [SerializeField] GridSystem gridSystem;
    [SerializeField] Canvas canvas;
    [SerializeField] float waitToWinningTime;
    [SerializeField] TimePlayer timePlayer;

    [SerializeField] Orange orange1;
    [SerializeField] Orange orange2;
    [SerializeField] Orange orange3;
    [SerializeField] Orange orange4;
    [SerializeField] GameObject block1;
    [SerializeField] GameObject block2;
    [SerializeField] GameObject block3;

    public void LoadLevel() {
        gridSystem.ResetGrid();
        levelData = LevelDataManager.Instance.GetLevelData();
        Init(levelData);
    }

    void Init(LevelData levelData) {
        gridSystem.PlaceObject(orange1.gameObject, levelData.orange1Coordinate);
        gridSystem.PlaceObject(orange2.gameObject, levelData.orange2Coordinate);
        gridSystem.PlaceObject(orange3.gameObject, levelData.orange3Coordinate);
        gridSystem.PlaceObject(orange4.gameObject, levelData.orange4Coordinate);
        gridSystem.PlaceObject(block1, levelData.block1Coordinate);
        gridSystem.PlaceObject(block2, levelData.block2Coordinate);
        gridSystem.PlaceObject(block3, levelData.block3Coordinate);
    }

    public void Check() {
        if (orange1.coordinate + Vector2Int.right == orange2.coordinate &&
            orange1.coordinate + Vector2Int.up == orange3.coordinate && 
            orange2.coordinate + Vector2Int.up == orange4.coordinate) {
            StartCoroutine(Winning());
        }
    }

    IEnumerator Winning() {
        yield return new WaitForSeconds(waitToWinningTime);

        int starCount = timePlayer.GetStar();
        if (levelData.starCount < starCount) {
            levelData.starCount = starCount;
        }
        LevelDataManager.Instance.ActiveNewLevel();

        GameManager.Instance.gameState = GameState.Pause;
        canvas.enabled = true;
        EndCanvas endCanvas = canvas.GetComponent<EndCanvas>();
        if (endCanvas != null) {
            endCanvas.Init(true, starCount);
        }
        LevelDataManager.Instance.Save();
    }

    private void OnEnable() {
        GameManager.Instance.OnBeginPlay += LoadLevel;
        gridSystem.OnCheck += Check;
    }

    private void OnDisable() {
        GameManager.Instance.OnBeginPlay -= LoadLevel;
        gridSystem.OnCheck -= Check;
    }
}