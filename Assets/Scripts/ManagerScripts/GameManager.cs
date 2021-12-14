using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        inputManagerInstance.Player.CloseGame.performed += CloseGame;
    }
    void CloseGame(InputAction.CallbackContext ctx)
    {
        Debug.Log("Closing Application");
        Application.Quit();
    }
}
