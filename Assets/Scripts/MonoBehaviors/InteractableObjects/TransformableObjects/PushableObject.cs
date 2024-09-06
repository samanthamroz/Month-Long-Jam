using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PushableObject : TransformableObject
{
    public float hitboxYOffset = -1f;
    private string hoverText = "Grab";

    public override string HoverText()
    {
        return hoverText;
    }

    public override void Interaction(GameObject player)
    {
        hoverText = "Push";
        player.GetComponent<UIController>().UpdateInteractPopupText(hoverText);
    }

    public override void EndInteraction(GameObject player) {
        isHolding = false;
        hoverText = "Grab";
        player.GetComponent<UIController>().UpdateInteractPopupText(hoverText);
    }

    public override void HoldInteraction(GameObject player) {
        isHolding = true;
        this.player = player;
        horizontalDifference = gameObject.transform.position.x - player.transform.position.x;
        verticalDifference = gameObject.transform.position.y - (player.transform.position.y + hitboxYOffset);
    }

    GameObject player;
    bool isHolding = false;
    float horizontalDifference, verticalDifference;
    void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation; //relock x and y
        if (isHolding) {
            Vector3 moveToPosition = new Vector3(
                player.transform.position.x + horizontalDifference, 
                player.transform.position.y + verticalDifference,
                0);

            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation; //unlock x and y
            gameObject.GetComponent<Rigidbody2D>().MovePosition(moveToPosition); //move (taking physics into accound)
        }
    }

    private IEnumerator ReapplyConstraintsAfterDelay()
    {
        // Wait for the next fixed frame (where physics calculations are done)
        yield return new WaitForFixedUpdate();

        // Reapply the constraints
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation; //relock x and y
    }

    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Player") {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag != "Player") {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
    }
    
}
