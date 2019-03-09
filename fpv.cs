using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fpv : MonoBehaviour
{
    CharacterController cc;
    Vector3 moveDirection = new Vector3();
    float yspeed = 0;
    float gravity = -15f;
    public float speed = 10f;
    public Transform fpsCamera;
    float pitch = 0f;
    

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        
    }    

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(xInput, 0, zInput);
        move = Vector3.ClampMagnitude(move, speed);
        move = transform.TransformVector(move);

        if (cc.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                yspeed = 10f;
            }

           else
            {
               yspeed = gravity * Time.deltaTime;
             }
        }

        else
        {
            yspeed += gravity * Time.deltaTime;
        }
        cc.Move((move + new Vector3(0, yspeed, 0)) * Time.deltaTime * speed);

        float xMouse = Input.GetAxis("Mouse X") * 5f;
        transform.Rotate(0, xMouse, 0);

        pitch -= Input.GetAxis("Mouse Y") * 5f;
        pitch = Mathf.Clamp(pitch, -45f, 45f);
        Quaternion camRotation = Quaternion.Euler(pitch, 0, 0);
        fpsCamera.localRotation = camRotation;
    }
}
