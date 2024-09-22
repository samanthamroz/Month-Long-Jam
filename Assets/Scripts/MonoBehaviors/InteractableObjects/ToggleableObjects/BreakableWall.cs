using UnityEngine;

public class BreakableWall : ToggleableObject
{
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

    public override void Interaction(GameObject player) {
        var saveData = SaveManager.Load<PlayerSaveData>().saveData;
        if (saveData.itemsCollected.Contains(Tool.MIXER)) {
            gameObject.SetActive(false);
        }
    }

    public override string HoverText()
    {
        var saveData = SaveManager.Load<PlayerSaveData>().saveData;
        if (saveData.itemsCollected.Contains(Tool.MIXER)) {
            return "Break";
        } else {
            return "Missing Item";
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Breaker") {
            gameObject.SetActive(false);
        }
    }
}