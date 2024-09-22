using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPickup : Pickup
{
    public Ingredient ingredient;
    public override void Interaction(GameObject player)
    {
        var saveData = SaveManager.Load<PlayerSaveData>().saveData;
        saveData.ingredientsCollected.Add(ingredient);
        SaveManager.Save(new SaveProfile<PlayerSaveData>(saveData));

        gameObject.SetActive(false);

        Autosave.SaveRoom();
    }
}
