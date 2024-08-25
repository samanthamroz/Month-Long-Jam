using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RotatingPushableObject : InteractableObject
{
    private Vector3 grabPointTop = new Vector3(0, 1, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Interaction(GameObject player) {
        if (player.transform.position.x < grabPointTop.x) { //player on left
            gameObject.transform.rotation = Quaternion.Euler(0, 0, gameObject.transform.rotation.z + 45);
        } else if (player.transform.position.x > grabPointTop.x) { //player on right
            gameObject.transform.rotation = Quaternion.Euler(0, 0, gameObject.transform.rotation.z - 45);
        }
    }
}