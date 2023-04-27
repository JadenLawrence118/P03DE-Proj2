using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Globals global;

    public GameObject target;
    public Vector3 offset;

    private float mouseX;
    private float mouseY;
    private float mouseZ;

    private void Start()
    {
        offset = transform.position - target.transform.position;
        global = GameObject.FindGameObjectWithTag("GameController").GetComponent<Globals>();
    }
    private void LateUpdate()
    {
        float angleBetween = Vector3.Angle(Vector3.up, transform.forward);
        float desiredAngle = target.transform.eulerAngles.y;
        float dist = Vector3.Distance(target.transform.position, transform.position);
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = target.transform.position + (rotation * offset);
        transform.LookAt(target.transform);


        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        mouseZ = Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetMouseButton(1))
        {
            offset = Quaternion.Euler(0, mouseX, 0) * offset;
        }


        if (Input.GetMouseButton(0))
        {
            if (((angleBetween > 90) && (mouseY < 0)) || ((angleBetween < 140) && (mouseY > 0)))
            {
                Vector3 LocalRight = target.transform.worldToLocalMatrix.MultiplyVector(transform.right);
                offset = Quaternion.AngleAxis(mouseY, LocalRight) * offset;
            }
        }

        if (mouseZ < 0)
        {
            if (!global.inVehicle && dist < 10)
            {
                offset = Vector3.Scale(offset, new Vector3(1.05f, 1.05f, 1.05f));
            }
            else if (global.inVehicle && dist < 20)
            {
                offset = Vector3.Scale(offset, new Vector3(1.05f, 1.05f, 1.05f));
            }
        }
        if (mouseZ > 0)
        {
            if (dist > 0.6)
            {
                offset = Vector3.Scale(offset, new Vector3(0.95f, 0.95f, 0.95f));
            }
        }
    }
    public void TargetSwitch(GameObject newTarget)
    {
        target = newTarget;
    }
}
