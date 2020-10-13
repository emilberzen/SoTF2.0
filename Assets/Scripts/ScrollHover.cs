using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollHover : MonoBehaviour
{
    private string hitName;
    private string oldHitName;
    private bool isHovering;
    private int currentObject;

    [SerializeField]
    public GameObject[] stages;

    [SerializeField]
    public GameObject[] ScrollObject;
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

                    ScrollObject[i].SetActive(true);



                    Debug.Log("FadeIn" + hitName + " " + oldHitName);
                    currentObject = i;

                    isHovering = true;

                }
                else if (oldHitName != hitName && isHovering)
                {
                    Debug.Log("Fade out" + " // " + stages[i].name);

                    ScrollObject[currentObject].SetActive(false);

                    isHovering = false;
                }


            }



        }
    }
}
