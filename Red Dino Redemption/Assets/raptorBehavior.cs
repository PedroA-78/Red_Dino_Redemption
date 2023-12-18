using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class raptorBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 0.1f;
    private Rigidbody raptorRb;
    private GameObject playerGmo;
    public float within_range;
    public Transform target;
    public NavMeshAgent enemy;
    private Animator anim;
    public float distance;

    void Start()
    {
        raptorRb = GetComponent<Rigidbody>();
        playerGmo = GameObject.Find("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //   raptorRb.AddForce((playerGmo.transform.position - transform.position).normalized * speed);
    //}

    public void Update()
    {
        //get the distance between the player and enemy (this object)
         float dist = Vector3.Distance(playerGmo.transform.position, raptorRb.transform.position);
        //check if it is within the range you set
        if (dist <= within_range)
        {

            this.gameObject.transform.LookAt(playerGmo.transform.position);

            if (dist <= within_range- distance) { 
                enemy.SetDestination(playerGmo.transform.position);
            }
            //anim.Play(1);
            //this.gameObject.transform.forward = (Vector3.MoveTowards(transform.position, target.transform.position, speed);

        }
        //else, if it is not in rage, it will not follow player
    }
}
