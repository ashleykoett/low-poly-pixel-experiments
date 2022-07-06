using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRenderer : MonoBehaviour
{
    public bool isEnabled = true;
    public Renderer rend;


    private void Start()
    {
        rend = GetComponent<Renderer>(); 
    }

    public void Disable()
    {
        rend.enabled = false;
    }

    public void Enable()
    {
        rend.enabled = true;
    }
}
