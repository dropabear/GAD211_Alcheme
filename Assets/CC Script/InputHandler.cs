using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public PlayerController CharacterController;
    private InputAction moveAction, lookAction;

    // Start is called before the first frame update
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movementVector = moveAction.ReadValue<Vector2>();
        CharacterController.Move(movementVector);

        Vector2 lookVector = lookAction.ReadValue<Vector2>();
        CharacterController.Rotate(lookVector);
    }
}
