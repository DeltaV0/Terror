using UnityEngine;

[CreateAssetMenu(fileName = "Senator", menuName = "Scriptable Objects/Senator")]
public class Senator : ScriptableObject
{
    public State home;
    public float leftrightStanding;

    public void Init(State h, float lrSt)
    {
        home = h;
        leftrightStanding = lrSt;
    }

    public static Senator NewSenator(State h, float lrSt)
    {
        Senator r = ScriptableObject.CreateInstance<Senator>();
        r.Init(h, lrSt);
        return r;
    }
}
