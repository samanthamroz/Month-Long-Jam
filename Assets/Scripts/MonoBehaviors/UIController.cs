using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject canvasPrefab;
    private GameObject canvasRef;

    void Awake()
    {
        canvasRef = Instantiate(canvasPrefab);
    }
}
