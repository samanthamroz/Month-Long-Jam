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
        gameObject.SetActive(false);
    }

    public override string HoverText()
    {
        return "Break";
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Breaker") {
            gameObject.SetActive(false);
        }
    }
}