using UnityEngine;

public class TestStateObject : ToggleableObject
{
    private bool saveState;
    public override bool GetSaveState()
    {
        return saveState;
    }
    public override void LoadFromSavedState(bool savedState)
    {
        if (savedState) {
            gameObject.transform.localScale = new Vector3(5,5,5);
        } else {
            gameObject.transform.localScale = Vector3.one;
        }
    }
    public override void Interaction(GameObject player) {
        saveState = !saveState;
        LoadFromSavedState(saveState);
    }
}