using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PushableObject : TransformableObject
{
    public int hitboxYOffset = -1;

    public override string HoverText()
    {
        return "Push";
    }

    public override void Interaction(GameObject player)
    {
        float horizontalDifference = gameObject.transform.position.x - player.transform.position.x;
        float verticalDifference = gameObject.transform.position.y - (player.transform.position.y + hitboxYOffset);

        //Debug.Log("Horizontal: " + horizontalDifference + ", Vertical: " + verticalDifference);
        Vector3 moveToPosition;

        if (math.abs(horizontalDifference) > math.abs(verticalDifference)) {
            if (horizontalDifference < 0) //check for left
            {
                moveToPosition = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, 0);
            } else { //check for right
                moveToPosition = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, 0);
            }
        } else {
            if (verticalDifference < 0) //check for top
            {
                moveToPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1, 0);
            }
            //check for bottom
            else
            {
                moveToPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, 0);
            }
        }

        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation; //unlock x and y
        gameObject.GetComponent<Rigidbody2D>().MovePosition(moveToPosition); //move (taking physics into accound)
        StartCoroutine(ReapplyConstraintsAfterDelay());
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
