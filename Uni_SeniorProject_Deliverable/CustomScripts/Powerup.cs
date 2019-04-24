/*
 * Handles the behavior of the powerup game object, will set the powerup bool in the WeaponScript
 * 
 * Monobehaviour has a Start() for the initalization of the instance of the gameobject, 
 * and various Update functions that are called by the game clock.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;

public class Powerup : MonoBehaviour
{


    // this powerup will modify the shooting mechanics of the game

    private Movescript MainCharacter;

    void OnCollisionEnter2D ( Collision2D col ) {

      if ( col.gameObject.name == "MainCharacter" ) {
      //if this object collides with the maincharacter, set its powerup bool to true and destroy the powerup.
        col.gameObject.GetComponent<Weapon>().powerup = true;
        Destroy( this.gameObject );

      }

    }

}
