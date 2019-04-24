/*
 * Change Level Script changes the scene to the correct level in the main menu.
 * 
 * Monobehaviour has a Start() for the initalization of the instance of the gameobject, 
 * and various Update functions that are called by the game clock.
 * 
 */
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevelScript : MonoBehaviour
{
     private GameObject go;
     public string path;
     // Start is called before the first frame update
     void Start()
    {
       go = GameObject.Find("New Game Button");
     }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
     {
          //if there is a save, delete it. Clear the buttons and load the first checkpoint.
          if (File.Exists(path)) File.Delete(path);
          clearButtons();
          SceneManager.LoadScene("Level1Check0", LoadSceneMode.Additive);
     }
     //deletes all the buttons for save/load.
    public void clearButtons()
     {
          go = GameObject.Find("New Game Button");
          Destroy(go);
          go = GameObject.Find("Load Game Button");
          Destroy(go);
          go = GameObject.Find("TitleScreen");
          Destroy(go);
     }
     public void LoadGame()
     {
          //if there is a file, load the scene name from the save tile, and then load the scene
          if (File.Exists(path))
          {
               string levelname = "";
               using (StreamReader sr = File.OpenText(path))
               {
                    levelname = sr.ReadLine();
               }
               clearButtons();
               SceneManager.LoadScene(levelname, LoadSceneMode.Additive);
          }
     }


}
