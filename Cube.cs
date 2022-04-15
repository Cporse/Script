using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CubeColor
{
    Red = 0,
    Green = 1,
    Blue = 2
}
public class Cube : MonoBehaviour
{
    [SerializeField] private CubeColor cubeColor;

    public CubeColor CubeColor { get => cubeColor; set => cubeColor = value; }
}