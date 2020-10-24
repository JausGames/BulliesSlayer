using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensibility = 100f;

    public Transform playerBody;

    float xRotation = 0f;
    private float mouseY = 0f;
    private float mouseX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponentInParent<PlayerMovement>().transform;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void SetLook(Vector2 dir)
    {
        mouseX = dir.x * mouseSensibility * Time.deltaTime;
        mouseY = dir.y * mouseSensibility * Time.deltaTime;
    }
    // Update is called once per frame
    void Update()
    {
        //mouseX = Input.GetAxis("Mouse X") * mouseSensibility * Time.deltaTime;
        //mouseY = Input.GetAxis("Mouse Y") * mouseSensibility * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
