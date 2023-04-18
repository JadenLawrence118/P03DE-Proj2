using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAccess : MonoBehaviour
{
    private bool inVehicle = false;

    private void OnTriggerEnter(Collider other)
    {
        print("hello");

        GameObject PlayerCharacter = GameObject.FindGameObjectWithTag("Player");
        Collider PlayerCollider = PlayerCharacter.GetComponent<Collider>();

        if (other == PlayerCollider)
        {
            inVehicle = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject PlayerCharacter = GameObject.FindGameObjectWithTag("Player");
        Collider PlayerCollider = PlayerCharacter.GetComponent<Collider>();

        if (other == PlayerCollider)
        {
            inVehicle = false;
        }
    }

    private void Update()
    {
        GameObject PlayerCharacter = GameObject.FindGameObjectWithTag("Player");
        GameObject Vehicle = GameObject.FindGameObjectWithTag("Vehicle");

        if (Input.GetButton("Interact") && inVehicle)
        {
            PlayerCharacter.SetActive(false);

        }
    }
}
