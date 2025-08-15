using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask CounterLayerMask;
    
    private bool isWalking;
    private Vector3 LastInteraction;
    private ClearCounter selectedCounter;

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    
    //passing ClearCounter as a parameter to the event args class
    //basically we are passing some extra data
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        //this is the ClearCounter that is currently selected by the player
        public ClearCounter SelectedCounter;
    }

    private void Awake()
    {
        if (Instance != null) 
        {
            Debug.LogError("More than one Player instance in the scene!");
        }
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteraction += GameInput_OnInteraction;
    }

    private void GameInput_OnInteraction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null) 
        {
            selectedCounter.Interact();
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GameInputNormalized();
        //projecting the 2d vector onto the 3d plane
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            LastInteraction = moveDir;
        }

        float interactionDistance = 2f;
        if (Physics.Raycast(transform.position, LastInteraction, out RaycastHit raycasthit, interactionDistance,CounterLayerMask))
        {
            if (raycasthit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if (clearCounter != selectedCounter)
                {
                    //when the raycast hits ClearCounter and it is not the same as the previously selected counter
                    setSelectedCounter(clearCounter);
                }
            }
            else 
            {
                //when the raycast hits something other than ClearCounter
                setSelectedCounter(null);
            }
        }
        else 
        {
            //when the raycast does not hit any ClearCounter
            setSelectedCounter(null);
        }
    }
    private void HandleMovement() 
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
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }

    private void setSelectedCounter(ClearCounter selectedCounter) 
    {
        this.selectedCounter = selectedCounter;

        //Triggering the event to notify subscribers that the selected counter has changed
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            SelectedCounter = selectedCounter
        });
    }
}
