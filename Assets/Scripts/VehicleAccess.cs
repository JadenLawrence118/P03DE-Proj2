using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAccess : MonoBehaviour
{
    private bool inVehicle = false;
    private FollowCamera cam;
    private Globals global;

    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowCamera>();
        global = GameObject.FindGameObjectWithTag("GameController").GetComponent<Globals>();
    }
    private void OnTriggerEnter(Collider other)
    {
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
        GameObject Miner = GameObject.FindGameObjectWithTag("Vehicle");

        if (Input.GetButtonUp("Interact") && inVehicle)
        {
            global.inVehicle = true;
            cam.TargetSwitch(GameObject.Find("MinerCameraTarget"));
        }
    }
}
