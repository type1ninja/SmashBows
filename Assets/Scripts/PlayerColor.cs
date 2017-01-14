using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour {

    private Renderer myRenderer;

    private Color currentColor = new Color(0, 1, 0);

    private void Start()
    {
        myRenderer = GetComponent<Renderer>();
        myRenderer.material.color = currentColor;
    }

    public void TakeColorDamage(float modifier) {
        currentColor.r = Mathf.Clamp(modifier - 100, 0, 255) / 255;
        currentColor.g = Mathf.Clamp(255 - (currentColor.r * 255), 0, 255) / 255 ;
        currentColor.b = 0;
        myRenderer.material.color = currentColor;
    }

    public void ResetColor()
    {
        currentColor = new Color(0, 1, 0);
        myRenderer.material.color = currentColor;
    }
}