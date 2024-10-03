using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToolPickup : Pickup
{
    public Tool tool;
    public AudioClip collect;
    public override void Interaction(GameObject player)
    {
        PlayerSaveData saveData;
        try {
            saveData = SaveManager.Load<PlayerSaveData>().saveData;
            saveData.itemsCollected.Add(tool);
        } catch {
            saveData = new PlayerSaveData {
                lastScene = SceneManager.GetActiveScene().name,
                player = player.transform.position,
                itemsCollected = new List<Tool>{tool},
                ingredientsCollected = new List<Ingredient>()
            };
        }
        SaveManager.Save(new SaveProfile<PlayerSaveData>(saveData));

        collect = Resources.Load<AudioClip>("treasureitem");
        SoundFXManager.instance.PlaySoundFXClip(collect, transform, 1f);

        gameObject.SetActive(false);

        Autosave.SaveRoom();
    }
}
