using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState {
    Play, Pause
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null) {
                    _instance = new GameObject("GameManager").AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }

    public Action OnBeginPlay;
    public Action OnLose;
    public Action OnWin;

    GameState _gameState = GameState.Play;
    public GameState gameState {
        get { return _gameState; }
        set {
            _gameState = value;
            if (_gameState == GameState.Play) {
                Time.timeScale = 1f;
            }
            else if (_gameState == GameState.Pause) {
                Time.timeScale = 0f;
            }
        }
    }

    int sceneIndex;

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
        }
        else {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        if (sceneIndex != 0) {
            LoadLevel();
        }
    }

    public void ChangeScene(int index) {
        if (sceneIndex == index) return;

        if (index >= 0 && index <= SceneManager.sceneCountInBuildSettings) {
            sceneIndex = index;
            SceneManager.LoadScene(index);
        }
    }

    public void LoadLevel() {
        OnBeginPlay?.Invoke();
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }
}
