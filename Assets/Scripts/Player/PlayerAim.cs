using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerAim : MonoBehaviour
{
    Rigidbody2D rb;

    PlayerInput playerInput;
    InputManager inputManager;

    Camera mainCam;
    Vector2 aimingVector;
    Vector2 mousePosition;
    Vector2 lookDirection;
    float lookAngle;
    Vector2 lastLookDirection = Vector2.zero;
    [SerializeField] float controllerDeadZone;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        inputManager = GameManager.instance.inputManagerInstance;
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        inputManager.Player.Enable();
    }

    private void OnDisable()
    {
        inputManager.Player.Disable();
    }

    void Update()
    {
        if (playerInput.currentControlScheme == "Keyboard and Mouse")
        {
            mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
            lastLookDirection = (mousePosition - rb.position).normalized;
        }
        else if (playerInput.currentControlScheme == "Gamepad")
        {
            aimingVector = inputManager.Player.Aiming.ReadValue<Vector2>();
            aimingVector = AssistedAim();
            if (aimingVector.magnitude > controllerDeadZone)
                lastLookDirection = aimingVector;
        }

        lookDirection = lastLookDirection;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
    }

    private void FixedUpdate()
    {
        rb.rotation = lookAngle;
    }

    Vector2 AssistedAim()
    {
        return Vector2.zero;
    }
}
