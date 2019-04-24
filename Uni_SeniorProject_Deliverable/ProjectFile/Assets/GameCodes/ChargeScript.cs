/*
 * ChargeScript handles the behaviour of the charging enemies.
 * 
 * Monobehaviour has a Start() for the initalization of the instance of the gameobject, 
 * and various Update functions that are called by the game clock.
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeScript : MonoBehaviour
{

     private Vector2 mov_vector;
     private Rigidbody2D rb2d;
     public float speed;
     public int damage;
     private bool set_flag = false;

     public GameObject AudioSource;
     private Movescript MainCharacter;
     private GameObject go;

     // Start is called before the first frame update
     void Start()
     {
          //initalizes references to components, and sets enabled to false so the enemy does not charge until it is seen by the camera.
          rb2d = GetComponent<Rigidbody2D>();
          go = GameObject.Find("MainCharacter");
          MainCharacter = (Movescript)go.GetComponent(typeof(Movescript));
          AudioSource = GameObject.Find("EnemyDiedAudio");
          enabled = false;
          
     }

     // Update is called once per frame
     void Update()
     {
          //if the charge has not been set, find the player and calculate the vector to go toward the player.
          if (!set_flag)
          {  
               float x = go.transform.position.x;
               float y = go.transform.position.y;
               mov_vector.x = x - this.transform.position.x;
               mov_vector.y = y - this.transform.position.y;
               set_flag = true;
          }
     }

     void FixedUpdate()
     {
          //accelerate toward the m ove vector. 
          rb2d.AddForce(mov_vector * speed);
     }

     void OnCollisionEnter2D(Collision2D col)
     {

          //If this object collides with the maincharacter, it will decrement health equal to the damage variable
          if (col.gameObject.name == "player1")
          {
               for (int i = 0; i < damage; i++)
               {
                    MainCharacter.dec_health();
               }
          }
          if (col.gameObject.name == "Lazer(Clone)") //if this object collides with a laser, destroy it.
          {
               AudioSource.GetComponent<AudioSource>().Play();
               Destroy(this.gameObject);
          }
     }

     //when the object is visible to the camera, it can begin moving
     void OnBecameVisible()
     {
          enabled = true;
     }
}
