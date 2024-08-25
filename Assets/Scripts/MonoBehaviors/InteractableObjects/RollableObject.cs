using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RollableObject : InteractableObject
{
    public int hitboxYOffset = -1;
    public float thrust = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Interaction(GameObject player)
    {
        float horizontalDifference = gameObject.transform.position.x - player.transform.position.x;
        float verticalDifference = gameObject.transform.position.y - (player.transform.position.y + hitboxYOffset);

        //Debug.Log("Horizontal: " + horizontalDifference + ", Vertical: " + verticalDifference);
        Vector3 direction;

        if (math.abs(horizontalDifference) > math.abs(verticalDifference)) {
            if (horizontalDifference < 0) //check for left
            {
                direction  = Vector3.left;
            } else { //check for right
                direction = Vector3.right;
            }
        } else {
            if (verticalDifference < 0) //check for top
            {
                direction = Vector3.down;
            }
            //check for bottom
            else
            {
                direction = Vector3.up;
            }
        }

        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation; //unlock x and y
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction * thrust);
        //StartCoroutine(ReapplyConstraintsAfterDelay());

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
