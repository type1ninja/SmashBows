using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class KnockbackModifier : NetworkBehaviour {

    Text healthText;
    private PlayerColor playerColor;
    
    //In hundreds, divide by 100 when we do the actual modifying
    [SyncVar]
    private float modifier = 100f;

    private void Start()
    {
        healthText = GameObject.Find("HUDCanvas").transform.FindChild("HealthBackground").FindChild("HealthText").GetComponent<Text>();
        playerColor = GetComponentInChildren<PlayerColor>();
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
            playerColor.UpdateColorDamage(modifier);
        }
    }

    public void ResetDamage()
    {
        modifier = 100f;
    }

    public float GetKnockbackModifier()
    {
        return modifier / 100;
    }
}