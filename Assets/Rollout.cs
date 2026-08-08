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
            if(Name.Equals("Roads I"))
            {
                location.totalInfra += 0.1f;
                location.localSupportDelta += 0.01f;
            }
            if(Name.Equals("Electrical I"))
            {
                location.totalInfra += (location.size * 0.02f);

                location.localSupportDelta += (location.size * 0.01f);
            }
            if(Name.Equals("Water I"))
            {
                location.totalInfra += 0.2f;
                location.localSupportDelta += 0.01f;
            }
            if(Name.Equals("Internet I"))
            {
                List<State> states = Nation.me.GetAllStatesWithTech("Internet I");
                foreach(State s in states)
                {
                    s.totalInfra += 0.01f;
                    s.localSupportDelta += 0.0025f;
                }
                location.totalInfra += 0.01f;
                location.localSupportDelta += 0.0025f;
            }
            if(Name.Equals("Internet II"))
            {
                List<State> states = Nation.me.GetAllStatesWithTech("Internet I");
                foreach(State s in states)
                {
                    s.totalInfra += 0.0025f;
                    s.localSupportDelta += 0.0025f;
                }
                List<State> states2 = Nation.me.GetAllStatesWithTech("Internet II");
                foreach(State s in states2)
                {
                    s.totalInfra += 0.02f;
                    s.localSupportDelta += 0.01f;
                }
                location.totalInfra += 0.05f;
                location.localSupportDelta += 0.01f;
            }
            if(Name.Equals("Housing I"))
            {
                location.totalInfra += (location.size * 0.01f) + 0.075f;
                location.localSupportDelta += 0.02f;
            }
            if(Name.Equals("Rail I"))
            {
                location.totalInfra += 0.1f;
                location.localSupportDelta += 0.01f;
            }
            if(Name.Equals("Education I"))
            {
                location.totalInfra += (location.size * 0.02f) + 0.075f;
                location.localSupportDelta += 0.03f;
            }
            if(Name.Equals("Health I"))
            {
                location.totalInfra += 0.0f;
                location.localSupportDelta += 0.04f;
            }


            if(Name.Equals("hotel"))
            {
                location.totalInfra += 0.1f;
                location.localSupportLevel += 0.01f;
                location.localSupportDelta += 0.02f;
            }
            if(Name.Equals("hospital"))
            {
                location.totalInfra += 0.025f;
                location.localSupportLevel += 0.09f;
                location.localSupportDelta += 0.03f;
            }
            if(Name.Equals("school"))
            {
                location.totalInfra += 0.05f;
                location.localSupportLevel += 0.08f;
                location.localSupportDelta += 0.03f;
            }
        }
        //national stuff down here
        if(Name.Equals("Nuclear I"))
            {
                Nation.me.nationalSpendingBase += 0.1f;
            }
    }


}
