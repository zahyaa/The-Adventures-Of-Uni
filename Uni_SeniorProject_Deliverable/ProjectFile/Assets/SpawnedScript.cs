using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedScript : MonoBehaviour
{

     private Vector2 mov_vector;
     private Rigidbody2D rb2d;
     public float speed;
     public int damage;


     private Movescript MainCharacter;
     private GameObject go;

     // Start is called before the first frame update
     void Start()
     {
          rb2d = GetComponent<Rigidbody2D>();
          go = GameObject.Find("MainCharacter");
          MainCharacter = (Movescript)go.GetComponent(typeof(Movescript));
     }

     // Update is called once per frame
     void Update()
     {

     }

     void FixedUpdate()
     {
          float x = go.transform.position.x;
          float y = go.transform.position.y;
          Vector2 mov_vector;
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
          //Destroy(this.gameObject);
     }
}
