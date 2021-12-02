using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myweapons : MonoBehaviour
{
    public Weapon weapon;
    public float speed, damage, coolDown;

    void Awake()
    {
        speed = weapon.speed;
        damage = weapon.damage;
        coolDown = weapon.coolDown;
    }
}
