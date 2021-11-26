using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public InputManager inputManagerInstance;
    private void Awake()
    {
        instance = this;
        inputManagerInstance = new InputManager();
        inputManagerInstance.Enable();
    }
    public void PlayerHealthUpdated()
    {

    }

}
