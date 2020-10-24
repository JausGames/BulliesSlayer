using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircleMovement : MonoBehaviour
{
    public Transform body = null;
    public Transform pivot = null;
    private float rotSpeed = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        body.RotateAround(pivot.position, Vector3.up, 0.0001f * rotSpeed / Time.deltaTime); 


    }
}
