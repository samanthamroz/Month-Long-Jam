using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoLoadingZone : MonoBehaviour
{
    public string sceneToLoad;
    [SerializeField] Vector3 spawnPositionInNextRoom;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (sceneToLoad != null)
        {
            PlayerSaveData saveData;
            try {
                saveData = SaveManager.Load<PlayerSaveData>().saveData;
                saveData.nextSpawn = spawnPositionInNextRoom;
            } catch {
                saveData = new PlayerSaveData {
                    lastScene = SceneManager.GetActiveScene().name,
                    itemsCollected = new List<Tool>(),
                    ingredientsCollected = new List<Ingredient>(),
                    nextSpawn = spawnPositionInNextRoom
                };
            }
            SaveManager.Save(new SaveProfile<PlayerSaveData>(saveData));

            Autosave.SaveRoom();

            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
