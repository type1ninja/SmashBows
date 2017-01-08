using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private Rigidbody rigbod;

    private float currentLifetime = 0;
    private float maxLifetime = 10;

    private void Start()
    {
        rigbod = GetComponent<Rigidbody>();  
    } 

    private void FixedUpdate()
    {
        //Face in the direction you're moving
        transform.rotation = Quaternion.LookRotation(rigbod.velocity);

        currentLifetime += Time.fixedDeltaTime;
        if (currentLifetime >= maxLifetime)
        {
            Destroy(gameObject);
        }
    } 
}