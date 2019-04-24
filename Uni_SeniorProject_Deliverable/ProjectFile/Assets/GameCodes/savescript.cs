/*
 * SaveScript writes to the save file dependent on whether the main character made it past a checkpoint.
 * 
 * Monobehaviour has a Start() for the initalization of the instance of the gameobject, 
 * and various Update functions that are called by the game clock.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;

public class savescript : MonoBehaviour
{
     public GameObject maincharacter;
     public GameObject checkpoint1_go;
     public GameObject checkpoint2_go;
     public string path;
     private bool checkpoint1;
     private bool checkpoint2;
     private Movescript movescript;
     // Start is called before the first frame update
     void Start()
    {
          //set checkpoints to false.
          checkpoint1 = false;
          checkpoint2 = false;
    }

    // Update is called once per frame
    void Update()
    {
          //if the main character passes a checkpoint, save the game.
          if (maincharacter.transform.position.x > checkpoint1_go.transform.position.x && !checkpoint1)
          {
               save("Level1Check1");
               checkpoint1 = true;
          }
          else if (maincharacter.transform.position.x > checkpoint2_go.transform.position.x && !checkpoint2)
          {
               Vector2 teleport_position;
               //once you pass the second flag, teleport the character to the boss area. 
               teleport_position.x = -190;
               teleport_position.y = 1;
               maincharacter.transform.position = teleport_position; 
               save("Level1Check2");
               checkpoint2 = true;
               enabled = false;
          }
    }

    void save(string name)
     {
          //create a save file containing the scene name and the health total. 
          if (File.Exists(path)) File.Delete(path);
          using (FileStream fs = File.Create(path))
          {
               byte[] info = new UTF8Encoding(true).GetBytes(name);
               fs.Write(info, 0, info.Length);
               movescript = (Movescript)maincharacter.GetComponent(typeof(Movescript));
               byte[] newline = Encoding.ASCII.GetBytes(System.Environment.NewLine);
               fs.Write(newline, 0, newline.Length);
               string health_string = movescript.get_health().ToString();
               info = new UTF8Encoding(true).GetBytes(health_string);
               fs.Write(info, 0, info.Length);
               //  print(name + "saved!");
          }
     }
}
