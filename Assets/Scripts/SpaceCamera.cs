using UnityEngine;
using System.Collections;

public class SpaceCamera : MonoBehaviour
{
    public GameObject target;
    public float rotationSpeed;
    Vector3 offset;
	// Use this for initialization
	void Start ()
    {
        offset = target.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float h =  Input.GetAxis("Aim X") * rotationSpeed;
        float v = -Input.GetAxis("Aim Y") * rotationSpeed;

        target.transform.Rotate(v, h, 0);

        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = target.transform.position - (rotation * offset);

        transform.LookAt(target.transform);
    }
}
