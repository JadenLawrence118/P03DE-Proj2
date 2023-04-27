using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerMovement : MonoBehaviour
{
    private Animator anim;
    private HashIDs hash;
    private float elapsedTime = 0;
    private bool noBackMov = true;
    private float desiredDuration = 0.5f;
    bool minerOn = false;

    public AudioClip shoutingClip;
    public float speedDampTime = 0.01f;
    public float sensitivityX = 1.0f;
    public float animationSpeed = 1.5f;
    public float pitchValue;
    private Globals global;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
        anim.SetLayerWeight(1, 1f);
        global = GameObject.FindGameObjectWithTag("GameController").GetComponent<Globals>();
    }

    void FixedUpdate()
    {
        if (global.inVehicle)
        {
            float v = Input.GetAxis("Vertical");
            float turn = Input.GetAxis("Turn");
            Rotating(turn);
            MovementManagement(v);
            elapsedTime += Time.deltaTime;
        }
    }
    void Update()
    {
        if (global.inVehicle)
        {
            bool shout = Input.GetButtonDown("Attract");
            anim.SetBool(hash.shoutingBool, shout);

            if (Input.GetButtonDown("Spin Up"))
            {
                minerOn = true;
            }
            else if (Input.GetButtonDown("Spin Down"))
            {
                minerOn = false;
            }
            MineManagement(minerOn);
        }
    }

    void Rotating(float turn)
    {
        //access the avatar's rigidbody
        Rigidbody ourBody = this.GetComponent<Rigidbody>();

        // check whether we have rotation data to apply
        if (turn != 0)
        {
            // use mouse input to create a Euler angle which provides rotation in the Y axis
            // this value is then turned into a Quarternion
            Quaternion deltaRotation = Quaternion.Euler(0f, turn * sensitivityX, 0f);
            //this value is applied to turn the body via the rigidbody
            ourBody.MoveRotation(ourBody.rotation * deltaRotation);
        }

        if (turn > 0)
        {
            anim.SetBool("Right Forward", false);
            anim.SetBool("Right Back", true);
            anim.SetBool("Left Back", false);
            anim.SetBool("Left Forward", true);
            anim.SetFloat("SpeedF", 1);
            anim.SetFloat("SpeedB", -1);
        }
        else if (turn < 0)
        {
            anim.SetBool("Right Back", false);
            anim.SetBool("Right Forward", true);
            anim.SetBool("Left Forward", false);
            anim.SetBool("Left Back", true);
            anim.SetFloat("SpeedF", 1);
            anim.SetFloat("SpeedB", -1);
        }
    }

    void MovementManagement(float vertical)
    {
        if (vertical > 0)
        {
            anim.SetFloat("SpeedF", 1);
            anim.SetFloat("SpeedB", -1);
            anim.SetFloat(hash.speedFloat, animationSpeed, speedDampTime, Time.deltaTime);
            noBackMov = true;
            anim.SetBool("Right Back", false);
            anim.SetBool("Left Back", false);
            anim.SetBool("Right Forward", true);
            anim.SetBool("Left Forward", true);

            float percentageComplete = elapsedTime / desiredDuration;

            Rigidbody ourBody = this.GetComponent<Rigidbody>();
            ourBody.MoveRotation(ourBody.rotation);
            float movement = Mathf.Lerp(0f, 0.025f, percentageComplete);
            Vector3 moveBack = new Vector3(movement, 0f, 0f);
            moveBack = ourBody.transform.TransformDirection(moveBack);
            ourBody.transform.position += moveBack;
        }
        if (vertical < 0)
        {
            if (noBackMov)
            {
                elapsedTime = 0;
                noBackMov = false;
            }
            anim.SetFloat("SpeedF", 1);
            anim.SetFloat("SpeedB", -1);
            anim.SetBool("Right Forward", false);
            anim.SetBool("Left Forward", false);
            anim.SetBool("Right Back", true);
            anim.SetBool("Left Back", true);
            
            float percentageComplete = elapsedTime / desiredDuration;

            anim.SetFloat(hash.speedFloat, -animationSpeed, speedDampTime, Time.deltaTime);


            Rigidbody ourBody = this.GetComponent<Rigidbody>();
            ourBody.MoveRotation(ourBody.rotation);
            float movement = Mathf.Lerp(0f, -0.025f, percentageComplete);
            Vector3 moveBack = new Vector3(movement, 0f, 0f);
            moveBack = ourBody.transform.TransformDirection(moveBack);
            ourBody.transform.position += moveBack;
        }

        if (vertical == 0)
        {
            anim.SetFloat(hash.speedFloat, 0);
            anim.SetBool(hash.backwardsBool, false);
            noBackMov = true;
            anim.SetFloat("SpeedF", 0);
            anim.SetFloat("SpeedB", 0);
        }
    }

    private void MineManagement(bool on)
    {
        if (on)
        {
            anim.SetBool("Mining", true);
        }
        else if (!on)
        {
            anim.SetBool("Mining", false);
        }
    }
}
