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

       // start.roads.Add(this);
        //end.roads.Add(this);
        
        road.widthMultiplier = 0.02f * tier;

        Vector2 midpoint = e.collider.ClosestPoint(start.transform.position);

        road.SetPosition(0, start.transform.position);
        road.SetPosition(4, end.transform.position);
        road.SetPosition(2, (Vector3)midpoint);

        road.SetPosition(1, (Vector3)(Vector2.Lerp(start.transform.position, midpoint, 0.5f) + (Vector2.Distance(start.transform.position, midpoint) * Random.insideUnitCircle * 0.25f)));
        road.SetPosition(3, (Vector3)(Vector2.Lerp(end.transform.position, midpoint, 0.5f) + (Vector2.Distance(end.transform.position, midpoint) * Random.insideUnitCircle * 0.25f)));

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(Nation.me.hoverState != null && (Nation.me.hoverState == start || Nation.me.hoverState == end))
        {
            road.sortingOrder = 65;
            transform.SetParent(Nation.me.hoverState.transform);
            transform.localScale = new Vector3(1, 1, 1);
            road.widthMultiplier = Mathf.Lerp(road.widthMultiplier, 0.02f * tier * 3f, 0.1f);

        } else
        {
            road.sortingOrder = 63;
            transform.SetParent(null);
            transform.localScale = new Vector3(1, 1, 1);
            transform.position = new Vector3(0, 0, 0);
            road.widthMultiplier = Mathf.Lerp(road.widthMultiplier, 0.02f * tier, 0.1f);


        }
    }
}
