using System.Collections;
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

    public Material buildingMat;

    private bool toggleStage;


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


        if (Input.GetKeyDown("space"))
        {
            toggleStage ^= true;
            Mask4_Event.transform.DOScale(new Vector3(0.36f, 0, 0.1971832f),2);
            Mask2_Interactive.transform.DOScale(new Vector3(0, 0, 0), 1);
        }


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

                            Invoke("activateMask3", 2f);
                        }

                        if(hitName == "XR")
                        {
                            activateXR();
                            Debug.Log("HEJ");
                        }


                        if(hitName == "InteractiveNatural")
                        {

                            Invoke("activateNatural",1.4f);
                            Mask3_XR.transform.DOScale(new Vector3(0, 0, 0), 3);

                        }
                    }




                }


            }
        }

    }

    public void goBack()
    {
        anim.SetTrigger("Back");
        Mask2_Interactive.transform.DOScale(new Vector3(0.370707f, 0.3977927f, 0.3811532f), 2);
        Mask3_XR.transform.DOScale(new Vector3(0.303488f, 0.7190667f, 0.5058357f), 2);

        
        oldHitName = null;
    }


    public void activateMask3()
    {
        CameraMask1 .SetActive(true);
        Mask2_Interactive.transform.DOScale(new Vector3(0.370707f, 0.3977927f, 0.3811532f), 2);
        Mask3_XR.transform.DOScale(new Vector3(0.303488f, 0.7190667f, 0.5058357f), 2);
        buildingMat.DOColor(Color.white, 2);
    }

    public void activateMask2()
    {

        Mask2_Interactive.transform.DOScale(new Vector3(0.370707f, 0.3977927f, 0.3811532f), 2);
        Mask3_XR.transform.DOScale(new Vector3(0, 0, 0), 2);
    }


    public void activateXR()
    {

        //CameraMask3.transform.DOScale(new Vector3(0.303488f, 0.7190667f, 0.5058357f), 2);
        // CameraMask1.transform.DOScale(new Vector3(0, 0, 0), 2);
    }


    public void activateEvent()
    {


    }

    public void activateInteractiveBlue()
    {


    }

    public  void activateNatural()
    {

        CameraMask2.SetActive(true);


    }



}
