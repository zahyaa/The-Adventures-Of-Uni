/*
 * Lazer handles the behavior of the Lazer object
 * 
 * Monobehaviour has a Start() for the initalization of the instance of the gameobject, 
 * and various Update functions that are called by the game clock.
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour {

	public float speed =5f;
	public int damage = 40;
	public Rigidbody2D rb;
	public GameObject impactEffect;

	private GameObject go;

	// Use this for initialization
	void Start () {
			rb.velocity = transform.right * speed;
	}

// changed from trigger
  void OnCollisionEnter2D ( Collision2D col ) {
		if ( col.gameObject.name != "MainCharacter" ){

			Destroy( this.gameObject );

		}
	}

}
