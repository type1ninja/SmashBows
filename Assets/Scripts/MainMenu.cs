using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MainMenu : NetworkBehaviour {
    
    //Button groups
    private GameObject joinBtns;
    private GameObject hostBtns;
    private GameObject mainBtns;

    //Join inputs
    Text joinIPText;
    Text joinPortText;
    Text joinPasswordText;

    //Host inputs
    Text hostPortText;
    Text hostPasswordText;

    NetworkManager netMan;

    private void Start()
    {
        //Get button groups
        joinBtns = transform.Find("JoinButtons").gameObject;
        hostBtns = transform.Find("HostButtons").gameObject;
        mainBtns = transform.Find("MainButtons").gameObject;

        //Set only main menu button group active
        joinBtns.SetActive(false);
        hostBtns.SetActive(false);
        mainBtns.SetActive(true);

        //Get join inputs
        joinIPText = transform.Find("JoinButtons").Find("IPField").Find("Text").GetComponent<Text>();
        joinPortText = transform.Find("JoinButtons").Find("PortField").Find("Text").GetComponent<Text>();
        joinPasswordText = transform.Find("JoinButtons").Find("PasswordField").Find("Text").GetComponent<Text>();

        //Get host inputs
        hostPortText = transform.Find("HostButtons").Find("PortField").Find("Text").GetComponent<Text>();
        hostPasswordText = transform.Find("HostButtons").Find("PasswordField").Find("Text").GetComponent<Text>();

        //Get the network manager
        netMan = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    private void Update()
    {
        if (Input.GetButton("Cancel"))
        {
            if (!mainBtns.activeSelf)
            {
                ReturnToMain();
            }
        }
    }

    //Start: Menu Navigation
    public void OpenJoinBtns()
    {
        joinBtns.SetActive(true);

        hostBtns.SetActive(false);
        mainBtns.SetActive(false);
    }

    public void OpenHostBtns()
    {
        hostBtns.SetActive(true);

        joinBtns.SetActive(false);
        mainBtns.SetActive(false);
    }

    public void ReturnToMain()
    {
        mainBtns.SetActive(true);

        joinBtns.SetActive(false);
        hostBtns.SetActive(false);
    }
    //End: Menu Navigation 
    
    //Start: Hosting/Joining/Quitting
    public void HostGame()
    {
        netMan.networkPort = int.Parse(hostPortText.text);

        netMan.StartHost();
    }

    public void JoinGame()
    {
        netMan.networkAddress = joinIPText.text;
        netMan.networkPort = int.Parse(joinPortText.text);

        netMan.StartClient();
    }

    public void Quit()
    {
        Application.Quit();
    }
}