using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RotatingPushableObject : InteractableObject
{
    public Collider2D clockwiseCollider1, clockwiseCollider2, counterClockwiseCollider1, counterClockwiseCollider2;

    public override void Interaction(GameObject player) {
        Collider2D playerCollider = player.GetComponent<Collider2D>();
        Rigidbody2D rb2D = gameObject.GetComponent<Rigidbody2D>();

        rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY; //unlock rotation

        if (clockwiseCollider1.IsTouching(playerCollider) || clockwiseCollider2.IsTouching(playerCollider)) {
            rb2D.MoveRotation(rb2D.rotation - 22.5f);
        } else if (counterClockwiseCollider1.IsTouching(playerCollider) || counterClockwiseCollider2.IsTouching(playerCollider)) {
            rb2D.MoveRotation(rb2D.rotation + 22.5f);
        }
        
        StartCoroutine(ReapplyConstraintsAfterDelay());
    }

    private IEnumerator ReapplyConstraintsAfterDelay()
    {
        // Wait for the next fixed frame (where physics calculations are done)
        yield return new WaitForFixedUpdate();

        // Reapply the constraints
        gameObject.GetComponent<Rigidbody2D>().constraints =  RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation; //relock x and y
    }
}