using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolPickup : Pickup
{
    public Tool tool;
    public override void Interaction(GameObject player)
    {
        var saveData = SaveManager.Load<PlayerSaveData>().saveData;
        saveData.itemsCollected.Add(tool);
        SaveManager.Save(new SaveProfile<PlayerSaveData>(saveData));

        gameObject.SetActive(false);

        Autosave.SaveRoom();
    }
}
