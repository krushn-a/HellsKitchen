using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private Animator Playeranimator;

    [SerializeField] private Player Player;

    private const string IS_WALKING = "IsWalking";
    private void Awake()
    {
        Playeranimator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if the player is walking and update the animator parameter
        Playeranimator.SetBool(IS_WALKING, Player.IsWalking());
    }
}

