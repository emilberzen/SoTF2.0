using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Drawing;

public class OnMouseHover : MonoBehaviour
{

    private bool hoveingEnter;
    public GameObject uiObject;
    public Component[] uiText;

    void OnMouseOver()
        {
        //If your mouse hovers over the GameObject with the script attached, output this message

        if (!hoveingEnter)
        {

            Debug.Log("Mouse is over GameObject.");
            uiObject.GetComponent<Image>().DOFade(0.6f, 1);
            uiText = uiObject.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI t in uiText)
                t.DOFade(1, 1);
            hoveingEnter = true;
        }
            
        }
        
        void OnMouseExit()
        {
        hoveingEnter = false;
            //The mouse is no longer hovering over the GameObject so output this message each frame
            Debug.Log("Mouse is no longer on GameObject.");
        uiObject.GetComponent<Image>().DOFade(0, 1);
        foreach (TextMeshProUGUI t in uiText)
            t.DOFade(0, 1);
    }
    
}
