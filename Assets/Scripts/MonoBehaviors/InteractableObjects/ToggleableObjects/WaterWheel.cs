using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterWheel : ToggleableObject
{
    private bool hasTurned;
    public float animationTime;
    public List<GameObject> waterLayers;

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

        switch (SceneManager.GetActiveScene().name) {
            case "WaterRoom0":
                break;
            case "WaterRoom1":
                StartCoroutine(player.GetComponent<PlayerController>().DoCutscene(animationTime));
                ActivateRoom1();
                hasTurned = true;
                break;
            case "WaterRoom2":
                break;
            default:
                throw new System.NotImplementedException();
        }
    }

    private void ActivateRoom0() {

    }

    private void ActivateRoom1() {
        waterLayers[0].SetActive(true);
        waterLayers[1].SetActive(true);
        LeanTween.moveY(waterLayers[1], -1, animationTime);
        waterLayers[2].SetActive(true);
        LeanTween.moveY(waterLayers[2], -1, animationTime);
    }

    private void ActivateRoom2() {

    }

    public override void LoadFromSavedState(bool savedState)
    {
        if (savedState) {
            animationTime = 0f;
            switch (SceneManager.GetActiveScene().name) {
                case "WaterRoom0":
                    break;
                case "WaterRoom1":
                    ActivateRoom1();
                    break;
                case "WaterRoom2":
                    break;
                default:
                    throw new System.NotImplementedException();
            }
        }
    }
}
