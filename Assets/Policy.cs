using UnityEngine;

[CreateAssetMenu(fileName = "Policy", menuName = "Scriptable Objects/Policy")]
public class Policy : ScriptableObject
{
    public string name;
    public float populatity;
    public float bias;

    public void Init(string s, float pop, float b)
    {
        name = s;
        populatity = pop;
        bias = b;

    }

    public static Policy NewPolicy(string s, float pop, float b)
    {
        Policy r = ScriptableObject.CreateInstance<Policy>();
        r.Init(s, pop, b);
        return r;
    }

    public void Pass()
    {
        
    }
}
