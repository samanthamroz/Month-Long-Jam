using UnityEngine;
using UnityEngine.InputSystem;
//test
public class PlayerController : MonoBehaviour
{
    private Vector2 movementInput;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (movementInput != Vector2.zero) {
            gameObject.transform.Translate(new Vector3(movementInput.x, movementInput.y, 0) * speed * Time.deltaTime);
        }
    }

    private void OnMove(InputValue value) {
        movementInput = value.Get<Vector2>();
    }

    private void OnInteract() {
        
    }
}