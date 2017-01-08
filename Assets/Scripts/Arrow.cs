using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    static float BASE_KNOCKBACK = 0.5f;

    private Rigidbody rigbod;

    private float currentLifetime = 0;
    private float maxLifetime = 5;

    private bool hasAttached = false;

    private void Start()
    {
        rigbod = GetComponent<Rigidbody>();  
    } 

    private void FixedUpdate()
    {
        if (!hasAttached)
        {
            //Face in the direction you're moving
            transform.rotation = Quaternion.LookRotation(rigbod.velocity);
        }

        currentLifetime += Time.fixedDeltaTime;
        if (currentLifetime >= maxLifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasAttached)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<Rigidbody>().AddForce(rigbod.velocity * BASE_KNOCKBACK, ForceMode.Impulse);
            }

            if (other.tag != "Arrow")
            {
                rigbod.velocity = Vector3.zero;
                rigbod.isKinematic = true;
                transform.SetParent(other.transform);

                hasAttached = true;
            }
        }
    }
}