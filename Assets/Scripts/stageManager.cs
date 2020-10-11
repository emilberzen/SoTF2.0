﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
using UnityEngine.Assertions.Must;

public class stageManager : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator anim;
    private string hitName;
    private string oldHitName;

    public GameObject CameraMask1;
    public GameObject CameraMask2;
    public GameObject CameraMask3;

    public GameObject Mask1_OutDoors;
    public GameObject Mask2_Interactive;
    public GameObject Mask3_XR;
    public GameObject Mask4_Event;
    public GameObject Mask5_WhiteBlue;


    public GameObject HoverObjects;
        
    private bool inStore_active;
    private bool XR_active;
    private bool natural_active;
    private bool whiteBlue_active;
    private bool event_active;



    [SerializeField]
    public Light[] lights;

    //public Animation doorAnim;
    [SerializeField]
    public GameObject[] Stages;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                
                hitName = hit.transform.gameObject.name;
                Debug.Log(hitName);
                for (int i = 0; i < Stages.Length; i++)
                {

                    if (hitName == Stages[i].name && hitName != oldHitName)
                    {

                        anim.SetTrigger(hitName);                        
                        oldHitName = hitName;


                        if (hitName == "Outdoor")
                        {
                            inStore_active = true; 
                            Invoke("actvateStore", 2f);
                        }


                        if(hitName == "XR")
                        {
                            XR_active = true;
                            inStore_active = false;
                            activateXR();
                        }

                        
                        if(hitName == "InteractiveNatural")
                        {

                            Invoke("activateNatural",1.35f);
                            natural_active = true;
                            inStore_active = false;

                        }

                        if (hitName == "InteractiveBlue/White")
                        {
                            Mask5_WhiteBlue.transform.DOScale(new Vector3(0.7875425f, 0, 0.428295f), 2);
                            natural_active = false;
                            whiteBlue_active = true;
                            Invoke("activateInteractiveBlue",2);

                        }

                        if(hitName == "Event")
                        {

                            activateEvent();
                        }
                    }
                }
            }
        }
    }

    public void goBack()
    {

        Debug.Log("HELLO");

        if (inStore_active)
        {
            anim.SetTrigger("Back");
            Mask2_Interactive.transform.DOScale(new Vector3(0, 0, 0), 2);
            Mask3_XR.transform.DOScale(new Vector3(0, 0, 0), 2);
            CameraMask1.transform.DOScale(new Vector3(0, 0, 0), 1.5f);
            inStore_active = false;
            Debug.Log("Instore");
        }


        if (XR_active)
        {
            anim.SetTrigger("Back");
            inStore_active = true;
            natural_active = false;
            XR_active = false;

            Debug.Log("XR");
        }

        if (natural_active)
        {
            anim.SetTrigger("Back");
            Mask2_Interactive.transform.DOScale(new Vector3(0.370707f, 0.3977927f, 0.3811532f), 2);
            Mask3_XR.transform.DOScale(new Vector3(0.303488f, 0.7190667f, 0.5058357f), 2);
            Mask4_Event.transform.DOScale(new Vector3(0, 0, 0), 2);
            Mask5_WhiteBlue.transform.DOScale(new Vector3(0, 0, 0), 2);
            CameraMask2.transform.DOScale(new Vector3(0,0,0),2.5f);
            natural_active = false;
            inStore_active = true;
            Debug.Log("NATURAL_EXIT");
        }

        if (whiteBlue_active)
        {

            HoverObjects.GetComponentInChildren<Collider>().enabled = false;
            Mask5_WhiteBlue.transform.DOScale(new Vector3(0.1316716f, 0, 0.21f), 2);
            natural_active = true;
            whiteBlue_active = false;
            Debug.Log("WHITE/BLUE EXIT");

        }

        if (event_active)
        {

            Mask5_WhiteBlue.transform.DOScale(new Vector3(0.1316716f, 0, 0.21f), 1);
            Mask4_Event.transform.DOScale(new Vector3(0.1316716f, 0, 0.21f), 1);

            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].DOIntensity(1, 2);
                lights[i].DOColor(Color.white, 2);
            }

            natural_active = true;
            event_active = false;
            Debug.Log("Event");
        }


        oldHitName = null;
    }




    public void WhiteStore()
    {



    }



    public void actvateStore()
    {
        CameraMask1.transform.DOScale(new Vector3(1, 1, 1), 0);
        Mask2_Interactive.transform.DOScale(new Vector3(0.370707f, 0.3977927f, 0.3811532f), 2);
        Mask3_XR.transform.DOScale(new Vector3(0.303488f, 0.7190667f, 0.5058357f), 2);
    }

    public void activateMask2()
    {

        Mask2_Interactive.transform.DOScale(new Vector3(0.370707f, 0.3977927f, 0.3811532f), 2);
        Mask3_XR.transform.DOScale(new Vector3(0, 0, 0), 2);
    }


    public void activateXR()
    {

 
    }


    public void activateEvent()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].DOIntensity(20, 2);
            lights[i].DOColor(Color.red, 2);
        }

        natural_active = false;
        event_active = true;
        Mask5_WhiteBlue.transform.DOScale(new Vector3(0, 0, 0), 0.2f);
        Mask4_Event.transform.DOScale(new Vector3(0.8196557f, 0, 0.372834f), 2);


    }

    public void activateInteractiveBlue()
    {
        Mask5_WhiteBlue.SetActive(true);
        HoverObjects.GetComponentInChildren<Collider>().enabled = true;
    }

    public  void activateNatural()
    {

        CameraMask2.transform.localScale = new Vector3(1, 1, 1);
        Mask5_WhiteBlue.transform.DOScale(new Vector3(0.1316716f, 0, 0.21f), 2);
        Mask4_Event.transform.DOScale(new Vector3(0.1316716f, 0, 0.21f), 2);


    }



}
