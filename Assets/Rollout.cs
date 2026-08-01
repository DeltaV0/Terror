using UnityEngine;
using System.Collections.Generic;


public class Rollout : ScriptableObject
{
    public string Name;

    public List<string> preRequisites = new List<string>();

    public int progress;

    public float cost;


    public void Init(string s, List<string> prereqs, int t, float c)
    {
        cost = c;
        progress = t;
        preRequisites = prereqs;
        Name = s;
    }

    public static Rollout NewRollout(string s, List<string> prereqs, int t, float c)
    {
        Rollout r = ScriptableObject.CreateInstance<Rollout>();
        r.Init(s, prereqs, t, c);
        return r;
    }

    public static Rollout NewRolloutFromRollout(Rollout roll)
    {
        Rollout r = ScriptableObject.CreateInstance<Rollout>();
        r.Init(roll.Name, roll.preRequisites, roll.progress, roll.cost);
        return r;
    }


}
