using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class TitleScreenController : MonoBehaviour
{
    [SerializeField] private string firstScene;
    [SerializeField] private Vector3 spawnPositionInNextRoom;
    public GameObject continueButton, newGameButton, settingsButton, quitButton;

    void Start()
    {
        if (!SaveManager.GameDataExists()) {
            continueButton.GetComponent<Button>().interactable = false;
        }
    }

    public void Continue() {
        var saveData = SaveManager.Load<PlayerSaveData>().saveData;
        SceneManager.LoadScene(saveData.lastScene);
    }

    public void NewGame() {
        try {
            SaveManager.DeleteAll();
        } finally {
            PlayerSaveData saveData;
            try {
                saveData = SaveManager.Load<PlayerSaveData>().saveData;
                saveData.nextSpawn = spawnPositionInNextRoom;
            } catch {
                saveData = new PlayerSaveData {
                    lastScene = firstScene,
                    itemsCollected = new List<Tool>(),
                    ingredientsCollected = new List<Ingredient>(),
                    nextSpawn = spawnPositionInNextRoom
                };
            }
            SaveManager.Save(new SaveProfile<PlayerSaveData>(saveData));

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
