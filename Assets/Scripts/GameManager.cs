using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static InputManager inputManagerInstance;
    private void Awake()
    {
        inputManagerInstance = new InputManager();
        inputManagerInstance.Enable();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
