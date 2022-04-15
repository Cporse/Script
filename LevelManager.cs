using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int LevelIndex { get => PlayerPrefs.GetInt("LevelIndex", 1); set => PlayerPrefs.SetInt("LevelIndex", value); }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (LevelIndex > 3)
        {
            LevelIndex = 1;
        }

        //Instantiate(levelPreFabs[LevelIndex]);
        Level level = Resources.Load<Level>("Levels/Level" + LevelIndex);
        Instantiate(level.LevelPrefab);
    }
    private void Start()
    {

    }
}