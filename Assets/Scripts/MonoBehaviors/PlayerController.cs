using System;
using UnityEngine;
using UnityEngine.InputSystem;
//test
public class PlayerController : MonoBehaviour
{
    private Camera cam;
    private InteractableObject currentInteraction;
    private Vector2 movementInput;
    public float movementSpeed;
    public float cameraDrag;
    public float characterDrag;
    private bool canMove = true; //used to momentarily stop movement during minor events

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    void FixedUpdate()
    {
        if (canMove && movementInput != Vector2.zero) {
            LeanTween.cancelAll();

            //move character
            //gameObject.GetComponent<Rigidbody2D>().MovePosition(gameObject.transform.position + (new Vector3(movementInput.x, movementInput.y, 0) * movementSpeed * Time.deltaTime));

            LeanTween.value(gameObject, moveCharacter, gameObject.transform.position, gameObject.transform.position + (new Vector3(movementInput.x, movementInput.y, 0) * movementSpeed * Time.deltaTime), characterDrag);


            //move camera
            LeanTween.moveLocal(cam.gameObject, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10), cameraDrag).setEaseInBounce().setEaseOutSine();
        }
        
        
    }

    void moveCharacter(Vector3 input) {
        Debug.Log(input);
        gameObject.GetComponent<Rigidbody2D>().MovePosition(input);
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