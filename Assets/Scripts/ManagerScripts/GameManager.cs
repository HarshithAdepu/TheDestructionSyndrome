using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool AutoFire;
    public static GameManager instance;
    public InputManager inputManagerInstance;
    public Weapon currentWeapon;
    private void Awake()
    {
        instance = this;
        inputManagerInstance = new InputManager();
        inputManagerInstance.Enable();
    }
}
