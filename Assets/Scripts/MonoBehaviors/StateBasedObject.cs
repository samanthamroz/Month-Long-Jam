using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBasedObject : InteractableObject
{
    public abstract override void Interaction(GameObject player);
    public Transform saveTransform {get {return gameObject.transform;} }
}
