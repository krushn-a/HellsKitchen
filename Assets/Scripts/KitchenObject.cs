using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) 
    {
        //first we clear the previous ClearCounter if it exists
        if (this.kitchenObjectParent != null) 
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        //then we set the new ClearCounter
        this.kitchenObjectParent = kitchenObjectParent;

        if (this.kitchenObjectParent.HasKitchenObject()) 
        {
            Debug.LogError("kitchenObjectParent already has a KitchenObject! Cannot set a new one.");
        }
        //and we set the parent of this KitchenObject to the ClearCounter's follow transform
        kitchenObjectParent.SetKitchenObject(this);

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }
}
