/*
 * Playermovement deals with the animation of the main character
 * 
 * Monobehaviour has a Start() for the initalization of the instance of the gameobject, 
 * and various Update functions that are called by the game clock.
 */
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public Movescript mvs;

    public Animator anim;

    public float runSpeed = 40f; //Character speed
    float horizontalMove = 0f;

    bool jump = false; //checks jumps

    private float timer = 0;
    private float timerMax = 0;


    Timer jumpTime = new Timer
    {
        Interval = 1000
    };

     //will return true if it has been 3 seconds. 
    private bool Waited(float seconds)
    {
        timerMax = seconds;

        timer += Time.deltaTime;

        if (timer >= timerMax)
        {
            return true; //max reached - waited x - seconds
        }

        return false;
    }


    // Update is called once per frame
    void Update () {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        anim.SetFloat("Speed", Mathf.Abs(horizontalMove)); // run is always positive

        // check the spacebar for input
        if (Input.GetButtonDown("Jump"))
        {
            //sets the animation to jump
            jump = true;
            anim.SetBool("IsJump", true);
            anim.SetBool("GotoIdle", false);
            timer = 0;

        }
        if (!jump && Waited(1))
        {
          //sets the animation back to idle.
            anim.SetBool("GotoIdle", true);
            anim.SetBool("IsJump", false);
        }

     }


    private void FixedUpdate()
    {
        mvs.Move(horizontalMove * Time.fixedDeltaTime,jump); // calculates moves, checks jumps and crouch
        jump = false; // jump is false if button is not activated
    }
}
