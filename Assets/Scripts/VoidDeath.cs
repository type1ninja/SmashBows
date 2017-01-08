using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidDeath : MonoBehaviour {

    private Rigidbody rigbod;

    private void Start()
    {
        rigbod = GetComponent<Rigidbody>();
    }

	void FixedUpdate () {
		if (transform.position.y < -20)
        {
            transform.position = new Vector3(0, 5, 0);
            rigbod.velocity = Vector3.zero;
        }
	}
}