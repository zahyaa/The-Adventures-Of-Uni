/*
 * Seekscript handles the behavior of the seeking/tracking enemies.
 * 
 * Monobehaviour has a Start() for the initalization of the instance of the gameobject, 
 * and various Update functions that are called by the game clock.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class seekscript : MonoBehaviour
{

     private Vector2 mov_vector;
     private Rigidbody2D rb2d;
     public float speed;
     public int damage;
     public bool start_moving;
     public GameObject AudioSource;



     private Movescript MainCharacter;
     private GameObject go;

     // Start is called before the first frame update
     void Start()
    {
          //get a reference to the main character so its components can be called
          rb2d = GetComponent<Rigidbody2D>();
          go = GameObject.Find("MainCharacter");
          //get a reference to the enemydiedaudio so it can be played later
          AudioSource = GameObject.Find("EnemyDiedAudio");
          //get a reference to movescript so the maincharacter can be damaged
          MainCharacter = (Movescript)go.GetComponent(typeof(Movescript));
          //by default enemies with seekscript will not start moving unless they are visible to the camera. 
          //the spawned enemy by the boss does not follow this restriction however, which is what the start_moving parameter is for
          enabled=false;
          if (start_moving) enabled = true;
     }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
     {
          //Find the position of the main character and record it.
          float x = go.transform.position.x;
          float y = go.transform.position.y;
          Vector2 mov_vector;
          //Calculate where the seeking enemy should go to get to the main character, and move it toward the main character
          mov_vector.x = x - this.transform.position.x;
          mov_vector.y = y - this.transform.position.y;
          rb2d.AddForce(mov_vector * speed);
     }

     void OnCollisionEnter2D(Collision2D col)
     {

          //If this object collides with the maincharacter, it will decrement health equal to the damage variable
          if (col.gameObject.name == "MainCharacter")
          {
               for (int i = 0; i < damage; i++)
               {
                    MainCharacter.dec_health();
               }
          }
          if (col.gameObject.name == "Lazer(Clone)") //if this object collides with a laser, play its death audio and destroy it
          {
               AudioSource.GetComponent<AudioSource>().Play();
               Destroy(this.gameObject);
          }
     }

     void OnBecameVisible()
     {
          //the enemy will not move unless it has been seen by the camera
       enabled=true;
     }
}
