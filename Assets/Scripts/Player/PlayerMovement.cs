using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform groundCheck;


    public float speed = 8f;
    public float aeralSpeed = 2f;
    public float gravity = -9.81f;
    private float x = 0f;
    [SerializeField] private float y = 0f;
    private float z = 0f;

    public float jumpHeight = 2.5f;

    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    public LayerMask enemyMask;
    [SerializeField] bool isGrounded;

    Vector3 velocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        groundCheck = transform.Find("groundCheck");
    }

    public void SetMovement(Vector2 dir)
    {
        x = dir.x;
        z = dir.y;
    }
    public void Jump(bool perf, bool canc)
    {
        if (perf && isGrounded) y = Mathf.Sqrt(jumpHeight * -1f * gravity);
        else if (canc) y = 0;
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) 
                    || Physics.CheckSphere(groundCheck.position, groundDistance, enemyMask);

        Vector3 move = transform.right * x + transform.forward * z;

        if (isGrounded) controller.Move(move * speed * Time.deltaTime);
        else controller.Move(move * aeralSpeed * Time.deltaTime) ;

        if (y != 0) velocity.y = y;
        velocity.y += gravity * Time.deltaTime;
        Debug.Log("velocity y : " + velocity.y); 

        controller.Move(velocity * Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
