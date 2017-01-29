using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class KnockbackModifier : NetworkBehaviour {

    Text healthText;
    private PlayerColor playerColor;
    private Rigidbody rigbod;
    
    //In hundreds, divide by 100 when we do the actual modifying
    [SyncVar(hook = "OnChangeModifier")]
    private float modifier = 100f;

    private void Start()
    {
        healthText = GameObject.Find("HUDCanvas").transform.FindChild("HealthBackground").FindChild("HealthText").GetComponent<Text>();
        playerColor = GetComponentInChildren<PlayerColor>();
        rigbod = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            healthText.text = modifier.ToString("F0");
        }
    }

    public void TakeDamage(float damage)
    {
        if (isServer)
        {
            modifier += damage;
        }
    }

    [ClientRpc]
    public void RpcTakeKnockback(Vector3 knockback)
    {
        rigbod.AddForce(knockback, ForceMode.Impulse);
    }

    public void ResetDamage()
    {
        modifier = 100f;
    }

    void OnChangeModifier(float modifier)
    {
        playerColor.UpdateColorDamage(modifier);
    }

    public float GetKnockbackModifier()
    {
        return modifier / 100;
    }
}