using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;


public class Nation : MonoBehaviour
{
    
    public List<Notification> mail = new List<Notification>();

    public List<string> mailQueue = new List<string>();


    public List<ForeignNation> NeighborNations = new List<ForeignNation>();

    public List<State> states = new List<State>();

    public List<State> statesNation = new List<State>();

    public Transform MainCameraCanvas;


    public GameObject statePf;
    public GameObject roadPf;

    public static Nation me;

    public Transform canvas;

    public List<Road> roads;

    public State selectedState;

    public State hoverState;

    public Button[] buttons;

    public List<Rollout> nationalRollouts = new List<Rollout>();

    public float money;

    public float debt;

    public float debtSpending;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        me = this;
        mailQueue.Add("default");
        mailQueue.Add("default");
        mailQueue.Add("default");

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

    public float GetTotalStateSpending()
    {
        float simMoney = money;
        float f = 0;
        for(int i = 0; i < statesNation.Count; i++)
        {
            f += statesNation[i].BaseSpending;
            f += statesNation[i].CalculateSpending();
        }
        return f;
    }

    public float GetProjectStateSpending()
    {
        float f = 0;
        for(int i = 0; i < statesNation.Count; i++)
        {
           // f += statesNation[i].BaseSpending;
            f += statesNation[i].CalculateSpending();
        }
        return f;
    }

    public float GetTotalStateIncome()
    {
        float f = 0;
        for(int i = 0; i < statesNation.Count; i++)
        {
            f += statesNation[i].income;
            //f += statesNation[i].CalculateSpending();
        }
        return f;
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

    public void nextMail()
    {
        if(mailQueue.Count > 0){
        GameObject g = Instantiate(FindEventFromID(mailQueue[0]).gameObject, Vector3.zero, Quaternion.identity);
        mailQueue.RemoveAt(0);
        g.transform.SetParent(MainCameraCanvas, true);
        g.transform.localScale = new Vector3(1, 1, 1);
        g.transform.position = new Vector3(0, 0, 0);
        }
    }

    public void nextTurn()
    {
        StartCoroutine(TurnEnum());
    }

    public IEnumerator TurnEnum()
    {
        for(int i = 0; i < statesNation.Count; i++)
        {
            statesNation[i].DoTurn();
            yield return new WaitForSeconds(0.1f);
        }

        for(int q = 0; q < nationalRollouts.Count; q++){
        for(int i = 0; i < 12; i++)
        {
            int Rand = Random.Range(0, 16);
                if (!statesNation[Rand].CheckIfHasRollout(nationalRollouts[q].Name))
                {
                    statesNation[Rand].thingsHere.Add(Rollout.NewRolloutFromRollout(nationalRollouts[q]));
                    //statesNation[Rand].doThingy();
                    //yield return new WaitForSeconds(0.1f);
                }
        }
        }

        debt -= debtSpending + money;
        money = 0;

        for(int i = 0; i < statesNation.Count; i++)
        {
            money += statesNation[i].income;
            yield return new WaitForSeconds(0.1f);
        }
        
        
    }

    public Notification FindEventFromID(string ID)
    {
        for(int i = 0; i < mail.Count; i++)
        {
            if (ID.Equals(mail[i].Identifier))
            {
                return mail[i];
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
