using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class PauseMenuController : MonoBehaviour
{
    public GameObject saveQuitButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveQuit() {
        Debug.Log(message: "This button is being pressed!");
        Application.Quit();
    }
}
