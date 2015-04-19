using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class NewPlayerMove : MonoBehaviour
{

    int Smooth = 5;
    Vector3 TargetPosition;
    bool ClickingObject;
    public int MaximumUseDistance = 3;
    NavMeshAgent nav;
    Transform TouchPosition;
    Rigidbody rb;//OnlyNeccesary for WASD control
    public float Movementspeed = 15;//OnlyNeccesary for WASD control
    public float Turningspeed = 120;//OnlyNeccesary for WASD control

    void Start()
    {
        rb = GetComponent<Rigidbody>();//OnlyNeccesary for WASD control
        nav = GetComponent<NavMeshAgent>();

    }

    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                ClickingObject = false;

                RaycastHit hit = new RaycastHit();
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                var Player = GameObject.FindGameObjectWithTag("Player");


                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.GetComponent(typeof(IInteractable)))
                    {
                        var Objects = Physics.OverlapSphere(gameObject.transform.position, MaximumUseDistance);
                        foreach (var i in Objects)
                        {
                            if (i == hit.collider)
                            {
                                var Interactable = (IInteractable)hit.collider.GetComponent(typeof(IInteractable));

                                Interactable.Interact(gameObject);
                                ClickingObject = true;
                                return;
                            }
                        }
                    }
                }
            }

            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled && ClickingObject == false)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    //Stops character moving, when ui elements are pressed.
                    return;
                }
                else
                {
                    MoveCharacter(touch);
                }

            }
        }



        UseWASDControl();//OnlyNeccesaryForWASDcontrol, obviously
    }

    private void UseWASDControl()
    {
        float horizontal = Input.GetAxis("Horizontal") * Turningspeed * Time.deltaTime;
        transform.Rotate(0, horizontal, 0);

        float vertical = Input.GetAxis("Vertical") * Movementspeed * Time.deltaTime;
        transform.Translate(0, 0, vertical);

        if (Input.GetKeyDown(KeyCode.E))
        {
            UseObject();
        }
    }

    private void MoveCharacter(Touch touch)
    {
        var playerPlane = new Plane(Vector3.up, transform.position);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 0.0f;

        if (playerPlane.Raycast(ray, out hitdist))
        {
            var targetPoint = ray.GetPoint(hitdist);
            TargetPosition = ray.GetPoint(hitdist);
            //var targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            //transform.rotation = targetRotation;

            //transform.position = Vector3.MoveTowards(transform.position, TargetPosition, Time.deltaTime * Smooth);

            nav.SetDestination(TargetPosition);

        }
    }

    public void UseObject()
    {
        var Objects = Physics.OverlapSphere(transform.position, 3);
        foreach (var i in Objects)
        {
            var Interactable = (IInteractable)i.GetComponent(typeof(IInteractable));

            if (Interactable != null)
            {
                Interactable.Interact(gameObject);
                return;
            }
        }
    }
}
