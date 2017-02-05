using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ArrowShooter : NetworkBehaviour {

    public GameObject ARROW_PREFAB;
    private Slider chargeSlider;
    private Slider chargeSlider2;
    private GameObject pausePanel;
    private Collider myCol;
    private Transform head;
    private Rigidbody rigbod;

    //the local offset from the player's head the arrow is spawned at
    private static Vector3 ARROW_SPAWN_OFFSET = new Vector3(0, 0, 2f);
    //the default arrow velocity
    private static Vector3 ARROW_SPAWN_VELOCITY_DEFAULT = new Vector3(0, 0, 25);
    //Time in seconds--if you don't charge for this long, your arrow is penalized
    private static float CHARGE_THRESHOLD_MEDIUM = .5f;
    //Power value for low-charge arrows
    private static float LOW_CHARGE_POWER_VALUE = .2f;
    //Arrow lifetime in seconds
    private static float ARROW_LIFETIME = 5.0f;

    private float chargeTime = 0;
    private float charge = 0;

    private void Start()
    {
        chargeSlider = GameObject.Find("HUDCanvas").transform.Find("ChargeSlider").GetComponent<Slider>();
        chargeSlider2 = GameObject.Find("HUDCanvas").transform.Find("ChargeSlider2").GetComponent<Slider>();
        pausePanel = GameObject.Find("PauseCanvas").transform.Find("PausePanel").gameObject;
        myCol = GetComponent<Collider>();
        head = transform.Find("PlayerCam");
        rigbod = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isLocalPlayer && !pausePanel.activeSelf)
        {
            if (Input.GetButton("PrimaryFire"))
            {
                chargeTime += Time.fixedDeltaTime;

                //Very important equation to determine power (credit to my bro Tanner for helping me out with this)
                //Important points (LOW_CHARGE_POWER_VALUE is abbreviated LCPV):
                //At charge = LCPV, the value is 1
                //At charge = 2 * LCPV, the value is 5/3, or about 1.667
                //At increasingly large charges, the value goes to 3
                charge = (-chargeTime / (chargeTime - (CHARGE_THRESHOLD_MEDIUM / 2))) + 3;
                //Update the slider
            }

            if (Input.GetButtonUp("PrimaryFire"))
            {
                CmdShoot(head.TransformPoint(ARROW_SPAWN_OFFSET), head.rotation, chargeTime, charge);

                chargeTime = 0;
                charge = 0;
            }
        
            if (chargeTime <= CHARGE_THRESHOLD_MEDIUM / 2)
            {
                chargeSlider.value = 0;
                chargeSlider2.value = 0;
            }
            else
            {
                chargeSlider.value = charge;
                chargeSlider2.value = charge;
            }
        }
    }

    [Command]
    public void CmdShoot(Vector3 pos, Quaternion rot, float chargeTime, float charge)
    {
        GameObject arrow = Instantiate(ARROW_PREFAB, pos, rot);
        Physics.IgnoreCollision(myCol, arrow.GetComponent<Collider>());
        
        //If you haven't met the charge threshold
        if (chargeTime < CHARGE_THRESHOLD_MEDIUM)
        {
            //arrow.GetComponent<Rigidbody>().AddRelativeForce(ARROW_SPAWN_VELOCITY_DEFAULT * LOW_CHARGE_POWER_VALUE, ForceMode.VelocityChange);
            arrow.GetComponent<Rigidbody>().velocity = arrow.transform.TransformDirection(ARROW_SPAWN_VELOCITY_DEFAULT * LOW_CHARGE_POWER_VALUE);// + rigbod.velocity; //inheritance
            arrow.GetComponent<Arrow>().SetPower(LOW_CHARGE_POWER_VALUE);
        }
        else //If you HAVE met the charge threshold
        {
            //arrow.GetComponent<Rigidbody>().AddRelativeForce(ARROW_SPAWN_VELOCITY_DEFAULT * charge, ForceMode.VelocityChange);
            arrow.GetComponent<Rigidbody>().velocity = arrow.transform.TransformDirection(ARROW_SPAWN_VELOCITY_DEFAULT * charge);// + rigbod.velocity; //inheritance
            arrow.GetComponent<Arrow>().SetPower(charge);
        }
        //Temp
        //arrow.GetComponent<Rigidbody>().velocity = arrow.transform.forward * 10 * charge;

        NetworkServer.Spawn(arrow);
        Destroy(arrow, ARROW_LIFETIME);
    }
}