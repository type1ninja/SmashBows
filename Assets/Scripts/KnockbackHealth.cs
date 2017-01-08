using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackHealth : MonoBehaviour
{
    //The character's Rigidbody
    private Rigidbody rigbod;

    //knockbackHealth is the multiplier for knockback the player takes
    private float knockbackHealth = 1.00f;

    private void Start()
    {
        rigbod = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("Arrow"))
        {
            //something something damage
            rigbod.AddForce(collision.transform.GetComponent<Rigidbody>().velocity * 2f, ForceMode.Impulse);
        }
    }
}