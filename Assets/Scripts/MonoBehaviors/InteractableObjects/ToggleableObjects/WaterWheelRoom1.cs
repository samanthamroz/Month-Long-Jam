using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterWheelRoom1 : ToggleableObject
{
    private bool hasTurned;
    public float animationTime;
    public GameObject breakableWall, leftStill, rightStill, rightFlowing, pipeFlowing, autoLoadingZone;

    private bool wallBroken;

    void Awake()
    {
        wallBroken = !breakableWall.activeSelf;
    }

    public override bool GetSaveState()
    {
        return hasTurned;
    }

    public override string HoverText()
    {
        if (hasTurned) {
            return null;
        }
        return "Turn";
    }

    public override void Interaction(GameObject player)
    {
        if (hasTurned) {
            return;
        }
        
        LeanTween.rotateAround(gameObject, Vector3.back, 360, animationTime);

        StartCoroutine(player.GetComponent<PlayerController>().DoCutscene(animationTime));
        ActivateRoom();
        hasTurned = true;


        PlayerSaveData saveData;

        try {
            saveData = SaveManager.Load<PlayerSaveData>().saveData;
            //saveData.ingredientsCollected.Add(ingredient);
        } catch {
            saveData = new PlayerSaveData {
                lastScene = SceneManager.GetActiveScene().name,
                player = player.transform.position,
                itemsCollected = new List<Tool>(),
                //ingredientsCollected = new List<Ingredient>{ingredient}
            };
        }
        SaveManager.Save(new SaveProfile<PlayerSaveData>(saveData));
    }

    void Update()
    {
        if (!wallBroken && !breakableWall.activeSelf) {
            UpdateRoomAfterBreak();
            wallBroken = true;
        }
    }

    private void UpdateRoomAfterBreak() {
        rightStill.SetActive(false);
        rightFlowing.SetActive(true);
        LeanTween.moveY(rightFlowing, 0, animationTime);
        autoLoadingZone.SetActive(true);
    }

    private void ActivateRoom() {
        pipeFlowing.SetActive(true);
        leftStill.SetActive(true);
        LeanTween.moveY(leftStill, -1, animationTime);
        if (breakableWall.activeSelf) {
            rightStill.SetActive(true);
            LeanTween.moveY(rightStill, -1, animationTime);
        } else {
            rightFlowing.SetActive(true);
            LeanTween.moveY(rightFlowing, 0, animationTime);
        }
        
    }

    public override void LoadFromSavedState(bool savedState)
    {
        if (savedState) {
            animationTime = 0f;
            ActivateRoom();
        }
    }
}
