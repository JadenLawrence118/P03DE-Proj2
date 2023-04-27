using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAccess : MonoBehaviour
{
    private bool inVehicleArea = false;
    private FollowCamera cam;
    private Globals global;
    private GameObject PlayerCharacter;

    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowCamera>();
        global = GameObject.FindGameObjectWithTag("GameController").GetComponent<Globals>();
        PlayerCharacter = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        Collider PlayerCollider = PlayerCharacter.GetComponent<Collider>();

        if (other == PlayerCollider)
        {
            inVehicleArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Collider PlayerCollider = PlayerCharacter.GetComponent<Collider>();

        if (other == PlayerCollider)
        {
            inVehicleArea = false;
        }
    }

    private void Update()
    {
        GameObject Miner = GameObject.FindGameObjectWithTag("Vehicle");

        if (Input.GetButtonUp("Interact") && inVehicleArea && !global.inVehicle)
        {
            global.inVehicle = true;
            cam.TargetSwitch(GameObject.Find("MinerCameraTarget"));
            PlayerCharacter.SetActive(false);
        }

        else if (Input.GetButtonUp("Interact") && global.inVehicle)
        {
            global.inVehicle = false;
            PlayerCharacter.SetActive(true);
            cam.TargetSwitch(GameObject.Find("PlayerCameraTarget"));
            PlayerCharacter.transform.position = new Vector3(Miner.transform.position.x + 5, Miner.transform.position.y + 1, Miner.transform.position.z + 5);
        }
    }
}
