using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PolicyManager : MonoBehaviour
{
    public TMP_Text[] text;

    public GameObject descriptor;

    public static PolicyManager me;

    public List<Technology> techs = new List<Technology>();

    public List<string> purchasedStuff = new List<string>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        me = this;
        UpdateAllTechs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAllTechs()
    {
        foreach(Technology t in techs)
        {
            bool a = true;
            t.shown = false;
            foreach(string s in t.prereqs)
            {
                if (!purchasedStuff.Contains(s))
                {
                    a = false;
                } else
                {
                    t.shown = true;
                }
            }
            t.shown = t.shown || a;
            t.purchasable = a;
            t.UpdateThis();
        }
    }

    public void Hover(Technology s)
    {
        text[0].text = s.title;
        text[1].text = s.description;
        descriptor.SetActive(true);
    }
    public void Unhover()
    {
        text[0].text = "";
        text[1].text = "";
        descriptor.SetActive(false);
    }
}
