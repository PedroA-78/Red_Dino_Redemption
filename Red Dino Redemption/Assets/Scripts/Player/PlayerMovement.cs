using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Transform camTransform;
    Vector2 rotationMouse;
    public int sensiblity;
    public int velocity;

    private Animator anim;
    public GameObject Axe;
    public GameObject Pistol;
    public GameObject WeaponUI;
    private GameObject crrWeapon;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        anim = GetComponent<Animator>();

        Axe.SetActive(false);
        Pistol.SetActive(false);

        crrWeapon = Pistol;
        WeaponUI.SetActive(false);
        
    }

    async void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float x = Input.GetAxis ("Horizontal");
        float y = Input.GetAxis ("Vertical");
        bool click = Input.GetMouseButtonDown(0);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) {
            crrWeapon = Pistol;
            await swipeIcon("Pistol");
            
        } else if (scroll < 0f) {
            crrWeapon = Axe;
            await swipeIcon("Axe");
        }


        if (Input.GetMouseButtonDown(1)){
            if (crrWeapon.name == "Pistol") {
                anim.SetBool("mousePressed", true);
            } else if (crrWeapon.name == "Axe 1") {
                anim.SetBool("scrollPressed", true);
            }
            anim.SetBool("triggered", false);
            anim.SetFloat("verticalMove", y);
            anim.SetFloat("horizontalMove", x);

            crrWeapon.SetActive(true);

        } else if (Input.GetMouseButtonDown(2)) {
            Axe.SetActive(true);

            anim.SetBool("scrollPressed", true);
            anim.SetFloat("verticalMove", y);
            anim.SetFloat("horizontalMove", x);

            if (Input.GetMouseButtonDown(0)) {
                anim.SetBool("triggered", true);
            }

                    

        } else if (Input.GetKeyDown(KeyCode.LeftShift) && (x != 0 || y != 0)) {
            anim.SetBool("shiftPressed", true);
            anim.SetFloat("verticalMove", y);
            anim.SetFloat("horizontalMove", x);
            Debug.Log("Horizontal: " + x);
            Debug.Log("Vertical: " + y);

        } else {
            anim.SetFloat("verticalMove", y);
            anim.SetFloat("horizontalMove", x);
        }

        if (Input.GetMouseButtonUp(1)) {
            if (crrWeapon.name == "Pistol") {
                anim.SetBool("mousePressed", false);
            } else if (crrWeapon.name == "Axe 1") {
                anim.SetBool("scrollPressed", false);
            }
            crrWeapon.SetActive(false);
            // await Task.Delay(450);

            // anim.SetBool("triggered", true);
            // Debug.Log("Tested");
        } else if (Input.GetMouseButtonUp(2)) {
            anim.SetBool("scrollPressed", false);
            Axe.SetActive(false);
        } else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            anim.SetBool("shiftPressed", false);
        } 

        // if (Input.GetMouseButtonUp(0)) {
        //         anim.SetBool("triggered", false);
        // }

        if (anim.GetBool("scrollPressed") && click) {
            anim.SetBool("triggered", true);

            await Task.Delay(1000);

            anim.SetBool("triggered", false);
        }


        float rotationX = mouseX * sensiblity * Time.deltaTime;
        float rotationY = mouseY * sensiblity * Time.deltaTime;

        rotationMouse = new Vector2(rotationX, rotationY);

        Vector3 dir = new Vector3(x, 0, y) * velocity;

        transform.Translate(dir * Time.deltaTime);

        transform.Rotate(new Vector3(
            0,
            rotationMouse.x,
            0
        ));

        // rotationMouse.y = Mathf.Clamp(rotationMouse.y, -20, 20);

        // camTransform.transform.Rotate(new Vector3(
        //     -rotationMouse.y,
        //     0,
        //     0
        // ));

        
    }

    public async Task swipeIcon(String Item){
        if (Item == "Pistol") {
            WeaponUI.transform.GetChild(1).transform.gameObject.SetActive(true);
            WeaponUI.transform.GetChild(2).transform.gameObject.SetActive(false);
        } else if (Item == "Axe") {
            WeaponUI.transform.GetChild(1).transform.gameObject.SetActive(false);
            WeaponUI.transform.GetChild(2).transform.gameObject.SetActive(true);
        }

        WeaponUI.SetActive(true);

        await Task.Delay(3000);

        WeaponUI.SetActive(false);
    }
}