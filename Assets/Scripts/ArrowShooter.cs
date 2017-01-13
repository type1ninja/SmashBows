using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowShooter : MonoBehaviour {

    public GameObject ARROW_PREFAB;
    private Slider chargeSlider;

    //the local offset from the player's head the arrow is spawned at
    private static Vector3 ARROW_SPAWN_OFFSET = new Vector3(0, 0, 2);
    //the default arrow velocity
    private static Vector3 ARROW_SPAWN_VELOCITY_DEFAULT = new Vector3(0, 0, 50);
    //Time in seconds--if you don't charge for this long, your arrow is penalized
    private static float CHARGE_THRESHOLD_MEDIUM = .5f;
    //Power value for low-charge arrows
    private static float LOW_CHARGE_POWER_VALUE = .2f;

    private float chargeTime = 0;
    private float charge = 0;

    private void Start()
    {
        chargeSlider = GameObject.Find("HUDCanvas").transform.FindChild("ChargeSlider").GetComponent<Slider>();
    }
    
    private void FixedUpdate()
    {
        if (Input.GetButton("PrimaryFire"))
        {
            chargeTime += Time.fixedDeltaTime;

            //Very important equation to determine power (credit to my bro Tanner for helping me out with this)
            //Important points (LOW_CHARGE_POWER_VALUE is abbreviated LCPV):
            //At charge = LCPV, the value is 1
            //At charge = 2 * LCPV, the value is 5/3, or about 1.667
            //At increasingly large charges, the value goes to 3
            charge = (-chargeTime / (chargeTime - (LOW_CHARGE_POWER_VALUE / 2))) + 3;
            //Update the slider
            chargeSlider.value = charge;
        }

        if (Input.GetButtonUp("PrimaryFire"))
        {
            GameObject arrow = Instantiate(ARROW_PREFAB, transform.TransformPoint(ARROW_SPAWN_OFFSET), transform.rotation);

            //If you haven't met the charge threshold
            if (chargeTime < CHARGE_THRESHOLD_MEDIUM)
            {
                arrow.GetComponent<Rigidbody>().AddRelativeForce(ARROW_SPAWN_VELOCITY_DEFAULT * LOW_CHARGE_POWER_VALUE, ForceMode.VelocityChange);
                arrow.GetComponent<Arrow>().SetPower(LOW_CHARGE_POWER_VALUE);
            } else //If you HAVE met the charge threshold
            {
                arrow.GetComponent<Rigidbody>().AddRelativeForce(ARROW_SPAWN_VELOCITY_DEFAULT * charge, ForceMode.VelocityChange);
                arrow.GetComponent<Arrow>().SetPower(charge);
            }

            chargeTime = 0;
            charge = 0;
        }
    }
}