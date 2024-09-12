using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class TitleScreenController : MonoBehaviour
{
    [SerializeField] private string firstScene;
    public GameObject continueButton, newGameButton, settingsButton, quitButton;

    void Start()
    {
        if (!SaveManager.GameDataExists()) {
            continueButton.GetComponent<Button>().interactable = false;
        }
    }

    void Update()
    {
        
    }

    public void Continue() {
        var saveData = SaveManager.Load<PlayerSaveData>().saveData;
        SceneManager.LoadScene(saveData.lastScene);
    }

    public void NewGame() {
        if (SaveManager.GameDataExists()) {
            Debug.Log("Overwriting!");
        }

        try {
            SaveManager.DeleteAll();
        } finally {
            SceneManager.LoadScene(firstScene);
        }
    }

    public void Settings() {
        throw new System.NotImplementedException();
    }

    public void Quit() {
        Application.Quit();
    }
}
