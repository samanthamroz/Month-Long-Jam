using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PushableObject : InteractableObject
{
    public int hitboxYOffset = -1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Interaction(GameObject player)
    {
        float horizontalDifference = gameObject.transform.position.x - player.transform.position.x;
        float verticalDifference = gameObject.transform.position.y - (player.transform.position.y + hitboxYOffset);

        //Debug.Log("Horizontal: " + horizontalDifference + ", Vertical: " + verticalDifference);

        if (math.abs(horizontalDifference) > math.abs(verticalDifference)) {
            if (horizontalDifference < 0) //check for left
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, 0);
            } else { //check for right
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, 0);
            }
        } else {
            if (verticalDifference < 0) //check for top
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1, 0);
            }
            //check for bottom
            else
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, 0);
            }
        }
    }
}
