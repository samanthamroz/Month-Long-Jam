using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public abstract void Interaction(GameObject player);

    public virtual void EndInteraction(GameObject player) {
        //do nothing, but can override if necessary
    }

    public virtual void HoldInteraction(GameObject player) {
        //do nothing, but can override if necessary
    }

    public abstract string HoverText();
}
