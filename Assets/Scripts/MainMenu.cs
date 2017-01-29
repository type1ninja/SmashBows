using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    private GameObject joinBtns;
    private GameObject hostBtns;
    private GameObject mainBtns;

    private void Start()
    {
        joinBtns = transform.Find("JoinButtons").gameObject;
        hostBtns = transform.Find("HostButtons").gameObject;
        mainBtns = transform.Find("MainButtons").gameObject;

        joinBtns.SetActive(false);
        hostBtns.SetActive(false);
        mainBtns.SetActive(true);
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

    public void Quit()
    {
        Application.Quit();
    }
}