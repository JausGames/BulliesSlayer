using UnityEngine;
using System.Collections;

public class animController : MonoBehaviour {

   public Animator anim;

 // Use this for initialization
 void Start ()
    {
        //anim = GetComponent<Animator>();
        anim = GetComponentInChildren<Animator>();
 }
 
 // Update is called once per frame
 void Update () {
            anim.Play("Walk");
    }
}﻿