using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackModifier : MonoBehaviour {

    static float ARROW_DAMAGE = 25f;
    
    //In hundreds, divide by 100 when we do the actual modifying
    private float modifier = 100f;

    public void TakeDamage()
    {
        modifier += ARROW_DAMAGE;
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