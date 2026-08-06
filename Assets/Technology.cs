using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class Technology : MonoBehaviour
{
    public string name;
    public List<string> prereqs = new List<string>();

    public float cost;
    public int time;

    public int initTime;
    public float initCost;

    public bool purchased;

    public string title;
    public string description;

    public Technology[] prereqTechs;

    public Button b;
    public bool purchasable;
    public bool shown;

    public Image[] images;

    void Start()
    {
        purchasable = false;
        shown = false;
        foreach(Technology t in prereqTechs)
        {
            prereqs.Add(t.name);
        }
        PolicyManager.me.techs.Add(this);
        PolicyManager.me.UpdateAllTechs();
    }

    public void UpdateThis()
    {
        foreach(Image i in images)
        {
            i.enabled = shown;
        }
       // gameObject.SetActive(false);
        b.interactable = purchasable && !purchased;
    }

    public void Buy()
    {
        purchased = true;
        PolicyManager.me.purchasedStuff.Add(name);
        PolicyManager.me.UpdateAllTechs();
        Nation.me.nationalRollouts.Add(Rollout.NewRollout(name, prereqs, time, cost, initTime, initCost));
    }
}
