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
    private bool isHovering;
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

                    uiImages[i].DOFade(0.7f, 1);
                    if (uiImages[i].transform.childCount > 0)
                    {
                        uiImages[i].GetComponentInChildren<TextMeshProUGUI>().DOFade(1, 1);
                        // we have children!
                    }


                    Debug.Log("FadeIn" + hitName + " " + oldHitName);
                    currentImage = i;

                    isHovering = true;

                }
                else if( oldHitName != hitName && isHovering)
                {
                    Debug.Log("Fade out" + " // " + stages[i].name);
                    // ActiveSprite.GetComponentInChildren<SpriteRenderer>().DOFade(0, 1);

                    if (uiImages[currentImage].transform.childCount > 0)
                    {
                        uiImages[currentImage].GetComponentInChildren<TextMeshProUGUI>().DOFade(0, 1);
                        // we have children!
                    }
                    uiImages[currentImage].DOFade(0, 1);

                    isHovering = false;
                }


            }



        }
    }
    }



