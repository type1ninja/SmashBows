using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidDeath : MonoBehaviour {

    private Rigidbody rigbod;
    private KnockbackModifier knockbackMod;

    private void Start()
    {
        rigbod = GetComponent<Rigidbody>();
        knockbackMod = GetComponent<KnockbackModifier>();
    }

	void FixedUpdate () {
		if (transform.position.y < -20)
        {
            transform.position = new Vector3(0, 5, 0);
            rigbod.velocity = Vector3.zero;
            knockbackMod.ResetDamage();
        }
	}
}