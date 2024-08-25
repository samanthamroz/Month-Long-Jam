using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObject : InteractableObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Interaction(GameObject player)
    {
        //check for left
        if (player.transform.position.x < gameObject.transform.position.x)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + 1, 0, 0);
        }
        //check for right
        else if (player.transform.position.x > gameObject.transform.position.x)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - 1, 0, 0);
        }
        //check for top
        else if (player.transform.position.y > gameObject.transform.position.y)
        {
            gameObject.transform.position = new Vector3(0, gameObject.transform.position.y - 1, 0);
        }
        //check for bottom
        else if (player.transform.position.y < gameObject.transform.position.y)
        {
            gameObject.transform.position = new Vector3(0, gameObject.transform.position.y + 1, 0);
        }
    }

}
