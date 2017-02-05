using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Death : NetworkBehaviour {

    private Rigidbody rigbod;
    private KnockbackModifier knockbackMod;
    private PlayerColor playerColor;

    private static Vector3[] RESPAWN_POINTS = {
        new Vector3(17, 4, 17), new Vector3(17, 4, -17),
        new Vector3(-17, 4, 17), new Vector3(-17, 4, -17)
    };

    private void Start()
    {
        rigbod = GetComponent<Rigidbody>();
        knockbackMod = GetComponent<KnockbackModifier>();
        playerColor = GetComponentInChildren<PlayerColor>();
    }

	void FixedUpdate () {
        if (transform.position.y < -20 && isServer)
        {
            RpcRespawn();
        }
	}

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            transform.position = RESPAWN_POINTS[Random.Range(0, RESPAWN_POINTS.Length)];
            rigbod.velocity = Vector3.zero;
        }

        //TODO--temporary if statement so that the crate can be knocked off the cliff and come back
        //remove the if statement later
        if (knockbackMod != null)
        {
            knockbackMod.ResetDamage();
        }
        if (playerColor != null)
        {
            playerColor.ResetColor();
        }
}
}