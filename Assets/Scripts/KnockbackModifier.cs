using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnockbackModifier : MonoBehaviour {

    Text healthText;

    static float ARROW_DAMAGE = 10f;
    
    //In hundreds, divide by 100 when we do the actual modifying
    private float modifier = 100f;

    private void Start()
    {
        healthText = GameObject.Find("HUDCanvas").transform.FindChild("HealthBackground").FindChild("HealthText").GetComponent<Text>();
    }

    private void Update()
    {
        if (gameObject.tag.Equals("Player"))
        {
            healthText.text = modifier.ToString();
        }
    }

    public void TakeDamage(float damage)
    {
        modifier += damage;
    }

    public void ResetDamage()
    {
        modifier = 100f;
    }

    public float GetKnockbackModifier()
    {
        return modifier / 100;
    }
}