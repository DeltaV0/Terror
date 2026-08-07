using UnityEngine;
using System.Collections.Generic;


public class Rollout : ScriptableObject
{
    public string Name;

    public List<string> preRequisites = new List<string>();

    public int progress;

    public float cost;

    public int Cprogress;

    public float Ccost;

    public float COTcost;
    public bool national;

    public State location;

    //public float NationalSpendingIncrease;


    public void Init(string s, List<string> prereqs, int t, float c, int Ct, float Cc, float COTC, bool nat)
    {
        Cprogress = Ct;
        Ccost = Cc;
        cost = c;
        progress = t;
        preRequisites = prereqs;
        Name = s;
        national = nat;
        COTcost = COTC;
    }

    public static Rollout NewRollout(string s, List<string> prereqs, int t, float c, int Ct, float Cc, float COTC, bool nat)
    {
        Rollout r = ScriptableObject.CreateInstance<Rollout>();
        r.Init(s, prereqs, t, c, Ct, Cc, COTC, nat);
        return r;
    }

    public static Rollout NewRolloutFromRollout(Rollout roll)
    {
        Rollout r = ScriptableObject.CreateInstance<Rollout>();
        r.Init(roll.Name, roll.preRequisites, roll.progress, roll.cost, roll.Cprogress, roll.Ccost, roll.COTcost, roll.national);
        return r;
    }

    public void Complete()
    {
        //state stuff up here
        if(!national){
            if(Name.Equals("Roads Rev"))
            {
                location.totalInfra += 0.1f;
            }
            if(Name.Equals("Railway Rev"))
            {
                location.totalInfra += 0.1f;
            }
        }
        //national stuff down here
        if(Name.Equals("Nuclear I"))
            {
                Nation.me.nationalSpendingBase += 0.1f;
            }
    }


}
