using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
//test
public class PlayerController : MonoBehaviour
{
    private UIController uic;
    private Camera cam;
    private InteractableObject currentInteraction;
    private Vector2 movementInput;
    public float movementSpeed;
    public float cameraDrag;
    public float characterDrag;

    void Awake()
    {
        cam = Camera.main;
        uic = gameObject.GetComponent<UIController>();
    }

    void FixedUpdate()
    {
        if (movementInput != Vector2.zero) {
            LeanTween.cancel(cam.gameObject);

            //move character
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(movementInput.x, movementInput.y) * movementSpeed;
            
            //move camera
            LeanTween.moveLocal(cam.gameObject, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10), cameraDrag).setEaseInBounce().setEaseOutSine();
        } else {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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
            uic.SetInteractPopupActive(true, currentInteraction.HoverText());
        } catch (Exception) {
            currentInteraction = null;
            uic.SetInteractPopupActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        currentInteraction = null;
        uic.SetInteractPopupActive(false);
    }

    public IEnumerator DoCutscene(float cutsceneTime)
    {
        StartCutscene();
        yield return new WaitForSeconds(cutsceneTime);
        EndCutscene();
    }

    public void StartCutscene() {
        GetComponent<PlayerInput>().SwitchCurrentActionMap("Cutscene");
    }

    public void EndCutscene() {
        GetComponent<PlayerInput>().SwitchCurrentActionMap("Ground");
    }
}