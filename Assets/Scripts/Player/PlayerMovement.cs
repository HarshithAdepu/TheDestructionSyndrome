using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed = 10f;

    Rigidbody2D rb;
    Vector2 movememtVector;
    InputManager inputManager;

    private void Awake()
    {
        inputManager = GameManager.instance.inputManagerInstance;
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

    private void Update()
    {
        movememtVector = inputManager.Player.Movement.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movememtVector.normalized * playerSpeed * Time.fixedDeltaTime);
    }
}
