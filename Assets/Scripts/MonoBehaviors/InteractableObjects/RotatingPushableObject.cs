using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RotatingPushableObject : InteractableObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Interaction(GameObject player) {
        if (player.transform.position.x < gameObject.transform.position.x) { //player on left
            gameObject.transform.rotation = Quaternion.Euler(0, 0, gameObject.transform.rotation.z - 45);
        } else if (player.transform.position.x > gameObject.transform.position.x) { //player on right
            gameObject.transform.rotation = Quaternion.Euler(0, 0, gameObject.transform.rotation.z + 45);
        }
    }
}