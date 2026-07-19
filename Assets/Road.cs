using UnityEngine;

public class Road : MonoBehaviour
{
    public State start;
    public State end;

    public LineRenderer road;

    public int tier; //0 = dirt, 1 = minor, 2=major

    public void Init(State s, State e, int i)
    {
        tier = i;
        start = s;
        end = e;
        
        road.widthMultiplier = 0.01f * (tier + 1);

        Vector2 midpoint = e.collider.ClosestPoint(start.transform.position);

        road.SetPosition(0, start.transform.position);
        road.SetPosition(4, end.transform.position);
        road.SetPosition(2, (Vector3)midpoint);

        road.SetPosition(1, (Vector3)(Vector2.Lerp(start.transform.position, midpoint, 0.5f) + (Vector2.Distance(start.transform.position, midpoint) * Random.insideUnitCircle * 0.25f)));
        road.SetPosition(3, (Vector3)(Vector2.Lerp(end.transform.position, midpoint, 0.5f) + (Vector2.Distance(end.transform.position, midpoint) * Random.insideUnitCircle * 0.25f)));

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
