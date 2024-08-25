using UnityEngine;

public class BreakableWall : InteractableObject
{
    public override void Interaction(GameObject player) {
        Destroy(gameObject);
    }
}