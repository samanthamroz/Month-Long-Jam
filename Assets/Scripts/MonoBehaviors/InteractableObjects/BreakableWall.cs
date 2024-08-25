using UnityEngine;

public class BreakableWall : InteractableObject
{
    public override void Interaction(GameObject player) {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("coll");
        if (other.gameObject.tag == "Breaker") {
            Destroy(gameObject);
        }
    }
}