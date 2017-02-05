using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PauseMenu : MonoBehaviour {

    private GameObject HUDCanvas;
    private GameObject pausePanel;
    NetworkManager netMan;

    private void Start()
    {
        HUDCanvas = GameObject.Find("HUDCanvas");
        pausePanel = transform.Find("PausePanel").gameObject;
        netMan = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();

        pausePanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            pausePanel.SetActive(!pausePanel.activeSelf);
            HUDCanvas.SetActive(!pausePanel.activeSelf);

            if (pausePanel.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            } else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        HUDCanvas.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Disconnect()
    {
        if (Network.isClient)
        {
            netMan.StopClient();
        } else
        {
            netMan.StopHost();
        }
    }
}