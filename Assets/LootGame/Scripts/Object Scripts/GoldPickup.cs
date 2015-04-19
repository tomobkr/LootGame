using UnityEngine;
using System.Collections;

public class GoldPickup : MonoBehaviour, IPickupable {

    public int GoldCount = 500;

    public void Pickup(GameObject player)
    {
        var PlayerStats = player.GetComponent<PlayerStats>();
        
        PlayerStats.GoldCount += GoldCount;

        Destroy(gameObject);
    }

}