using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    public float Movementspeed = 15;
    public float Turningspeed = 120;
    public float Jumpforce = 250;
    public float DistToGround;
    RaycastHit hit;
    Rigidbody rb;

	void Start () 
    {
        rb = GetComponent<Rigidbody>();
	}
	
	void Update () 
    {
        float horizontal = Input.GetAxis("Horizontal") * Turningspeed * Time.deltaTime;
        transform.Rotate(0, horizontal, 0);

        float vertical = Input.GetAxis("Vertical") * Movementspeed * Time.deltaTime;
        transform.Translate(0, 0, vertical);

        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            DistToGround = hit.distance;
        }

        if (Input.GetKeyDown(KeyCode.Space) && (hit.distance <= .51))
        {
            rb.AddForce(Vector3.up * Jumpforce);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            UseObject(); 
        }
	}

    private void UseObject()
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
