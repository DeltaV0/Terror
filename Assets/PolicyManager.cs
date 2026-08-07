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

    public GameObject[] panels;

    public int PanelOpen;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        me = this;
        UpdateAllTechs();
        SetOpenPanel(0);
        Unhover();
    }

    public void SetOpenPanel(int q)
    {
        for(int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
        panels[q].SetActive(true);
        PanelOpen = q;
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
                    if(!CheckIfTechComplete(s)){
                        a = false;
                    }
                    t.shown = true;
                }
            }
            t.shown = t.shown || a;
            t.purchasable = a;
            t.UpdateThis();
        }
    }

    public bool CheckIfTechComplete(string s)
    {
       foreach(Rollout r in Nation.me.nationalRollouts)
        {
            //Debug.Log(r.name + " prog: " + r.Cprogress);

            if(r.Name.Equals(s) && r.Cprogress <= 0)
            {
                return true;
            }
        }
        return false;
    }

    public void Hover(Technology s)
    {
        text[0].text = s.title;
        text[1].text = s.description;
        descriptor.SetActive(true);
        if(!s.purchased){
        if(s.purchasable){
            string str = "Costs " + s.cost + " per province per month for " + s.time + " months";
            if(s.initTime > 0)
            {
                str += "\ntakes " + s.initTime + " months to plan, at " + s.initCostOverTime + " per month";
            }
            if(s.initCost > 0)
            {
                str += "\nCosts " + s.initCost + " immediately!";
            }
        CameraMover.me.HoverOverSomething(str);
        } else
        {
            string str = "Requires: ";
            foreach(string preq in s.prereqs)
            {
                str += preq + ", ";
            }
            str += "\nAll prerequisites must be nationally rolled out before buying!!";
             CameraMover.me.HoverOverSomething(str);
        }
        } else
        {
            CameraMover.me.HoverOverSomething("Already have this!");
        }
    }
    public void Unhover()
    {
        text[0].text = "";
        text[1].text = "";
        descriptor.SetActive(false);
        CameraMover.me.StopHovering();
    }
}
