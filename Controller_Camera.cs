using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Camera : MonoBehaviour
{
    [SerializeField] private Transform target;
    public static Controller_Camera Instance;
    private float lerpTime = 0.07f;
    
    Vector3 Margin;

    void Start()
    {
        Margin = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z - 7);
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void FixedUpdate()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position + Margin, lerpTime);
        }
    }
    public void Set_Target(Transform target)
    {
        this.target = target;
        lerpTime = 0.01f;
    }
}