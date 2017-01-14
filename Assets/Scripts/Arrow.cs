using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    static float BASE_KNOCKBACK = 10f;
    static float BASE_DAMAGE = 20f;

    private Rigidbody rigbod;

    private float currentLifetime = 0;
    private float maxLifetime = 5;
    private float power = 1f;

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

    public void SetPower(float newPower)
    {
        power = newPower;
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerStay(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!hasAttached && other.tag != "Arrow")
        {
            Rigidbody otherRigbod = other.GetComponent<Rigidbody>();
            if (otherRigbod != null)
            {
                KnockbackModifier otherKnockbackModifier = other.GetComponent<KnockbackModifier>();
                if (otherKnockbackModifier != null)
                {
                    otherRigbod.AddForce(rigbod.velocity.normalized * BASE_KNOCKBACK * power * otherKnockbackModifier.GetKnockbackModifier(), ForceMode.Impulse);
                    otherKnockbackModifier.TakeDamage(BASE_DAMAGE * power);
                } else
                {
                    otherRigbod.AddForce(rigbod.velocity.normalized * BASE_KNOCKBACK * power, ForceMode.Impulse);
                }
            }

            rigbod.velocity = Vector3.zero;
            rigbod.isKinematic = true;
            transform.SetParent(other.transform);

            hasAttached = true;
        }
    }
}