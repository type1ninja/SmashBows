using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidDeath : MonoBehaviour {

    private Rigidbody rigbod;
    private KnockbackModifier knockbackMod;
    private PlayerColor playerColor;

    private void Start()
    {
        rigbod = GetComponent<Rigidbody>();
        knockbackMod = GetComponent<KnockbackModifier>();
        playerColor = GetComponentInChildren<PlayerColor>();
    }

	void FixedUpdate () {
		if (transform.position.y < -20)
        {
            transform.position = new Vector3(0, 5, 0);
            rigbod.velocity = Vector3.zero;
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
        }
	}
}