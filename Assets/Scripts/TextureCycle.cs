using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureCycle : MonoBehaviour
{
    [SerializeField]
    public Texture2D[] Images;
    private int delay = 5;
    private int textureCounter = 0;


    private void Start()
    {
        InvokeRepeating("CycleTextures", delay, delay);
    }


    private void CycleTextures()
    {
        textureCounter = ++textureCounter % Images.Length;
        GetComponent<Renderer>().material.mainTexture = Images[textureCounter];
    }
}
