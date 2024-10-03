using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
//test
public class PlayerController : MonoBehaviour
{
    public bool spawnFromSave = true;
    private UIController uic;
    public Camera cam;
    private InteractableObject currentInteraction;
    private Vector2 movementInput;
    public GameObject cameraPrefab;
    public float movementSpeed;
    public float cameraDrag;
    private bool menuActive = false;
    Animator animator;
    public AudioSource source;
    public AudioClip footsteps;

    void Awake()
    {
        Instantiate(cameraPrefab).GetComponent<Camera>().enabled = true;
        cam = Camera.main;
        cam.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);
        uic = gameObject.GetComponent<UIController>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (spawnFromSave) {
            try {
                var saveData = SaveManager.Load<PlayerSaveData>().saveData;
                gameObject.transform.position = saveData.nextSpawn;
            } catch { 
                Debug.Log("spawning at default location");
            }
        }
        cam.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);
    }

    void FixedUpdate()
    {
        if (movementInput != Vector2.zero) {
            LeanTween.cancel(cam.gameObject);
            LeanTween.cancel(uic.messageRef);

            //move character
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(movementInput.x, movementInput.y) * movementSpeed;
            
            //move camera
            LeanTween.moveLocal(cam.gameObject, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10), cameraDrag).setEaseInBounce().setEaseOutSine();
            animator.SetFloat("moveX", movementInput.x);
            animator.SetFloat("moveY", movementInput.y);

            //move ui
            Vector3 wtsp = RectTransformUtility.WorldToScreenPoint(cam, gameObject.transform.position);
            Vector3 uiPos = new(wtsp.x, wtsp.y + 300, 0);
            LeanTween.move(uic.messageRef, uiPos, cameraDrag / 2);

            //sound
            if (!source.isPlaying)
            {
                source.clip = footsteps;
                source.Play();
            }
            
        } else {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            if (source.isPlaying)
            {
                source.Stop();
            }
        }
        UpdateAnimation();
    }

    void moveCharacter(Vector3 input) {
        Debug.Log(input);
        gameObject.GetComponent<Rigidbody2D>().MovePosition(input);
    }

    void UpdateAnimation() {
        if(movementInput != Vector2.zero)
        {
            animator.Play("Base Layer.MandyWalk");
        }
        else {
            animator.Play("Base Layer.MandyIdle");
        }
    }

    private bool isHolding;
    private void OnMove(InputValue value) {
        if (isHolding && value.Get<Vector2>() != Vector2.zero) {
            try {
                PushableObject cast = (PushableObject)currentInteraction;
                cast.HoldInteraction(gameObject, value.Get<Vector2>());
            } catch { }
        } else {
            movementInput = value.Get<Vector2>();
        }
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
            isHolding = true;
        } else if (isHeld == 0f) {
            //button was just unpressed
            isHolding = false;
            if (currentInteraction != null) {
                currentInteraction.EndInteraction(gameObject);
            }
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
        if (gameObject.GetComponent<Collider2D>().GetContacts(new ContactPoint2D[0]) == 0) {
            if (currentInteraction != null) {
                currentInteraction.EndInteraction(gameObject);
                currentInteraction = null;
            }
            uic.SetInteractPopupActive(false);
        }
    }

    public IEnumerator DoCutscene(float cutsceneTime, bool disableHitbox = false)
    {
        if (cutsceneTime != 0f) {
            StartCoroutine(uic.DoCutscene(cutsceneTime));
            StartCutscene();
            if (disableHitbox) {
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
            yield return new WaitForSeconds(cutsceneTime);
            gameObject.GetComponent<Collider2D>().enabled = true;
            EndCutscene();
        } else {
            yield return null;
        }
    }

    public void StartCutscene() {
        GetComponent<PlayerInput>().SwitchCurrentActionMap("Cutscene");
    }

    public void EndCutscene() {
        GetComponent<PlayerInput>().SwitchCurrentActionMap("Ground");
    }
}