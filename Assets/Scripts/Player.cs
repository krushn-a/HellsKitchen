using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private bool isWalking;
    private void Update()
    {
        //Making a 2d vector to store input
        Vector2 inputVector = new Vector2(0,0);

        if (Input.GetKey(KeyCode.W)) 
        {
            inputVector.y += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x -= 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x += 1;
        }

        //Normalizing the vector to ensure consistent speed in all directions
        inputVector = inputVector.normalized;

        //projecting the 2d vector onto the 3d plane
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        //Checking if the player is walking
        isWalking = moveDir != Vector3.zero;

        //Moving the player in the direction of the input vector
        transform.position += moveDir * Time.deltaTime * moveSpeed;

        //Setting the forward direction of the player to face the movement direction
        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward,moveDir,Time.deltaTime*rotationSpeed);

    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
