using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Arrow : NetworkBehaviour {

    static float BASE_KNOCKBACK = 10f;
    static float BASE_DAMAGE = 20f;
    //an offset so that players get knocked up off the ground
    static Vector3 KNOCKBACK_UP_OFFSET = new Vector3(0, .5f, 0);

    private Rigidbody rigbod;
    [SyncVar]
    private NetworkInstanceId spawnedBy;
    //private KillCount myKillCount;

    private float power = 1f;

    private bool hasAttached = false;

    private void Start()
    {
        rigbod = GetComponent<Rigidbody>();
    }

    public override void OnStartClient()
    {
        GameObject obj = ClientScene.FindLocalObject(spawnedBy);
        Physics.IgnoreCollision(GetComponent<Collider>(), obj.GetComponent<Collider>());
    }

    private void FixedUpdate()
    {
        if (!hasAttached)
        {
            //Face in the direction you're moving
            transform.rotation = Quaternion.LookRotation(rigbod.velocity);
        }
    }

    public void SetPower(float newPower)
    {
        power = newPower;
    }

    public void SetKCount(KillCount newKillCount)
    {
        //myKillCount = newKillCount;
    }

    public void SetSpawnedBy(NetworkInstanceId newSpawnerId)
    {
        spawnedBy = newSpawnerId;
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
                    if (isServer)
                    {
                        otherKnockbackModifier.RpcTakeKnockback((rigbod.velocity.normalized + KNOCKBACK_UP_OFFSET) * BASE_KNOCKBACK * power * otherKnockbackModifier.GetKnockbackModifier());
                    }
                    otherKnockbackModifier.TakeDamage(BASE_DAMAGE * power);
                } else
                {
                    otherRigbod.AddForce(rigbod.velocity.normalized * BASE_KNOCKBACK * power, ForceMode.Impulse);
                }

                Death otherDeath = other.GetComponent<Death>();
                if (otherDeath != null)
                {
                    //otherDeath.SetLastPlayerHit(myKillCount);
                }
            }

            rigbod.velocity = Vector3.zero;
            rigbod.isKinematic = true;
            transform.SetParent(other.transform);

            hasAttached = true;
        }
    }
}