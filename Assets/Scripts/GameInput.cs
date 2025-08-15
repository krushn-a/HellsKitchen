using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private InputSystem_Actions PlayerInput;

    public event EventHandler OnInteraction;

    private void Awake()
    {
        PlayerInput = new InputSystem_Actions();
        PlayerInput.Player.Enable();

        PlayerInput.Player.Interactions.performed += Interactions_performed;
    }

    private void Interactions_performed(InputAction.CallbackContext obj)
    {
        //if(OnInteraction != null)
        //{
        //    OnInteraction(this, EventArgs.Empty);
        //}
        
        //short form of above code
        OnInteraction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GameInputNormalized() 
    {
        //Making a 2d vector to store input
        Vector2 inputVector = PlayerInput.Player.Move.ReadValue<Vector2>();

        //Normalizing the vector to ensure consistent speed in all directions
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
