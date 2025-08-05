using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameInput gameInput;
    private bool isWalking;
    private void Update()
    {
        Vector2 inputVector = gameInput.GameInputNormalized();
        //projecting the 2d vector onto the 3d plane
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        //Checking if the player is walking
        isWalking = moveDir != Vector3.zero;

        float moveDistance = moveSpeed * Time.deltaTime;
        float PlayerHeight = 2f;
        float PlayerRadius = 0.7f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHeight, PlayerRadius, moveDir, moveDistance);

        if (!canMove) 
        {
            //cannot mave towars moveDir

            //Attempt to move Only on X
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHeight, PlayerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                //Can move only on X
                moveDir = moveDirX;
            }
            else 
            {
                //Cannot move only on the X


                //Attempt only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHeight, PlayerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else 
                {
                    //Cannot move in any direction
                }
            }
        }

        if (canMove) 
        {
            //Moving the player in the direction of the input vector
            transform.position += moveSpeed * Time.deltaTime * moveDir;
        }

        //Setting the forward direction of the player to face the movement direction
        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward,moveDir,Time.deltaTime*rotationSpeed);

    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
