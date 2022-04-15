using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public static MaterialManager Instance;
    [SerializeField] private List<Material> materialList = new List<Material>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public Material GetMaterial(CubeColor cubeColor)
    {

        return materialList[(int)cubeColor];
    }
}