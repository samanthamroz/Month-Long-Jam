using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngredientPickup : Pickup
{
    public Ingredient ingredient;
    public AudioClip collect;
    public override void Interaction(GameObject player)
    {
        PlayerSaveData saveData;

        try {
            saveData = SaveManager.Load<PlayerSaveData>().saveData;
            saveData.ingredientsCollected.Add(ingredient);
        } catch {
            saveData = new PlayerSaveData {
                lastScene = SceneManager.GetActiveScene().name,
                player = player.transform.position,
                itemsCollected = new List<Tool>(),
                ingredientsCollected = new List<Ingredient>{ingredient}
            };
        }
        SaveManager.Save(new SaveProfile<PlayerSaveData>(saveData));

        collect = Resources.Load<AudioClip>("ingredient");
        SoundFXManager.instance.PlaySoundFXClip(collect, transform, 1f);

        gameObject.SetActive(false);

        Autosave.SaveRoom();
    }
}
