using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : InteractableObject
{
    [SerializeField] string sceneToLoad;
    [SerializeField] Vector3 spawnPositionInNextRoom;
    public override string HoverText()
    {
        return "Open";
    }
    
    public override void Interaction(GameObject player) {
        if (sceneToLoad != null)
        {
            PlayerSaveData saveData;
            try {
                saveData = SaveManager.Load<PlayerSaveData>().saveData;
                saveData.nextSpawn = spawnPositionInNextRoom;
            } catch {
                saveData = new PlayerSaveData {
                    lastScene = SceneManager.GetActiveScene().name,
                    player = player.transform.position,
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