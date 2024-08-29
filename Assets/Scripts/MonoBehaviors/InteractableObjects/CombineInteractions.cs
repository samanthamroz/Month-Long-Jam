using UnityEngine;
public class CombineInteractions : InteractableObject {
    public InteractableObject[] interactions;
    public override void Interaction(GameObject player)
    {
        foreach (InteractableObject i in interactions) {
            i.Interaction(player);
        }
    }
}