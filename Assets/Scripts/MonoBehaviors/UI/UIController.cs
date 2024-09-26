using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject canvasPrefab;
    private GameObject canvasRef;
    private PlayerSaveData saveData { //read only
        get { return SaveManager.Load<PlayerSaveData>().saveData; }
    }

    void Awake()
    {
        canvasRef = Instantiate(canvasPrefab);
        SetInteractPopupActive(false);
        
    }

    public void SetInteractPopupActive(bool isActive, string text = "null") {
        GameObject popup = canvasRef.transform.GetChild(0).gameObject;
        popup.SetActive(isActive);
        popup.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void UpdateInteractPopupText(string text = "null") {
        canvasRef.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void SetPauseMenuActive(bool isActive)
    {
        GameObject pause = canvasRef.transform.GetChild(1).gameObject;
        pause.SetActive(isActive);
        UpdateItemsList();
    }

    public void UpdateItemsList() {
        //Debug.Log(saveData.ingredientsCollected.Contains(Ingredient.BAKINGPOWDER));
        GameObject bakingPowder = canvasRef.transform.GetChild(1).GetChild(0).gameObject;
        bakingPowder.SetActive(saveData.ingredientsCollected.Contains(Ingredient.BAKINGPOWDER));
        GameObject butter = canvasRef.transform.GetChild(1).GetChild(1).gameObject;
        butter.SetActive(saveData.ingredientsCollected.Contains(Ingredient.BUTTER));
        GameObject eggs = canvasRef.transform.GetChild(1).GetChild(2).gameObject;
        eggs.SetActive(saveData.ingredientsCollected.Contains(Ingredient.EGGS));
        GameObject flour = canvasRef.transform.GetChild(1).GetChild(3).gameObject;
        flour.SetActive(saveData.ingredientsCollected.Contains(Ingredient.FLOUR));
        GameObject milk = canvasRef.transform.GetChild(1).GetChild(4).gameObject;
        milk.SetActive(saveData.ingredientsCollected.Contains(Ingredient.MILK));
        GameObject sugar = canvasRef.transform.GetChild(1).GetChild(5).gameObject;
        sugar.SetActive(saveData.ingredientsCollected.Contains(Ingredient.SUGAR));

        GameObject mixer = canvasRef.transform.GetChild(1).GetChild(6).gameObject;
        mixer.SetActive(saveData.itemsCollected.Contains(Tool.MIXER));
        GameObject spatula = canvasRef.transform.GetChild(1).GetChild(7).gameObject;
        mixer.SetActive(saveData.itemsCollected.Contains(Tool.SPATULA));
    }

    public void SetNotEnoughItemsPopupActive(bool isActive) {
        GameObject notEnoughItems = canvasRef.transform.GetChild(2).gameObject;
        notEnoughItems.SetActive(isActive);
    }
}
