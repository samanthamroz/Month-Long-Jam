using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ToggleableObject : InteractableObject
{
    public abstract bool GetSaveState();
    public abstract void LoadFromSavedState(bool savedState);
}