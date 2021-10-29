using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public float playerSpeed = 10f;
    Rigidbody2D rb;
    Vector2 movememtVector;
    public Camera mainCam;
    Vector2 mousePosition;
    Vector2 lookPosition;
    float mouseAngle;
    private InputManager inputManager;
    private PlayerInput playerInput;
    private void Awake()
    {
        inputManager = new InputManager();
        inputManager.Enable();
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        inputManager.Player.Movement.performed += PlayerMovement;
    }
    private void OnEnable()
    {
        inputManager.Enable();  
    } 

    void Update()
    {
        /*movememtVector.x = Input.GetAxisRaw("Horizontal");
        movememtVector.y = Input.GetAxisRaw("Vertical");
        mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        lookPosition = mousePosition - rb.position;
        mouseAngle = Mathf.Atan2(lookPosition.y, lookPosition.x) * Mathf.Rad2Deg;*/
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movememtVector.normalized * playerSpeed * Time.fixedDeltaTime);
        rb.rotation = mouseAngle;
    }
    private void PlayerMovement(InputAction.CallbackContext callBackContext)
    {
        Debug.Log(callBackContext);
    }
}
