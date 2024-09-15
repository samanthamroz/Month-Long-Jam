using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixerPickup : ToggleableObject
{
    public float hoverDistance, hoverSpeed;
    private float hoverTop, hoverBase;

    public override string HoverText()
    {
        return "Pick Up";
    }

    public override void Interaction(GameObject player)
    {
        var saveData = SaveManager.Load<PlayerSaveData>().saveData;
        saveData.itemsCollected.Add(Tool.MIXER);
        SaveManager.Save(new SaveProfile<PlayerSaveData>(saveData));

        gameObject.SetActive(false);

        Autosave.SaveRoom();
    }

    public override bool GetSaveState()
    {
        return gameObject.activeSelf;
    }

    public override void LoadFromSavedState(bool savedState)
    {
        if (!savedState) {
            gameObject.SetActive(false);
        }
    }

    void Awake()
    {
        hoverBase = gameObject.transform.position.y;
        hoverTop = hoverBase + hoverDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y >= hoverTop) {
            LeanTween.moveLocalY(gameObject, hoverBase, hoverSpeed).setEaseInOutSine();
        }
        if (gameObject.transform.position.y <= hoverBase) {
            LeanTween.moveLocalY(gameObject, hoverTop, hoverSpeed).setEaseInOutSine();
        }
    }
}
