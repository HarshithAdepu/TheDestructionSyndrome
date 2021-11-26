using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponHandler : MonoBehaviour
{
    float damage, spread, impactforce, coolDown, reloadTime, timeBetweenShots;
    public int ammo;
    public bool allowFireHold, shooting, readyToShoot;
    [SerializeField] Transform shootPoint;
    //[SerializeField] Dictionary<Weapon, WeaponClass> weapons;
    [SerializeField] Weapon currentWeapon;
    [SerializeField] List<Weapon> weapons;

    void Awake()
    {
        currentWeapon = weapons.Find(w => w.weaponName == "Pistol");
        GameManager.instance.inputManagerInstance.Player.WeaponSwitching.performed += ctx => WeaponSwitch(ctx);

    }
    void WeaponSwitch(InputAction.CallbackContext context)
    {
        // damage = currentWeapon.damage;
        // spread = currentWeapon.spread;
        // impactforce = currentWeapon.impactforce;
        // coolDown = currentWeapon.coolDown;
        // timeBetweenShots = currentWeapon.timeBetweenShots;
        // ammo = currentWeapon.ammo;
        // allowFireHold = currentWeapon.allowFireHold;

        // Debug.Log("Current Weapon: " + currentWeapon);
    }

}
