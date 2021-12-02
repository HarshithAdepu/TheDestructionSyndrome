using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
[System.Serializable]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public float speed, damage, spread, impactforce, coolDown, timeBetweenShots, camShakeIntensity, camShakeDuration;
    public int ammo, bulletsPerShot, ammoLeft;
    public bool allowFireHold;
    public AudioClip fireSFX;
}
