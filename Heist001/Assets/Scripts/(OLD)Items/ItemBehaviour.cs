using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public ItemDataSO itemData;

    public event System.Action Pickup;
    public event System.Action Drop;

    public void PickupItem() {
        Pickup?.Invoke();
    }

    public void DropItem() {
        Drop?.Invoke();
    }
}
