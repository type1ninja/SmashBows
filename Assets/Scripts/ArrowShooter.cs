using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter : MonoBehaviour {
    public GameObject ARROW_PREFAB;
    private static Vector3 ARROW_SPAWN_OFFSET = new Vector3(0, 0, 2);
    private static Vector3 ARROW_SPAWN_FORCE = new Vector3(0, 0, 30);

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("PrimaryFire"))
        {
            GameObject arrow = Instantiate(ARROW_PREFAB, transform.TransformPoint(ARROW_SPAWN_OFFSET), transform.rotation);
            arrow.GetComponent<Rigidbody>().AddRelativeForce(ARROW_SPAWN_FORCE, ForceMode.Impulse);
        }
    }
}