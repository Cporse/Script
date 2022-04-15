using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish_Plate : MonoBehaviour
{
    public static Finish_Plate Instance;
    [SerializeField] public int coefficient;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("plate"))
        {
            Game_Manager.Instance.Control(coefficient, collision.gameObject);
        }
    }

    //END LINE.
}