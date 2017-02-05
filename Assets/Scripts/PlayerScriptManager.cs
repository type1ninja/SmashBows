using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerScriptManager : NetworkBehaviour {

    private RigidbodyFPS rigbodFPS;
    private SimpleSmoothMouseLook mouseLook;
    //private ArrowShooter arrowShoot;
    private Death death;

    private AudioListener audioListen;
    private Camera cam;
    private FlareLayer flare;
    private GUILayer guiLayer;

    public override void OnStartLocalPlayer()
    {
        rigbodFPS = GetComponentInChildren<RigidbodyFPS>();
        mouseLook = GetComponentInChildren<SimpleSmoothMouseLook>();
        //arrowShoot = GetComponentInChildren<ArrowShooter>();
        //death = GetComponentInChildren<VoidDeath>();

        audioListen = GetComponentInChildren<AudioListener>();
        cam = GetComponentInChildren<Camera>();
        flare = GetComponentInChildren<FlareLayer>();
        guiLayer = GetComponentInChildren<GUILayer>();

        rigbodFPS.enabled = true;
        mouseLook.enabled = true;
        //arrowShoot.enabled = true;
        //death.enabled = true;

        audioListen.enabled = true;
        cam.enabled = true;
        flare.enabled = true;
        guiLayer.enabled = true;
    }
}