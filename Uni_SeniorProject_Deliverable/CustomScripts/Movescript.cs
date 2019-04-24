/*
 * Movescript moves the maincharacter and deals with character health
 * 
 * Monobehaviour has a Start() for the initalization of the instance of the gameobject, 
 * and various Update functions that are called by the game clock.
 * 
 * 
 */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine.Events;

public class Movescript : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 800f;                          // Amount of force added when the player jumps.
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings

    public string path;
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded = true;          // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 velocity = Vector3.zero;
    public GameObject AudioSource;
    public int health = 3; // health of the character

    public Slider HealthSlider;


    //creating events
    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;


    private void Awake()
    {
     //sets a unity event to initialize the animation.
        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }

        //pulls a reference to taking damage audio.
        AudioSource = GameObject.Find("TookDamageSource");

          //if there is a valid save file, read the health.
          if (File.Exists(path))
          {
               using (StreamReader sr = File.OpenText(path))
               {
                    sr.ReadLine();
                    string health_string = sr.ReadLine();

                    health = int.Parse(health_string);
               }
          }
          m_Rigidbody2D = GetComponent<Rigidbody2D>(); // gets a reference to the component that enables physics.
    }
     void Update()
     {
          if (health <= 0)
          {
               // if we have no health left, destroy the main character.
               Destroy(this.gameObject);
          }
     }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    private void FixedUpdate()
    {
        bool was_ground = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                m_Grounded = true;

            if (!was_ground)
            {
                OnLandEvent.Invoke();
            }
        }
          //set the value of the health slider to the player health.
          HealthSlider.value = health;
    }
    public void Move(float move,bool jump)
    {   //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        transform.Rotate ( 0f, 180f, 0f );
    }

    // called when the object collides with another object
    void OnCollisionEnter2D(Collision2D col)
     {
          m_Grounded= true;
     }
     public void dec_health()
     {
          //play the damage sound and decrement health
          AudioSource.GetComponent<AudioSource>().Play();
          health--;
     }

     public int get_health()
     {
          return health;
     }




}
