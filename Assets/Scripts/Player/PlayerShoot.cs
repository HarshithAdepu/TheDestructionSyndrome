using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerShoot : MonoBehaviour
{
    public enum Weapon { Pistol, Shotgun, AR}

    public Weapon currentWeapon;

    Transform gunBarrel;

    ObjectPooler objectPooler;
    InputManager inputManager;



    void Awake()
    {
        inputManager = GameManager.instance.inputManagerInstance;
        gunBarrel = gameObject.transform.GetChild(0).GetComponent<Transform>();
        objectPooler = ObjectPooler.objectPoolerInstance;
        currentWeapon = Weapon.Pistol;
    }

    private void OnEnable()
    {
        inputManager.Player.Enable();
        inputManager.Player.Shoot.performed += Shoot;
    }

    private void OnDisable()
    {
        inputManager.Player.Disable();
        inputManager.Player.Shoot.performed -= Shoot;
    }
    void Shoot(InputAction.CallbackContext callBackContext)
    {
        ObjectPooler.objectPoolerInstance.SpawnObject("BulletHit", gunBarrel.position, gunBarrel.rotation);

        switch (currentWeapon)
        {
            case Weapon.Pistol:
                ShootPistol();
                break;
            case Weapon.Shotgun:
                ShootShotgun();
                break;
            case Weapon.AR:
                ShootAR();
                break;
        }
    }

    void ShootPistol()
    {
        AudioManager.audioManagerInstance.PlaySound("Gunshot");

        GameObject bullet = objectPooler.SpawnObject("Bullet", gunBarrel.position, gunBarrel.rotation);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.velocity = gunBarrel.up * bullet.GetComponent<BulletBehaviour>().bulletSpeed;
    }
    void ShootShotgun()
    {
        AudioManager.audioManagerInstance.PlaySound("Gunshot");

        GameObject bullet = objectPooler.SpawnObject("Bullet", gunBarrel.position, gunBarrel.rotation);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.velocity = gunBarrel.up * bullet.GetComponent<BulletBehaviour>().bulletSpeed;
    }
    void ShootAR()
    {
        AudioManager.audioManagerInstance.PlaySound("Gunshot");

        GameObject bullet = objectPooler.SpawnObject("Bullet", gunBarrel.position, gunBarrel.rotation);
    }
}
