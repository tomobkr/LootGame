using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour, IWaypointHandler {

    Transform player;
    public bool FollowingPlayer = false;
    NavMeshAgent nav;
    Queue<GameObject> WaypointQueue = new Queue<GameObject>();
    GameObject tmpGameObject;
    public Transform Waypoint1;
    public Transform Waypoint2;
    public Transform Waypoint3;
    public Transform Waypoint4;
    public Transform Waypoint5;
    public Transform Waypoint6;
    public Transform Waypoint7;
    public Transform Waypoint8;
    public Transform Waypoint9;
    public Transform Waypoint10;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        for (int i = 1; i < 10; i++)
        {
            tmpGameObject = GameObject.Find("Waypoint" + i);
            if (tmpGameObject != null)
          {
              WaypointQueue.Enqueue(tmpGameObject);
          }
        }

        tmpGameObject = WaypointQueue.Dequeue();
        nav.SetDestination(tmpGameObject.transform.position);
        WaypointQueue.Enqueue(tmpGameObject);
    }

    void update()
    {
            //TODO, make it so the player properly follows the player, when he is too close
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            nav.SetDestination(player.transform.position);
            //MakeEnemyDoStuff
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            nav.SetDestination(tmpGameObject.transform.position);
            //MakeEnemyDoOtherStuff
        }
    }

    public void IterateWaypoints(GameObject EnteredWaypoint)
    {
        if (EnteredWaypoint.name == tmpGameObject.name)
        {
            tmpGameObject = WaypointQueue.Dequeue();
            nav.SetDestination(tmpGameObject.transform.position);
            WaypointQueue.Enqueue(tmpGameObject);
        }
    }
}
