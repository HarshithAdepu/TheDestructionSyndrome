using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] float playerSpeed = 10f;
    Rigidbody2D rb;
    Vector2 movememtVector;
    Vector2 aimingVector;
    Camera mainCam;
    Vector2 mousePosition;
    Vector2 lookDirection;
    float mouseAngle;
    Transform gunBarrel;
    ObjectPooler objectPooler;
    [SerializeField] float bulletSpeed;
    InputManager inputManager;
    Vector2 lastLookDirection;
    [SerializeField] float controllerDeadZone;
    private void Awake()
    {
        inputManager = GameManager.instance.inputManagerInstance;
        objectPooler = ObjectPooler.objectPoolerInstance;
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        inputManager.Player.Movement.performed += PlayerMovement;
        inputManager.Player.Shoot.performed += Shoot;
        gunBarrel = gameObject.transform.GetChild(0).GetComponent<Transform>();
    }
    private void OnEnable()
    {
        GameManager.instance.inputManagerInstance.Enable();  
    } 

    void Update()
    {
        movememtVector = inputManager.Player.Movement.ReadValue<Vector2>();
        aimingVector = inputManager.Player.Aiming.ReadValue<Vector2>();
        //mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        //lookDirection = mousePosition - rb.position;
        if (inputManager.Player.Aiming.ReadValue<Vector2>().magnitude < controllerDeadZone)
            lastLookDirection = movememtVector.magnitude > aimingVector.magnitude ? movememtVector : aimingVector;
        else
            lastLookDirection = aimingVector;
        lookDirection = lastLookDirection;
        mouseAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movememtVector.normalized * playerSpeed * Time.fixedDeltaTime);
        rb.rotation = mouseAngle;
    }
    private void PlayerMovement(InputAction.CallbackContext callBackContext)
    {
        movememtVector = callBackContext.ReadValue<Vector2>();
        Debug.Log(movememtVector);
        rb.AddForce(movememtVector, ForceMode2D.Force);
    }

    public void Shoot(InputAction.CallbackContext callBackContext)
    {
        AudioManager.audioManagerInstance.PlaySound("Gunshot");
        objectPooler.SpawnObject("BulletHit", gunBarrel.position, gunBarrel.rotation);
        GameObject bullet = objectPooler.SpawnObject("Bullet", gunBarrel.position, gunBarrel.rotation);
        bullet.GetComponent<BulletBehaviour>().bulletDamage = 10f;
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.velocity = gunBarrel.up * bulletSpeed;
    }
}
