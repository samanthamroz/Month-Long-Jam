using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class RollableObject : InteractableObject
{
    public bool canBreakObjects;
    public int hitboxYOffset = -1;
    public float thrust = 5f;
    public PlayerController playerController;
    public AudioClip roll;

    public override string HoverText()
    {
        return "Push";
    }

    public override void Interaction(GameObject player)
    {
        float horizontalDifference = gameObject.transform.position.x - player.transform.position.x;
        float verticalDifference = gameObject.transform.position.y - (player.transform.position.y + hitboxYOffset);
        roll = Resources.Load<AudioClip>("stonepush");
        //Debug.Log("Horizontal: " + horizontalDifference + ", Vertical: " + verticalDifference);
        Vector3 direction;

        if (math.abs(horizontalDifference) > math.abs(verticalDifference)) {
            if (horizontalDifference < 0) //check for left
            {
                SoundFXManager.instance.PlaySoundFXClip(roll, transform, 1f);
                direction  = Vector3.left;

            } else { //check for right
                SoundFXManager.instance.PlaySoundFXClip(roll, transform, 1f);
                direction = Vector3.right;

            }
        } else {
            if (verticalDifference < 0) //check for top
            {
                SoundFXManager.instance.PlaySoundFXClip(roll, transform, 1f);
                direction = Vector3.down;

            }
            //check for bottom
            else
            {
                SoundFXManager.instance.PlaySoundFXClip(roll, transform, 1f);
                direction = Vector3.up;
            }
        }

        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation; //unlock x and y
        gameObject.GetComponent<Rigidbody2D>().velocity = direction * thrust;
        Debug.Log(gameObject.GetComponent<Rigidbody2D>().velocity);
        //take away player's movement
        playerController = player.GetComponent<PlayerController>();
        StartCoroutine(RollUntilHit());
    }

    private IEnumerator RollUntilHit()
    {
        playerController.StartCutscene();
        while (math.abs(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude) > 0.05f) {
            yield return new WaitForFixedUpdate();
        }
        gameObject.SetActive(false);
        playerController.EndCutscene();
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
