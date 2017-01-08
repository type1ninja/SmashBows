using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour {

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(-Vector3.forward);
    }
}