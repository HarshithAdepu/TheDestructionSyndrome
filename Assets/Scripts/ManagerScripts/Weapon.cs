using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
[System.Serializable]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public float damage, spread, impactforce, coolDown, timeBetweenShots;
    public int ammo;
    public bool allowFireHold;
}
