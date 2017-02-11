using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Death : NetworkBehaviour {

    private Rigidbody rigbod;
    private KnockbackModifier knockbackMod;
    private PlayerColor playerColor;

    private Text deathCountText;

    //The kill count of the last player to hit me
    //private KillCount lastPlayerHit;

    private static Vector3[] RESPAWN_POINTS = {
        new Vector3(17, 4, 17), new Vector3(17, 4, -17),
        new Vector3(-17, 4, 17), new Vector3(-17, 4, -17)
    };

    int deathCount = 0;

    private void Start()
    {
        rigbod = GetComponent<Rigidbody>();
        knockbackMod = GetComponent<KnockbackModifier>();
        playerColor = GetComponentInChildren<PlayerColor>();

        deathCountText = GameObject.Find("HUDCanvas").transform.Find("DeathCountBackground").Find("DeathCountText").GetComponent<Text>();
        if (isLocalPlayer)
        {
            deathCountText.text = "Deaths: 0";
        }
    }

	void FixedUpdate () {
        if (transform.position.y < -20 && isServer)
        {
            RpcRespawn();
        }
	}

    public void SetLastPlayerHit(KillCount kCount)
    {
        //lastPlayerHit = kCount;
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            transform.position = RESPAWN_POINTS[Random.Range(0, RESPAWN_POINTS.Length)];
            rigbod.velocity = Vector3.zero;
            deathCount++;
            deathCountText.text = "Deaths: " + deathCount;
            //lastPlayerHit.AddKill();
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