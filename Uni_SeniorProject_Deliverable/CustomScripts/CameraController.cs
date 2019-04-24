/*
 * CameraController sets the position of the camera to the maincharacter
 * 
 * Monobehaviour has a Start() for the initalization of the instance of the gameobject, 
 * and various Update functions that are called by the game clock.
 * 
 */
using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

     public GameObject Player1;       //stores a reference to where the camera will be centered


     private Vector3 offset;         //variable to set the offset distance

     // Use this for initialization
     void Start()
     {
          //find the offset between the camera and the player.
          offset = transform.position - Player1.transform.position;
     }

     // LateUpdate is called after Update each frame
     void LateUpdate()
     {
          // Set the position of the camera to be the same as the players, but offset
          transform.position = Player1.transform.position + offset;
     }
}