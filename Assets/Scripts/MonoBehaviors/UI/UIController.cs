using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    public GameObject canvasPrefab, floatingCanvasPrefab;
    private GameObject canvasRef, floatingCanvasRef;
    public GameObject messageRef;
    private PlayerSaveData saveData { //read only
        get { return SaveManager.Load<PlayerSaveData>().saveData; }
    }

    void Awake()
    {
        canvasRef = Instantiate(canvasPrefab);
        floatingCanvasRef = Instantiate(floatingCanvasPrefab);
        messageRef = floatingCanvasRef.transform.GetChild(0).gameObject;
    }

    void Start()
    {
        floatingCanvasRef.GetComponent<Canvas>().worldCamera = GetComponent<PlayerController>().cam;
        SetInteractPopupActive(false);
    }

    public void SetInteractPopupActive(bool isActive, string text = "null") {
        GameObject popup;
        try {
            popup = canvasRef.transform.GetChild(0).gameObject;
        } catch {
            return;
        }

        popup.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = text;
        if (text != null) {
            popup.SetActive(isActive);
        }
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

    public IEnumerator StartCutscene() {
        Transform bars = canvasRef.transform.GetChild(2);
        LeanTween.moveLocalY(bars.GetChild(0).gameObject, 450, 0.2f).setEaseInBounce().setEaseOutSine();
        LeanTween.moveLocalY(bars.GetChild(1).gameObject, -550, 0.2f).setEaseInBounce().setEaseOutSine();
    
        yield return null;
    }

    public IEnumerator EndCutscene() {
        SetDialoguePopupActive(false);

        Transform bars = canvasRef.transform.GetChild(2);
        LeanTween.moveLocalY(bars.GetChild(0).gameObject, 575, 0.2f).setEaseInBounce().setEaseOutSine();
        LeanTween.moveLocalY(bars.GetChild(1).gameObject, -675, 0.2f).setEaseInBounce().setEaseOutSine();

        yield return null;
    }

    public void SetDialoguePopupActive(bool isActive, string text = null) {
        if (text != null) {
            messageRef.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = text;
        } else {
            messageRef.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
        }
        
        messageRef.SetActive(isActive);
    }
}
