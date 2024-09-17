using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
//test
public class PlayerController : MonoBehaviour
{
    private UIController uic;
    private Camera cam;
    private InteractableObject currentInteraction;
    private Vector2 movementInput;
    public GameObject cameraPrefab;
    public float movementSpeed;
    public float cameraDrag;
    public float characterDrag;
    public float jumpDistance;
    public float jumpHeight;
    public float jumpAnimationTime;
    private bool menuActive = false;
    void Awake()
    {
        Instantiate(cameraPrefab).GetComponent<Camera>().enabled = true;
        cam = Camera.main;
        cam.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);
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

    private void OnInteract(InputValue value) {
        float isHeld = value.Get<float>();
        if (isHeld == 1f && currentInteraction != null) {
            //button was just pressed
            currentInteraction.Interaction(gameObject);
        } else if (isHeld == 0f && currentInteraction != null) {
            //button was just unpressed
            currentInteraction.EndInteraction(gameObject);
        }
    }

    private void OnInteractHold(InputValue value) {
        float isHeld = value.Get<float>();
        if (isHeld == 1f && currentInteraction != null) {
            //button was just pressed
            try {
                PushableObject cast = (PushableObject)currentInteraction;
                cast.HoldInteraction(gameObject);
            } catch { }
        } else if (isHeld == 0f && currentInteraction != null) {
            //button was just unpressed
            currentInteraction.EndInteraction(gameObject);
        }
    }

    private void OnEscape(InputValue value) {
        float isPressed = value.Get<float>();
        if (isPressed == 1f && uic != null) {
            //button was just pressed
            menuActive = !menuActive;
            uic.SetPauseMenuActive(menuActive);
            if (menuActive == true) {
                StartCutscene();
            }
            else {
                EndCutscene();
            }
        }
        //next make it stop player movement i think but for now i am gonna work on save and quit functions first then come back
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

        if (other.CompareTag("Water")) 
        {
            Vector2 reverseDirection = new Vector2(-gameObject.GetComponent<Rigidbody2D>().velocity.x, -gameObject.GetComponent<Rigidbody2D>().velocity.y);
            
            Vector3 targetPosition = new Vector3(gameObject.transform.position.x + jumpDistance * Math.Sign(reverseDirection.x),
                gameObject.transform.position.y + jumpDistance * Math.Sign(reverseDirection.y),
                gameObject.transform.position.z);
            
            LeanTween.moveLocal(gameObject, targetPosition, jumpAnimationTime).setEaseOutExpo();
            /*LeanTween.scale(gameObject, 
                new Vector3(transform.localScale.x + jumpHeight, transform.localScale.y + jumpHeight, transform.localScale.z + jumpHeight),
                jumpAnimationTime).setLoopPingPong().setLoopCount(2);*/
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        OnTriggerEnter2D(other);
    }

    void OnTriggerExit2D(Collider2D other) {
        currentInteraction = null;
        uic.SetInteractPopupActive(false);
    }

    public IEnumerator DoCutscene(float cutsceneTime, bool disableHitbox = false)
    {
        StartCutscene();
        if (disableHitbox) {
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
        yield return new WaitForSeconds(cutsceneTime);
        gameObject.GetComponent<Collider2D>().enabled = true;
        EndCutscene();
    }

    public void StartCutscene() {
        GetComponent<PlayerInput>().SwitchCurrentActionMap("Cutscene");
    }

    public void EndCutscene() {
        GetComponent<PlayerInput>().SwitchCurrentActionMap("Ground");
    }
}