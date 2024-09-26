using System;
using System.Collections.Generic;
using UnityEngine;

public class FlowingWater : MonoBehaviour
{
    public Vector2 directionVector;
    public float speed;
    private List<Rigidbody2D> floatingObjects;

    void Awake()
    {
        floatingObjects = new List<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        foreach (Rigidbody2D rb in floatingObjects) {
            rb.AddForce(speed * directionVector);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        try {
            floatingObjects.Add(other.gameObject.GetComponent<Rigidbody2D>());
        } catch { }
    }

    void OnTriggerExit2D(Collider2D other) {
        try {
            floatingObjects.Remove(other.gameObject.GetComponent<Rigidbody2D>());
        } catch { }
    }

    void OnCollision2DEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) {
            Vector2 reverseDirection = new Vector2(-gameObject.GetComponent<Rigidbody2D>().velocity.x, -gameObject.GetComponent<Rigidbody2D>().velocity.y);
            
            Vector3 targetPosition = new Vector3(gameObject.transform.position.x + 1 * Math.Sign(reverseDirection.x),
                gameObject.transform.position.y + 1 * Math.Sign(reverseDirection.y),
                gameObject.transform.position.z);
            
            LeanTween.moveLocal(gameObject, targetPosition, .2f).setEaseOutExpo();
            /*LeanTween.scale(gameObject, 
                new Vector3(transform.localScale.x + jumpHeight, transform.localScale.y + jumpHeight, transform.localScale.z + jumpHeight),
                jumpAnimationTime).setLoopPingPong().setLoopCount(2);*/
        }
    }
}
