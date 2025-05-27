using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDataManager", menuName = "Manager/LevelDataManager")]
public class LevelDataManager : ScriptableObject
{
    private static LevelDataManager _instance;
    public static LevelDataManager Instance {
        get {
            if (_instance == null) {
                _instance = Resources.Load<LevelDataManager>("LevelDataManager");
            }
            return _instance;
        }
    }

    public List<LevelData> levelDataList = new List<LevelData>();
    public bool isTutorial = true;
    int currentLevelIndex = 0;

    public LevelData GetLevelData() {
        return levelDataList[currentLevelIndex];
    }

    public bool SetLevelIndex(int index) {
        if (index >= 0 && index < levelDataList.Count) {
            currentLevelIndex = index;
            return true;
        }
        else return false;
    }

    public void ActiveNewLevel() {
        if (currentLevelIndex < levelDataList.Count - 1) {
            levelDataList[currentLevelIndex + 1].isActive = true;
        }
    }

    public bool IncreaseLevel() {
        if (currentLevelIndex < levelDataList.Count - 1 && levelDataList[currentLevelIndex].starCount > 0) {
            currentLevelIndex += 1;
            return true;
        }
        else return false;
    }

    private void Load() {
        for (int i = 0; i < levelDataList.Count; i++) {
            Load(i);
        }
    }

    private void Load(int i) {
        string filePath = Application.persistentDataPath + $"/level{i}.json";

        if (File.Exists(filePath)) {
            string json = File.ReadAllText(filePath);
            Data data = JsonUtility.FromJson<Data>(json);
            levelDataList[i].isActive = data.isActive;
            levelDataList[i].starCount = data.starCount;
        }
        else Save(i);
    }

    public void Save() {
        for (int i = 0; i < levelDataList.Count; i++) {
            Save(i);
        }
    }

    public void Save(int i) {
        string filePath = Application.persistentDataPath + $"/level{i}.json";

        Data data = new Data();

        data.isActive = levelDataList[i].isActive;
        data.starCount = levelDataList[i].starCount;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
    }

    private void OnEnable() {
        Load();
    }
}

[Serializable]
public class Data {
    public bool isActive;
    public int starCount;
}