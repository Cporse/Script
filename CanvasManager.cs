using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private Text plate_Count;
    [SerializeField] private Text level;

    public static CanvasManager Instance;

    int plate = 0;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        //LevelManager.Instance.LevelIndex = PlayerPrefs.GetInt("LevelIndex");
        level.text = "Level : " + (LevelManager.Instance.LevelIndex);
    }
    public void plateCountAdd()
    {
        plate++;
        plate_Count.text = "Plate count : " + plate;
    }
    public void plateCountRemove()
    {
        plate--;
        plate_Count.text = "Plate count : " + plate;
    }
}