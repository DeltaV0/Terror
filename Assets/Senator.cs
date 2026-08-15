using UnityEngine;

[CreateAssetMenu(fileName = "Senator", menuName = "Scriptable Objects/Senator")]
public class Senator : ScriptableObject
{
    public State home;
    public float leftrightStanding;

    public bool aye;

    public void Init(State h, float lrSt)
    {
        home = h;
        leftrightStanding = Mathf.Clamp(lrSt,-1,1);
    }

    public static Senator NewSenator(State h, float lrSt)
    {
        Senator r = ScriptableObject.CreateInstance<Senator>();
        r.Init(h, lrSt);
        return r;
    }

    public float CalculateVotingChance(Policy p)
    {
        return ((p.populatity-(Mathf.Abs(leftrightStanding-p.bias)*p.bias))/p.populatity) - (1-p.populatity) + (p.populatity*p.bias*leftrightStanding);
    }
}
