using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FlowingWater : MonoBehaviour
{
    public enum Direction {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    public Direction direction;
    private Vector2 directionVector;
    public float speed;
    
    void Awake()
    {
        switch (direction) {
            case Direction.UP:
                directionVector = Vector2.up;
                break;
            case Direction.DOWN:
                directionVector = Vector2.down;
                break;
            case Direction.LEFT:
                directionVector = Vector2.left;
                break;
            case Direction.RIGHT:
                directionVector = Vector2.right;
                break;
        }

        floatingObjects = new List<Rigidbody2D>();
    }

    private List<Rigidbody2D> floatingObjects;

    void FixedUpdate()
    {
        foreach (Rigidbody2D rb in floatingObjects) {
            rb.AddForce(speed * directionVector);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("try0");
        try {
            Debug.Log("add");
            floatingObjects.Add(other.gameObject.GetComponent<Rigidbody2D>());
        } catch { }
    }

    void OnCollisionExit2D(Collision2D other) {
        try {
            floatingObjects.Remove(other.gameObject.GetComponent<Rigidbody2D>());
        } catch { }
    }
}
