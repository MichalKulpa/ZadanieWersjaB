using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private UIController uIController;
    private Movement input;

    void Awake()
    {
        input = new Movement();
        input.OnFoot.LeftClick.performed += ctx => playerMovement.ClickToMove();
        input.OnFoot.Dash.performed += ctx => playerMovement.Dash();
        input.OnFoot.RightClick.performed += ctx => playerMovement.Teleport();
        input.OnFoot.Run.performed += ctx => playerMovement.Run();
    }

    void Update()
    {
        playerMovement.UpdateStamina();
        playerMovement.FaceTarget();
        playerMovement.UpdateSpeed();
        uIController.UpdateStaminaBar();
    }
    void OnEnable()
    { 
        input.Enable(); 
    }

    void OnDisable()
    {
        input.Disable(); 
    }

}
