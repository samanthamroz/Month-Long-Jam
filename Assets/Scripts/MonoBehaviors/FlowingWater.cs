using System;
using System.Collections.Generic;
using UnityEngine;

public class FlowingWater : MonoBehaviour
{
    public Vector2 directionVector;
    public float speed;
    private List<GameObject> floatingObjects;

    void Awake()
    {
        floatingObjects = new List<GameObject>();
    }

    void FixedUpdate()
    {
        foreach (GameObject obj in floatingObjects) {
            if (obj.layer == 3) {
                obj.GetComponent<Rigidbody2D>().AddForce(speed * directionVector);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        try {
            if (gameObject.GetComponent<Collider2D>().bounds.Contains(other.bounds.min)
            && gameObject.GetComponent<Collider2D>().bounds.Contains(other.bounds.max)) {
                floatingObjects.Add(other.gameObject);
                other.gameObject.layer = 3;
            }
        } catch { }
    }

    void OnTriggerStay2D(Collider2D other) {
        //if it isn't already floating, keep checking
        if (!floatingObjects.Contains(other.gameObject)) {
            OnTriggerEnter2D(other);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        try {
            floatingObjects.Remove(other.gameObject);
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
