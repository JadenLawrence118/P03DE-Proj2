using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator anim;
    private HashIDs hash;
    private Globals global;
    private float elapsedTime = 0;
    private bool noBackMov = true;
    private float desiredDuration = 0.5f;

    public AudioClip shoutingClip;
    public float speedDampTime = 0.01f;
    public float sensitivityX = 1.0f;
    public float animationSpeed = 1.5f;
    public float pitchValue;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
        global = GameObject.FindGameObjectWithTag("GameController").GetComponent<Globals>();
        anim.SetLayerWeight(1, 1f);
    }

    void FixedUpdate()
    {
        if (!global.inVehicle)
        {
            float v = Input.GetAxis("Vertical");
            bool sneak = Input.GetButton("Sneak");
            bool sprint = Input.GetButton("Sprint");
            float turn = Input.GetAxis("Turn");
            Rotating(turn);
            MovementManagement(v, sneak, sprint);
            elapsedTime += Time.deltaTime;
        }
    }
    void Update()
    {
        if (!global.inVehicle)
        {
            bool shout = Input.GetButtonDown("Attract");
            anim.SetBool(hash.shoutingBool, shout);
            AudioManagement(shout);
        }
    }

    void Rotating(float mouseXInput)
    {
        //access the avatar's rigidbody
        Rigidbody ourBody = this.GetComponent<Rigidbody>();

        // check whether we have rotation data to apply
        if (mouseXInput != 0)
        {
            // use mouse input to create a Euler angle which provides rotation in the Y axis
            // this value is then turned into a Quarternion
            Quaternion deltaRotation = Quaternion.Euler(0f, mouseXInput * sensitivityX, 0f);
            //this value is applied to turn the body via the rigidbody
            ourBody.MoveRotation(ourBody.rotation * deltaRotation);
        }
    }

    void MovementManagement(float vertical, bool sneaking, bool sprinting)
    {
        anim.SetBool(hash.sneakingBool, sneaking);
        if (vertical > 0)
        {
            anim.SetFloat(hash.speedFloat, animationSpeed, speedDampTime, Time.deltaTime);
            anim.SetBool("Backwards", false);
            noBackMov = true;
        }
        if (vertical < 0)
        {
            if (noBackMov)
            {
                elapsedTime = 0;
                noBackMov = false;
            }
            
            float percentageComplete = elapsedTime / desiredDuration;

            anim.SetFloat(hash.speedFloat, -animationSpeed, speedDampTime, Time.deltaTime);
            anim.SetBool("Backwards", true);

            Rigidbody ourBody = this.GetComponent<Rigidbody>();
            ourBody.MoveRotation(ourBody.rotation);
            float movement = Mathf.Lerp(0f, -0.025f, percentageComplete);
            Vector3 moveBack = new Vector3(0f, 0f, movement);
            moveBack = ourBody.transform.TransformDirection(moveBack);
            ourBody.transform.position += moveBack;
        }

        if (vertical == 0)
        {
            anim.SetFloat(hash.speedFloat, 0);
            anim.SetBool(hash.backwardsBool, false);
            noBackMov = true;
        }

        if (sprinting)
        {
            anim.speed = 2;
        }
        else
        {
            anim.speed = 1;
        }
    }
    void AudioManagement(bool shout)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            if (!GetComponent<AudioSource>().isPlaying)
            { //not is playing rather than isNotPlaying
                GetComponent<AudioSource>().pitch = 0.27f;
                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            GetComponent<AudioSource>().Stop();
        }
        if (shout)
        {
            AudioSource.PlayClipAtPoint(shoutingClip, transform.position);
        }
    }
}
