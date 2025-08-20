using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private ClearCounter clearCounter;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetClearCounter(ClearCounter clearcounter) 
    {
        //first we clear the previous ClearCounter if it exists
        if (this.clearCounter != null) 
        {
            this.clearCounter.ClearKitchenObject();
        }
        //then we set the new ClearCounter
        this.clearCounter = clearcounter;

        if (clearCounter.HasKitchenObject()) 
        { 
            Debug.LogError("ClearCounter already has a KitchenObject! Cannot set a new one.");
        }
        //and we set the parent of this KitchenObject to the ClearCounter's follow transform
        clearcounter.SetKitchenObject(this);

        transform.parent = clearcounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }
}
