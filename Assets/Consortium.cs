using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Consortium : MonoBehaviour
{
    public static Consortium me;
    public List<Advisor> council = new List<Advisor>();

    public bool meeting;

    public List<Transform> positions = new List<Transform>();
    public List<SpriteRenderer> Sprites = new List<SpriteRenderer>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        me = this;
        Dismiss();
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
        meeting = true;
        Sprites[0].gameObject.SetActive(true);
    }

    public void Dismiss()
    {
        meeting = false;
        Sprites[0].gameObject.SetActive(false);
    }

    public void Select(int i)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
