using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance;

    public int max_Plate = 0;
    public GameObject maxPlate;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void Control(int value, GameObject maxPlate)
    {
        if (value > max_Plate)
        {
            max_Plate = value;
            this.maxPlate = maxPlate;
            //Controller_Camera.Instance.Set_Target(Controller_Vault.Instance.list_game_object[Controller_Vault.Instance.list_game_object.Count - 1].transform);
            Controller_Camera.Instance.Set_Target(maxPlate.transform);
            Debug.Log("New Record " + max_Plate);
        }
    }
}