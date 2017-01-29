﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class VoidDeath : NetworkBehaviour {

    private Rigidbody rigbod;
    private KnockbackModifier knockbackMod;
    private PlayerColor playerColor;

    private static Vector3 RESPAWN_POINT = new Vector3(0, 2, 0);

    private void Start()
    {
        rigbod = GetComponent<Rigidbody>();
        knockbackMod = GetComponent<KnockbackModifier>();
        playerColor = GetComponentInChildren<PlayerColor>();
    }

	void FixedUpdate () {
        if (transform.position.y < -20)
        {
            //TODO--temporary if statement so that the crate can be knocked off the cliff and come back
            //remove this later
            if (knockbackMod != null)
            {
                knockbackMod.ResetDamage();
            }
            if (playerColor != null)
            {
                playerColor.ResetColor();
            }

            if (isServer) { 
                RpcRespawn();
            }
        }
	}

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            transform.position = RESPAWN_POINT;
            rigbod.velocity = Vector3.zero;
        }
    }
}