using UnityEngine;
public class CombineInteractions : InteractableObject {
    public InteractableObject[] interactions;

    public override string HoverText()
    {
        throw new System.NotImplementedException();
    }

    public override void Interaction(GameObject player)
    {
        foreach (InteractableObject i in interactions) {
            i.Interaction(player);
        }
    }
}