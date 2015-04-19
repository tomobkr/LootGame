using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float HeightFromObject = 10;
    public float HeightFromGround = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(target.position.x, target.position.y + HeightFromGround, target.position.z - HeightFromObject);
	}
}
