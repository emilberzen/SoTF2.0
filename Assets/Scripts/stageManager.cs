﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
using UnityEngine.Assertions.Must;
using TMPro;

public class stageManager : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator anim;
    public Animation frontDoor;
    private string hitName;
    private string oldHitName;

    public TextMeshProUGUI backButton;
    public TextMeshProUGUI RandomStudio;

    public GameObject CameraMask1;
    public GameObject CameraMask2;
    public GameObject CameraMask3;
    public GameObject CameraMask4;
    public GameObject CameraMask5;

    public GameObject Mask1_OutDoors;
    public GameObject Mask2_Interactive;
    public GameObject Mask3_XR;
    public GameObject Mask4_Event;
    public GameObject Mask5_WhiteBlue;


    public GameObject[] HoverObjects_Blue;

    public GameObject[] HoverObjects_natural;

    public GameObject hoverClothes;
    public GameObject hoverEvent;

    private bool inStore_active;
    private bool XR_active;
    private bool natural_active;
    private bool whiteBlue_active;
    private bool Clothes_active;

    private bool event_active;



    [SerializeField]
    public Light[] lights;


    //public Animation doorAnim;
    [SerializeField]
    public GameObject[] Stages;
    void Start()
    {
        anim = Camera.main.GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("TESTING THISS!!");
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
                            frontDoor.Play("FrontWindowAnim");
                            inStore_active = true;
                            backButton.DOFade(1, 2);
                            RandomStudio.DOFade(0, 1);
                            Debug.Log("hellp");
                            Invoke("actvateStore", 2f);
                        }


                        if(hitName == "XR")
                        {
                            XR_active = true;
                            inStore_active = false;
                            Invoke("activateXR", 2);
                        }

                        
                        if(hitName == "InteractiveNatural")
                        {

                            Invoke("activateNatural",1.35f);
                            natural_active = true;
                            inStore_active = false;

                        }

                        if (hitName == "InteractiveBlue/White")
                        {
                            Mask5_WhiteBlue.transform.DOScale(new Vector3(1f, 0, 0.428295f), 2);
                            natural_active = false;
                            whiteBlue_active = true;

                            for (int j = 0; j < HoverObjects_natural.Length; j++)
                            {
                                HoverObjects_natural[j].GetComponent<Collider>().enabled = false;
                            }

                            Invoke("activateInteractiveBlue",2);

                        }

                        if(hitName == "InteractiveClothes")
                        {
                            CameraMask1.transform.localScale = new Vector3(0, 0, 0);
                            CameraMask2.transform.localScale = new Vector3(0, 0, 0);
                            CameraMask5.transform.localScale = new Vector3(1, 1, 1);

                            Invoke("activateClothes", 1);
                        }

                        if(hitName == "Event")
                        {
                            Invoke("activateEventHover",1);
                            for (int j = 0; j < HoverObjects_natural.Length; j++)
                            {
                                HoverObjects_natural[j].GetComponent<Collider>().enabled = false;
                            }
                            activateEvent();
                        }
                    }
                }
            }
        }
    }

    public void activateClothes()
    {
        hoverClothes.SetActive(true);
        for (int j = 0; j < HoverObjects_Blue.Length; j++)
        {
            HoverObjects_Blue[2].SetActive(false);
        }
        Clothes_active = true;

    }

    public void activateEventHover()
    {
        hoverEvent.SetActive(true);
    }
   
    public void goBack()
    {


        if (inStore_active)
        {
            anim.SetTrigger("Back");
            inStore_active = false;
            frontDoor.Play("FrontWindowAnimExit");
            Mask2_Interactive.transform.DOScale(new Vector3(0, 0, 0), 2);
            Mask3_XR.transform.DOScale(new Vector3(0, 0, 0), 2);
            CameraMask1.transform.DOScale(new Vector3(0, 0, 0), 1.5f);
            backButton.DOFade(0, 1);
            RandomStudio.DOFade(1, 2);
            Debug.Log("Instore");
        }


        if (XR_active)
        {
            anim.SetTrigger("Back");

            CameraMask3.transform.DOScale(new Vector3(0, 0, 0), 1);
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

        if (whiteBlue_active && !Clothes_active)
        {

            for (int i = 0; i < HoverObjects_Blue.Length; i++)
            {
                HoverObjects_Blue[i].GetComponent<Collider>().enabled = false;
            }
            
            Mask5_WhiteBlue.transform.DOScale(new Vector3(0.1316716f, 0, 0.21f), 2);
            CameraMask5.transform.localScale = new Vector3(0, 0, 0);


            for (int j = 0; j < HoverObjects_natural.Length; j++)
            {
                HoverObjects_natural[j].GetComponent<Collider>().enabled = true;
            }

            natural_active = true;
            whiteBlue_active = false;
            Debug.Log("WHITE/BLUE EXIT");

        }

        if (Clothes_active)
        {
            anim.SetTrigger("Back");
            Invoke("activateCameraMasks", 2);
            hoverClothes.SetActive(false);
            for (int j = 0; j < HoverObjects_Blue.Length; j++)
            {
                HoverObjects_Blue[2].SetActive(true);
            }
            
            Clothes_active = false;

        }

        if (event_active)
        {

            Mask5_WhiteBlue.transform.DOScale(new Vector3(0.1316716f, 0, 0.21f), 1);
            Mask4_Event.transform.DOScale(new Vector3(0.1316716f, 0, 0.21f), 1);

            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].DOIntensity(0.1f, 1);
                lights[i].DOColor(Color.white, 2);
            }



            for (int j = 0; j < HoverObjects_natural.Length; j++)
            {
                HoverObjects_natural[j].GetComponent<Collider>().enabled = true;
            }

            hoverEvent.SetActive(false);
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




    public void activateXR()
    {

        CameraMask3.transform.DOScale(new Vector3(1, 1, 1), 2);
    }


    public void activateEvent()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].DOIntensity(2, 2);
            lights[i].DOColor(Color.red, 2);
        }

        natural_active = false;
        event_active = true;
        Mask5_WhiteBlue.transform.DOScale(new Vector3(0, 0, 0), 0.2f);
        Mask4_Event.transform.DOScale(new Vector3(1f, 0, 0.372834f), 2);


    }

    public void activateInteractiveBlue()
    {
        Mask5_WhiteBlue.SetActive(true);



        for (int i = 0; i < HoverObjects_Blue.Length; i++)
        {
            HoverObjects_Blue[i].GetComponent<Collider>().enabled = true;
        }

    }

    public void activateCameraMasks()
    {

        CameraMask1.transform.localScale = new Vector3(1, 1, 1);
        CameraMask2.transform.localScale = new Vector3(1, 1, 1);

    }

    public  void activateNatural()
    {

        CameraMask2.transform.localScale = new Vector3(1, 1, 1);
        Mask5_WhiteBlue.transform.DOScale(new Vector3(0.1316716f, 0, 0.21f), 2);
        Mask4_Event.transform.DOScale(new Vector3(0.1316716f, 0, 0.21f), 2);


    }



}
