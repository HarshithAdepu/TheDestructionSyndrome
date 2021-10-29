using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootBullet : MonoBehaviour
{
    public Transform gunBarrel;
    public float bulletSpeed;
    ObjectPooler objectPooler;
    public InputManager inputManager;
    private void Awake()
    {
        inputManager = new InputManager();
        inputManager.Player.Shoot.performed +=  Shoot;
    }
    void Start()
    {
        //Put shooting script in player manager
        objectPooler = ObjectPooler.objectPoolerInstance; 
    }

    void Update()
    {
        /*if(Input.GetMouseButtonDown(0))
            Shoot();*/
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            AudioManager.audioManagerInstance.PlaySound("Gunshot");
            objectPooler.SpawnObject("BulletHit", gunBarrel.position, gunBarrel.rotation);
            GameObject bullet = objectPooler.SpawnObject("Bullet", gunBarrel.position, gunBarrel.rotation);
            bullet.GetComponent<BulletBehaviour>().bulletDamage = 10f;
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            bulletRB.velocity = gunBarrel.up * bulletSpeed;
        }
    }
}
