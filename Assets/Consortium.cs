using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Consortium : MonoBehaviour
{
    public static Consortium me;
    public List<Advisor> Consortium = new List<Advisor>();

    public bool meeting;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void ToggleActive()
    {
        meeting = !meeting;
        if (meeting)
        {
            Activate();
        } else
        {
            Dismiss();
        }
    }

    public void Activate()
    {
        
    }

    public void Dismiss()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
