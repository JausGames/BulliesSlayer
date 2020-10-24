using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

 public class SwitchingCameras : MonoBehaviour
{

 public Camera cam1;
 public Camera cam2;
 
 void Start() {
     cam2.enabled = true;
     cam1.enabled = false;
    }
 
 void Update() {
 
     if (Input.GetKeyDown(KeyCode.Tab)) {
         cam1.enabled = !cam1.enabled;
         cam2.enabled = !cam2.enabled;
     }
 }
}