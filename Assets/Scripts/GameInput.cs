using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private InputSystem_Actions PlayerInput;

    private void Awake()
    {
        PlayerInput = new InputSystem_Actions();
        PlayerInput.Player.Enable();
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
