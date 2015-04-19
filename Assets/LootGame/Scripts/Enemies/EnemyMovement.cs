using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    Transform player;
    NavMeshAgent nav;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
    }

	void Update () {
        nav.SetDestination(player.position);
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //MakeEnemyDoStuff
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //MakeEnemyDoOtherStuff
        }
    }
}
