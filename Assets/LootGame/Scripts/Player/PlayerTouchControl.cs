using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class PlayerTouchControl : MonoBehaviour {

    Vector3 TargetPosition;
    bool ClickingObject;
    public int MaximumUseDistance = 3;
    Rigidbody rb;//OnlyNeccesary for WASD control
    public float WASDMovementspeed = 15;//OnlyNeccesary for WASD control
    public float WASDTurningspeed = 120;//OnlyNeccesary for WASD control
    public float SlowSpeed = 3;
    public float FastSpeed = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody>();//OnlyNeccesary for WASD control
    }

	void Update () {

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
                                if (i ==  hit.collider)
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

    private void MoveCharacter(Touch touch)
    {
        var playerPlane = new Plane(Vector3.up, transform.position);
        var ray = Camera.main.ScreenPointToRay(touch.position);
        float hitdist = 0.0f;

        if (playerPlane.Raycast(ray, out hitdist))
        {
            var targetPoint = ray.GetPoint(hitdist);
            TargetPosition = ray.GetPoint(hitdist);
            var targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            transform.rotation = targetRotation;

            float distance = Vector3.Distance(transform.position, TargetPosition);

            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, Time.deltaTime * ((distance < 5) ? SlowSpeed : FastSpeed));
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

    private void UseWASDControl()
    {
        float horizontal = Input.GetAxis("Horizontal") * WASDTurningspeed * Time.deltaTime;
        transform.Rotate(0, horizontal, 0);

        float vertical = Input.GetAxis("Vertical") * WASDMovementspeed * Time.deltaTime;
        transform.Translate(0, 0, vertical);

        if (Input.GetKeyDown(KeyCode.E))
        {
            UseObject();
        }
    }
}
