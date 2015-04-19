using UnityEngine;
using System.Collections;

public class PlayerPickup : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        var pickupable = (IPickupable) collider.GetComponent(typeof(IPickupable));
        if (pickupable == null)
            return;

        pickupable.Pickup(gameObject);
        UIUpdate();
    }

    void UIUpdate()
    {
        var UIUpdate = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIUpdate>();
        UIUpdate.GoldScoreUpdate();
    }
    
}
