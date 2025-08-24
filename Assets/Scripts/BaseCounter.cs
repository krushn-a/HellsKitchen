using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{

    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;
    public virtual void Interact(Player player)
    {
        // This method can be overridden in derived classes
        // to provide specific interaction behavior.
        Debug.Log("BaseCounter Interact called");
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
