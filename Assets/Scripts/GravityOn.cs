using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityOn : MonoBehaviour
{
    private Rigidbody thisRigid;
    private void Start()
    {
        thisRigid = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyUp("g"))
        {
            thisRigid.useGravity = true;
        }
    }
}
