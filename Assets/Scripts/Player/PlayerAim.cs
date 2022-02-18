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
    [SerializeField] Transform closestE;

    private void Start()
    {
       
    }

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
            lastLookDirection = (mousePosition - rb.position).normalized;

            aimingVector = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        }
        else if (playerInput.currentControlScheme == "Gamepad")
        {
            aimingVector = inputManager.Player.Aiming.ReadValue<Vector2>();
            //aimingVector = AssistedAim(aimingVector) - (Vector2)transform.position;
            if (aimingVector.magnitude > controllerDeadZone)
                lastLookDirection = aimingVector;
            else
                aimingVector = lastLookDirection;
            Debug.Log("Closest Guy: " + aimingVector);
        }

        lookAngle = Mathf.Atan2(aimingVector.y, aimingVector.x) * Mathf.Rad2Deg;
    }

    private void FixedUpdate()
    {
        rb.rotation = lookAngle;
    }

    Vector2 AssistedAim(Vector2 aimingVector)
    {
        List<Transform> visibleEntities = FieldOfView.fieldOfViewInstance.visibleEntities;
        if (visibleEntities.Count == 0)
            return aimingVector;

        int closestEnemyIndex = 0;
        float distance = int.MaxValue;
        for (int i = 0; i < visibleEntities.Count; i++)
        {
            if (Vector2.Distance(visibleEntities[i].position, transform.position) < distance)
            {
                distance = Vector2.Distance(visibleEntities[i].position, transform.position);
                closestEnemyIndex = i;
            }
        }
        return visibleEntities[closestEnemyIndex].position.normalized;
    }
}
