/*
 * Weapon handles the firing of the weapon from the main character. 
 * 
 * Monobehaviour has a Start() for the initalization of the instance of the gameobject, 
 * and various Update functions that are called by the game clock.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour {

	public Transform firePoint;
	public Transform firePoint1;
	public Transform firePoint2;
	public GameObject LazerPrefab;

	AudioSource audioSource;
	public AudioClip impact;

	public bool powerup = false;

	void Start()
	{
          //pull the shoot sound from the object.
		audioSource=GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
          //handles what kind of projectiles the player should shoot if the button is pressed. 
		if ( Input.GetButtonDown( "Fire1" ) )
		{
			if ( powerup == false ) {
				Shoot();

			}
			else
			Shoot_powered();

		}

	}

	void Shoot ()
	{
		// shooting logic
		var clone = Instantiate( LazerPrefab, firePoint.position, firePoint.rotation );
		GetComponent<AudioSource>().Play();
		Destroy ( clone.gameObject, 3 );		// destroys the lazer after 3 seconds
	}

	void Shoot_powered () {

		// shooting logic
		GetComponent<AudioSource>().Play();
		var clone = Instantiate( LazerPrefab, firePoint.position, firePoint.rotation );
		var clone1 = Instantiate( LazerPrefab, firePoint1.position, firePoint1.rotation );
		var clone2 = Instantiate( LazerPrefab, firePoint2.position, firePoint2.rotation );

	}
}
