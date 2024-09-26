using System;
using System.Collections;
using UnityEngine;

public class PushableObject : TransformableObject
{
    public float hitboxYOffset = -1f;
    private string hoverText = "Grab";
    public int tileWidth;
    public float moveAnimationTime;

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
        hoverText = "Grab";
        player.GetComponent<UIController>().UpdateInteractPopupText(hoverText);
    }

    public override void HoldInteraction(GameObject player, Vector2 direction) {
        float horizontalDifference = gameObject.transform.position.x - player.transform.position.x;
        float verticalDifference = gameObject.transform.position.y - (player.transform.position.y + hitboxYOffset);
        Vector3 moveToPosition = gameObject.transform.position;
        
        if (Math.Abs(horizontalDifference) >= Math.Abs(verticalDifference) && direction.y == 0) {
            moveToPosition.x = moveToPosition.x + direction.x * tileWidth;
        } else if (Math.Abs(horizontalDifference) <= Math.Abs(verticalDifference) && direction.x == 0) {
            moveToPosition.y = moveToPosition.y + direction.y * tileWidth;
        }

        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation; //unlock x and y
        LeanTween.moveLocal(gameObject, moveToPosition, moveAnimationTime);
        
        StartCoroutine(player.GetComponent<PlayerController>().DoCutscene(moveAnimationTime));
        
        StartCoroutine(ReapplyConstraintsAfterDelay(moveAnimationTime));
    }

    private IEnumerator ReapplyConstraintsAfterDelay(float waitTime)
    {
        // Wait for the next fixed frame (where physics calculations are done)
        yield return new WaitForSeconds(waitTime);

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
