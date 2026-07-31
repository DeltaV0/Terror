using UnityEngine;
using System.Collections.Generic;


public class Nation : MonoBehaviour
{

    public List<ForeignNation> NeighborNations = new List<ForeignNation>();

    public List<State> states = new List<State>();

    public List<State> statesNation = new List<State>();


    public GameObject statePf;
    public GameObject roadPf;

    public static Nation me;

    public Transform canvas;

    public List<Road> roads;

    public State selectedState;

    public State hoverState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        me = this;
        for(int i = 0; i < 16; i++)
        {
            GameObject g = Instantiate(statePf, new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f), 0), Quaternion.identity);
            statesNation.Add(g.GetComponent<State>());
            states.Add(g.GetComponent<State>());
            states[i].ID = i;
            states[i].outside = false;
            states[i].init();
            g.transform.SetParent(canvas, true);
            //Debug.Log(i);
        }

        for(int i = 0; i < 8; i++)
        {
            float q = (float)(i / 4f) * Mathf.PI + Random.Range(-0.25f, 0.25f);
            GameObject g = Instantiate(statePf, new Vector3(Random.Range(3f, 5f) * Mathf.Cos(q), Random.Range(3f, 5f) * Mathf.Sin(q), 0), Quaternion.identity);
            states.Add(g.GetComponent<State>());
            states[i+ 16].ID = i + 16;
            states[i+ 16].outside = true;
            states[i+ 16].init();
           g.transform.SetParent(canvas, true);

           // Debug.Log(i + "a");

        }
       // states[17].init();

        for(int i = 0; i < 16; i++)
        {
            float q = Random.Range(0, 2f * Mathf.PI);
            GameObject g = Instantiate(statePf, new Vector3(Random.Range(6f, 10f) * Mathf.Cos(q), Random.Range(6f, 10f) * Mathf.Sin(q), 0), Quaternion.identity);
            states.Add(g.GetComponent<State>());
            states[i+ 24].ID = i + 24;
            states[i+ 24].outside = true;
            states[i+ 24].init();
            g.transform.SetParent(canvas, true);

           // Debug.Log(i + "b");
        }
        /*for(int i = 0; i < states.Count; i++)
        {
            states[i].InitVectorDrawers();
            states[i].RenderLines();
        }*/
        GraphicsRenderer.me.initGraphics();
    }

    public void ConnectRoads()
    {
        for(int i = 0; i < states.Count; i++)
                {
                    for(int q = 0; q < states[i].neighbors.Count; q++)
                    {
                if (!CheckForRoadConnection(states[i].neighbors[q], states[i]))
                {
                    Road r = Instantiate(roadPf, Vector3.zero, Quaternion.identity).GetComponent<Road>();
                    r.Init(states[i], states[i].neighbors[q], Random.Range(0, 3));
                    roads.Add(r);
                }
                    }

                    for(int q = 0; q < states[i].neighborsOutside.Count; q++)
                    {
                if (!CheckForRoadConnection(states[i].neighborsOutside[q], states[i]) && Random.Range(0f, 1f) > 0.5f)
                {
                    Road r = Instantiate(roadPf, Vector3.zero, Quaternion.identity).GetComponent<Road>();
                    r.Init(states[i], states[i].neighborsOutside[q], Random.Range(0, 3));
                    roads.Add(r);
                }
                    }
                }
    }

    public bool CheckForRoadConnection(State s, State s2)
    {
        for(int i = 0; i < roads.Count; i++)
        {
            if(roads[i].start == s && roads[i].end == s2 || roads[i].start == s2 && roads[i].end == s)
            {
                return true;
            }
        }
        return false;
    }

    /*public State getStateFromColour(Color col)
    {
        for(int i = 0; i < states.Count; i++)
        {
            if((Vector4)states[i].c == (Vector4)col)
            {
                return states[i];
            }
        }
        return null;
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
