using System;
using UnityEngine;
using UnityEngine.InputSystem;
//test
public class PlayerController : MonoBehaviour
{
    private InteractableObject currentInteraction;
    private Vector2 movementInput;
    public float speed;
    private bool canMove = true; //used to momentarily stop movement during minor events

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (canMove && movementInput != Vector2.zero) {
            gameObject.GetComponent<Rigidbody2D>().MovePosition(gameObject.transform.position + (new Vector3(movementInput.x, movementInput.y, 0) * speed * Time.deltaTime));
        }
    }

    private void OnMove(InputValue value) {
        movementInput = value.Get<Vector2>();
    }

    private void OnInteract() {
        if (currentInteraction != null) {
            currentInteraction.Interaction(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        try {
            currentInteraction = other.gameObject.GetComponent<InteractableObject>();
        } catch (Exception) {
            currentInteraction = null;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        currentInteraction = null;
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }

}