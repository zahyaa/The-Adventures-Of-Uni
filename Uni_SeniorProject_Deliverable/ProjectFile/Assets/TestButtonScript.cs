using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButtonScript : MonoBehaviour
{
     void OnGUI()
     {
          GUI.Box(new Rect(100, 100, 100, 100), "This is a box");
     }
}
