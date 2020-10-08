using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class stageManager : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator anim;
    private string hitName;
    private string oldHitName;

    public GameObject CameraMask1;
    public GameObject CameraMask2;
    public GameObject CameraMask3;

    public GameObject Mask1_XR;
    public GameObject Mask2_Interactive;
    public GameObject Mask3_OutDoors;
    public GameObject Mask4;

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
            Mask4.transform.DOScale(new Vector3(0.36f, 0, 0.1971832f),2);
            Mask2_Interactive.transform.DOScale(new Vector3(0, 0, 0), 1);
        }


        if (Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                
                hitName = hit.transform.gameObject.name;
                for (int i = 0; i < Stages.Length; i++)
                {

                    if (hitName == Stages[i].name && hitName != oldHitName)
                    {

                        anim.SetTrigger(hitName);
                        
                        oldHitName = hitName;

                        if (hitName == "StartAnim")
                        {

                            Invoke("activateMask3", 2f);
                        }


                        if(hitName == "Mask2")
                        {

                            activateMask2();
                        }
                    }




                }


            }
        }

    }

    public void goBack()
    {
        anim.SetTrigger("Back");
        Mask2_Interactive.transform.DOScale(new Vector3(0.276705f, 0.3977927f, 0.3578745f), 2);
        Mask1_XR.transform.DOScale(new Vector3(0.3312956f, 0.5445f, 0.5445f), 2);

        
        oldHitName = null;
    }


    public void activateMask3()
    {
        CameraMask3.SetActive(true);
        Mask2_Interactive.transform.DOScale(new Vector3(0.276705f, 0.3977927f, 0.3578745f), 2);
        Mask1_XR.transform.DOScale(new Vector3(0.4404871f, 0.5445f, 0.5716246f), 2);
        buildingMat.DOColor(Color.white, 2);
    }

    public void activateMask2()
    {

        Mask2_Interactive.transform.DOScale(new Vector3(0.5f, 0.3977927f, 0.3977927f), 2);
        Mask1_XR.transform.DOScale(new Vector3(0, 0, 0), 2);
    }



}
