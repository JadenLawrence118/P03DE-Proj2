using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public Camera triggeredCam;
    public Camera liveCam;
    public Camera camHolder;

    private void Awake()
    {
        liveCam = Camera.allCameras[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject PlayerCharacter = GameObject.FindGameObjectWithTag("Player");
        Collider PlayerCollider = PlayerCharacter.GetComponent<Collider>();

        if (other == PlayerCollider)
        {
            camHolder = liveCam;
            triggeredCam.GetComponent<Camera>().enabled = true;
            liveCam.GetComponent<Camera>().enabled = false;
            triggeredCam.GetComponent<AudioListener>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject PlayerCharacter = GameObject.FindGameObjectWithTag("Player");
        Collider PlayerCollider = PlayerCharacter.GetComponent<Collider>();

        if (other == PlayerCollider)
        {   
            liveCam = camHolder;
            liveCam.GetComponent<Camera>().enabled = true;
            triggeredCam.GetComponent<Camera>().enabled = false;
            liveCam.GetComponent<AudioListener>().enabled = true;
            camHolder = triggeredCam;
        }
    }
}
