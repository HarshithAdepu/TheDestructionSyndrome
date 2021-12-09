using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerWeaponHandler : MonoBehaviour
{
    float speed, damage, spread, impactforce, coolDown, reloadTime, timeBetweenShots, camShakeIntensity, camShakeDuration;
    int ammo, weaponIndex, bulletsPerShot, ammoLeft, shotcount;
    bool allowFireHold, shooting, readyToShoot, buttonHeldDown;
    float lastFired;
    [SerializeField] Text ammoCount;
    [SerializeField] Text weaponText;
    [SerializeField] Transform shootPoint;
    [SerializeField] Weapon currentWeapon;
    [SerializeField] List<Weapon> weapons;
    InputManager inputManager;

    void Awake()
    {
        lastFired = Time.time;

        weaponIndex = 0;
        currentWeapon = weapons[weaponIndex];

        GameManager.instance.currentWeapon = currentWeapon;

        coolDown = currentWeapon.coolDown;
        timeBetweenShots = currentWeapon.timeBetweenShots;
        ammo = currentWeapon.ammo;
        allowFireHold = currentWeapon.allowFireHold;
        bulletsPerShot = currentWeapon.bulletsPerShot;
        camShakeDuration = currentWeapon.camShakeDuration;
        camShakeIntensity = currentWeapon.camShakeIntensity;
        gameObject.GetComponent<AudioSource>().clip = currentWeapon.fireSFX;
        foreach (Weapon w in weapons)
        {
            w.ammoLeft = w.ammo;
        }
        ammoLeft = currentWeapon.ammoLeft;

        inputManager = GameManager.instance.inputManagerInstance;
        inputManager.Player.WeaponSwitching.performed += ctx => WeaponSwitch(ctx);
        readyToShoot = true;

        weaponText.text = currentWeapon.name;
        if (currentWeapon.ammoLeft >= 0)
            ammoCount.text = "Ammo: " + currentWeapon.ammoLeft + "/" + currentWeapon.ammo;
        else ammoCount.text = "Ammo: ∞";
    }

    void Update()
    {
        shooting = inputManager.Player.Shoot.ReadValue<float>() > 0;
        buttonHeldDown = inputManager.Player.Shoot.ReadValue<float>() > 0;

        if (shooting && readyToShoot)
        {
            if (currentWeapon.bulletsPerShot > 1)
            {
                for (int i = 0; i < currentWeapon.bulletsPerShot; i++)
                {
                    Invoke("Shoot", currentWeapon.timeBetweenShots * i);
                }
                if (currentWeapon.ammoLeft > 0)
                    currentWeapon.ammoLeft--;
            }
            else
            {
                Shoot();
                if (currentWeapon.ammoLeft > 0)
                    currentWeapon.ammoLeft--;
            }

            if (currentWeapon.ammoLeft >= 0)
                ammoCount.text = "Ammo: " + currentWeapon.ammoLeft + "/" + currentWeapon.ammo;
            else ammoCount.text = "Ammo: ∞";
        }

        if (!currentWeapon.allowFireHold && buttonHeldDown)
            readyToShoot = false;
        else if (!currentWeapon.allowFireHold && Time.time > lastFired + currentWeapon.coolDown || currentWeapon.allowFireHold && Time.time > lastFired + currentWeapon.coolDown)
            readyToShoot = true;

    }

    void WeaponSwitch(InputAction.CallbackContext context)
    {
        lastFired = Time.time;
        float value = context.ReadValue<float>();
        if (value > 0)
            weaponIndex++;
        else if (value < 0)
            weaponIndex += (weapons.Count - 1);
        currentWeapon = weapons[weaponIndex % weapons.Count];
        GameManager.instance.currentWeapon = currentWeapon;
        Debug.Log("Current Weapon: " + currentWeapon);

        coolDown = currentWeapon.coolDown;
        timeBetweenShots = currentWeapon.timeBetweenShots;
        ammoLeft = currentWeapon.ammoLeft;
        allowFireHold = currentWeapon.allowFireHold;
        bulletsPerShot = currentWeapon.bulletsPerShot;
        camShakeDuration = currentWeapon.camShakeDuration;
        camShakeIntensity = currentWeapon.camShakeIntensity;
        gameObject.GetComponent<AudioSource>().clip = currentWeapon.fireSFX;

        if (currentWeapon.ammoLeft >= 0)
            ammoCount.text = "Ammo: " + currentWeapon.ammoLeft + "/" + currentWeapon.ammo;
        else ammoCount.text = "Ammo: ∞";

        weaponText.text = currentWeapon.name;
    }

    public void Shoot()
    {
        CameraShake.cameraShakeInstance.ShakeCam(camShakeIntensity, camShakeDuration);
        lastFired = Time.time;

        if (currentWeapon.ammoLeft == 0)
        {
            AudioManager.audioManagerInstance.PlaySound("ShootFail");
            return;
        }

        readyToShoot = false;
        ObjectPooler.objectPoolerInstance.SpawnObject("BulletHit", shootPoint.position, shootPoint.rotation);
        GameObject bullet = ObjectPooler.objectPoolerInstance.SpawnObject("Bullet", shootPoint.position, shootPoint.rotation);
        AudioManager.audioManagerInstance.PlaySound("Gunshot");

    }
}
