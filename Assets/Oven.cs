using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Oven : InteractableObject
{
    public override string HoverText()
    {
        return "Bake";
    }

    public override void Interaction(GameObject player)
    {
        if (HasRequiredItems()) {
            SceneManager.LoadScene("Ending");
        } else {
            Debug.Log("don't have all items");
            //player.GetComponent<UIController>().SetNotEnoughItemsPopupActive(true);
            StartCoroutine(PopupCoroutine(player));
        }
    }

    private bool HasRequiredItems() {
        PlayerSaveData saveData = SaveManager.Load<PlayerSaveData>().saveData;

        foreach (Ingredient i in saveData.ingredientsCollected) {
            Debug.Log(i);
        }
        foreach (Tool i in saveData.itemsCollected) {
            Debug.Log(i);
        }
        

        return saveData.ingredientsCollected.Contains(Ingredient.FLOUR) &&
            saveData.ingredientsCollected.Contains(Ingredient.MILK) &&
            saveData.ingredientsCollected.Contains(Ingredient.EGGS) &&
            saveData.ingredientsCollected.Contains(Ingredient.BUTTER) &&
            saveData.ingredientsCollected.Contains(Ingredient.BAKINGPOWDER) &&
            saveData.ingredientsCollected.Contains(Ingredient.SUGAR) &&
            saveData.itemsCollected.Contains(Tool.MIXER) &&
            saveData.itemsCollected.Contains(Tool.SPATULA);
    }

    private IEnumerator PopupCoroutine(GameObject player) {
        player.GetComponent<UIController>().SetNotEnoughItemsPopupActive(true);
        yield return new WaitForSeconds(2);
        player.GetComponent<UIController>().SetNotEnoughItemsPopupActive(false);
    }
}
