/*
 * BossScript deals with the movement and behavior of the boss enemy.
 * 
 * Monobehaviour has a Start() for the initalization of the instance of the gameobject, 
 * and various Update functions that are called by the game clock.
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
     public float speed;
     public int damage;
     public Transform BossFirePoint;
     public Transform target;
     public GameObject seekingenemyprefab;
     public float rotation_speed;
     public int health;

     private Vector2 mov_vector;
     private Vector2 start_position;
     private Rigidbody2D rb2d;
     private Movescript MainCharacter;
     private GameObject go;
     private int mode = 0;
     private float timer = 0;
     private float timerMax = 0;
     public GameObject AudioSource;
     public GameObject SpawnedAudioSource;
     public GameObject TookDamageAudioSource;


     /*
      * Mode 0: Finding the Charge
      * Mode 1: Charging:
      * Mode 2: Returning:
      */

     // Start is called before the first frame update
     void Start()
    {
          //make sure the boss doesn't move before it is seen by the player.
          enabled = false;
          //get references to components and game objects
          rb2d = GetComponent<Rigidbody2D>();
          go = GameObject.Find("MainCharacter");
          MainCharacter = (Movescript)go.GetComponent(typeof(Movescript));
          start_position.x = this.transform.position.x;
          start_position.y = this.transform.position.y;
          AudioSource = GameObject.Find("EnemyDiedAudio");
          SpawnedAudioSource = GameObject.Find("SpawnedEnemyAudio");
          TookDamageAudioSource = GameObject.Find("BossDamageAudio");
     }

     // Update is called once per frame
     void Update()
    {
          //calculate the vector for the charge toward the player
          if (mode == 0)
          {
               float x = go.transform.position.x;
               float y = go.transform.position.y;
               mov_vector.x = x - this.transform.position.x;
               mov_vector.y = y - this.transform.position.y;
               mode++;
          }
          else if (mode == 2) //teleport back to the start point, wait three seconds, and spawn an enemy. 
          {
               this.transform.position = start_position;
               if (Waited(3)) SpawnEnemy();
          }
         

     }

     void FixedUpdate()
     {
          //accelerate towards the charge vector
          rb2d.AddForce(mov_vector * speed);
          //rotate toward the player
          turn_torward();
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
          else if (col.gameObject.name == "trackingenemy(Clone)") { }
          else if (col.gameObject.name == "Lazer(Clone)")
          {
               //decrease the boss's health if it collides with a laser
               dec_health();
          }
          else if (col.gameObject.name == "WallBlock")
          {
               //if the boss comes into contact with the edge of the arena, teleport back to the center.
               rb2d.velocity = Vector2.zero;
               mov_vector = Vector2.zero;
               mode = 2;
          }
     }

     void OnBecameVisible()
     {
          //once the boss is visible, it can begin moving.
          enabled = true;
     }

     void SpawnEnemy()
     {
          //spawn an enemy, and set the timers to 0 so the update can properly wait to spawn new enemies
          SpawnedAudioSource.GetComponent<AudioSource>().Play();
          var clone = Instantiate(seekingenemyprefab, BossFirePoint.position, BossFirePoint.rotation);
          mode = 0;
          timer = 0;
          timerMax = 0;
}

     private bool Waited(float seconds)
     {
          //waits seconds and returns true if it has waited that long. 
          timerMax = seconds;

          timer += Time.deltaTime;

          if (timer >= timerMax)
          {
               return true; //max reached - waited x - seconds
          }

          return false;
     }
     
     void turn_torward()
     {
          Vector3 offset = target.position - transform.position; //calculates a vector toward the player. 

          transform.rotation = Quaternion.LookRotation(Vector3.forward, offset); //rotates the boss toward the offset, keeps the z rotation the same because this game is 2d, and doesn't have a z axis. 
     }
     void dec_health()
     {
          //decreases health, checks if health is below 0 and if it is it will destroy the boss. 
          health--;
          TookDamageAudioSource.GetComponent<AudioSource>().Play();
          if (health <= 0)
          {
              AudioSource.GetComponent<AudioSource>().Play();
               Destroy(this.gameObject);
          }
     }

}
