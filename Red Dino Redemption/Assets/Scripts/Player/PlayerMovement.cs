using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform camTransform;
    Vector2 rotationMouse;
    public int sensiblity;
    public int velocity;

    private Animator anim;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        anim.SetFloat("horizontalMove", x);
        anim.SetFloat("verticalMove", y);

        float rotationX = mouseX * sensiblity * Time.deltaTime;
        float rotationY = mouseY * sensiblity * Time.deltaTime;

        rotationMouse = new Vector2(rotationX, rotationY);

        // Vector3 dir = new Vector3(x, 0, y) * velocity;

        // transform.Translate(dir * Time.deltaTime);

        transform.Rotate(new Vector3(
            0,
            rotationMouse.x,
            0
        ));

        rotationMouse.y = Mathf.Clamp(rotationMouse.y, -20, 20);

        camTransform.transform.Rotate(new Vector3(
            -rotationMouse.y,
            0,
            0
        ));
    }
}