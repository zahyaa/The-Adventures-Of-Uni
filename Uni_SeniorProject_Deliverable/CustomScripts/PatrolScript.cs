/*
 * PatrolScript deals with the behavior of the patrolling enemies
 * 
 * Monobehaviour has a Start() for the initalization of the instance of the gameobject, 
 * and various Update functions that are called by the game clock.
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolScript : MonoBehaviour {

     public Vector2 mov_vector;
     private Rigidbody2D rb2d;
     public float speed;
     public int damage;

     private Movescript MainCharacter;
     private GameObject go;
     public GameObject AudioSource;


     // Use this for initialization
     void Start () {
          //Gets a reference the the rigidbody, which contains physics functions
          rb2d = GetComponent<Rigidbody2D>();
          //freeze rotation
          rb2d.freezeRotation = true;
          //pull a reference to the main character so it can damage it. 
          go = GameObject.Find("MainCharacter");
          MainCharacter = (Movescript)go.GetComponent(typeof(Movescript));
          //pull a reference to the death sound audio so it can play it. 
          AudioSource = GameObject.Find("EnemyDiedAudio");
     }


	void FixedUpdate () {
          //accelerate the object if it isn't more than the max velocity.
          if (rb2d.velocity.magnitude <=20) rb2d.AddForce(mov_vector * speed);
     }
     void OnCollisionEnter2D(Collision2D col)
     {
          //Stops the object, then reverses the direction of the vector upon collision
          rb2d.velocity = Vector2.zero;
          mov_vector.x = mov_vector.x * -1;
          mov_vector.y = mov_vector.y * -1;

          //If this object collides with the maincharacter, it will decrement health equal to the damage variable
          if (col.gameObject.name == "MainCharacter")
          {
               for (int i = 0; i < damage; i++)
               {
                    MainCharacter.dec_health();
               }
          }
          if (col.gameObject.name == "Lazer(Clone)") //If the collision is with a lazer projectile, the object will play its death sound and die
          {
               AudioSource.GetComponent<AudioSource>().Play();
               Destroy(this.gameObject);
          }
     }
}
