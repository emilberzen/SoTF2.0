using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HoverManager : MonoBehaviour
{

    private string hitName;
    private string oldHitName;
    private TextMeshPro uiText;
    private bool isHovering;
    private GameObject ActiveSprite;

    private int currentImage;

    [SerializeField]
    public GameObject[] stages;

    [SerializeField]
    public Image [] uiImages;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            hitName = hit.transform.gameObject.name;

            for (int i = 0; i < stages.Length; i++)
            {

                if (hitName == stages[i].name && !isHovering)
                {
                    oldHitName = hitName;


                    //ActiveSprite = hit.transform.gameObject;
                   // ActiveSprite.GetComponentInChildren<SpriteRenderer>().DOFade(1, 1);

                    uiImages[i].DOFade(1, 1);
                    currentImage = i; 

                    Debug.Log("FadeIn" + hitName + " " + oldHitName);

                    isHovering = true;

                }
                else if( oldHitName != hitName && isHovering)
                {
                    Debug.Log("Fade out" + " // " + stages[i].name);
                   // ActiveSprite.GetComponentInChildren<SpriteRenderer>().DOFade(0, 1);
                    uiImages[currentImage].DOFade(0, 1);

                    isHovering = false;
                }


            }



        }
    }

    }



