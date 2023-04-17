using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowDoor : MonoBehaviour
{
    private Rigidbody hitMe;
    public float force = 30;
    public float torqueForce = 30;
    public Vector3 forceDirection = new Vector3(0, -1, 0);
    private void Start()
    {
        hitMe = this.GetComponent<Rigidbody>();
    }
    private void OnMouseDown()
    {
        hitMe.AddForce(forceDirection * force, ForceMode.Force);
        hitMe.AddTorque(this.transform.right * torqueForce);
    }
}