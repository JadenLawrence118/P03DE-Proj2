using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpCameras : MonoBehaviour
{
    public Camera FollowCamera;
    public Camera StaticCamera; 
    // Start is called before the first frame update
    void Start()
    {
        GameObject PlayerCharacter = GameObject.FindGameObjectWithTag("Player");

        FollowCamera.enabled = true;
        StaticCamera.enabled = false;

        PlayerCharacter.GetComponent<AudioListener>().enabled = true;
        StaticCamera.GetComponent<AudioListener>().enabled = false;
    }
}
