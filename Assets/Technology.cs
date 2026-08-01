using UnityEngine;
using System.Collections.Generic;


public class Technology : MonoBehaviour
{
    public string name;
    public List<string> prereqs = new List<string>();

    public float cost;
    public int time;

    public bool purchased;

    public void Buy()
    {
        purchased = true;
        Nation.me.nationalRollouts.Add(Rollout.NewRollout(name, prereqs, time, cost));
    }
}
