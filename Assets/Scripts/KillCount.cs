using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCount : MonoBehaviour {
    private int killCount;

    Text killCountText;

    private void Start()
    {
        killCountText = GameObject.Find("HUDCanvas").transform.Find("KillCountBackground").Find("KillCountText").GetComponent<Text>();
        killCountText.text = "Frags: 0";
    }

    public void AddKill()
    {
        killCount++;
        killCountText.text = "Frags: " + killCount;
    }

    public void AddKill(int killAmount)
    {
        killCount += killAmount;
        killCountText.text = "Frags: " + killCount;
    }
}