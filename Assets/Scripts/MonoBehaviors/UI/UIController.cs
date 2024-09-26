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
        GameObject mixer = canvasRef.transform.GetChild(1).GetChild(0).gameObject;
        bool haveMixer = saveData.ingredientsCollected.Contains(Ingredient.BAKINGPOWDER);
        mixer.SetActive(haveMixer);
    }

    public void SetNotEnoughItemsPopupActive(bool isActive) {
        GameObject notEnoughItems = canvasRef.transform.GetChild(2).gameObject;
        notEnoughItems.SetActive(isActive);
    }
}
