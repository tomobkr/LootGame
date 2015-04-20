using UnityEngine;
using System.Collections;

public class WaypointEnter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
        else
        {
            var IWaypointHandled = (IWaypointHandler)other.GetComponent(typeof(IWaypointHandler));

            if (IWaypointHandled != null)
            {
                IWaypointHandled.IterateWaypoints(gameObject);
                return;
            }
        }
        
    }
}
