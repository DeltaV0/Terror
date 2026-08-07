using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using TMPro;


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
    public GameObject textpf;

    public static Nation me;

    public Transform canvas;

    public List<Road> roads;

    public State selectedState;

    public State hoverState;

    public Button[] buttons;
    public TMP_Text[] text;

    public List<Rollout> nationalRollouts = new List<Rollout>();

    public float money;

    private float moneyShown;

    public float nationalSpendingExtra;
    public float nationalSpendingBase;

    public float debt;

    public float debtSpending;

    public bool doingTurnStuff;

    public int openThings;

    public float[] partyPopularityAmounts = {25f, 50f, 25f}; //liberal, center, conservative
    public float[] satisfaction = {20f, 50f, 20f}; //percent populatity (how much they like you)

    public float Corruption;
    public float CorruptionDelta;

    public int turn;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        turn-=1;
        CorruptionDelta = 1f;
        me = this;
        mailQueue.Add("intro advisor");
        mailQueue.Add("intro");

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

    public float GetNationalSpending()
    {
        float f = 0;
        foreach (Rollout r in nationalRollouts)
        {
            if(r.Cprogress > 0)
            {
                f+= r.COTcost;
            }
                
        }
        return f;
    }

    public float GetTotalStateSpending()
    {
        float f = 0;
        for(int i = 0; i < statesNation.Count; i++)
        {
            f += statesNation[i].BaseSpending;
            f += statesNation[i].CalculateSpending();
        }
        f += GetNationalSpending();
        f += nationalSpendingBase;
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
        f+= GetNationalSpending();
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
        GameObject g = Instantiate(FindEventFromID(mailQueue[0]).gameObject, new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0), Quaternion.identity);
        mailQueue.RemoveAt(0);
        g.transform.SetParent(MainCameraCanvas, true);
        g.transform.localScale = new Vector3(1, 1, 1);
        g.transform.position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
        }
    }

    public void nextTurn()
    {
        StartCoroutine(TurnEnum());
    }

    public IEnumerator TurnEnum()
    {
        nationalSpendingExtra = GetNationalSpending();
        doingTurnStuff = true;
        Corruption += CorruptionDelta;
        Corruption = Mathf.Max(0, Corruption);
       // CameraMover.me.targeting = true;
        CameraMover.me.screenPos = new Vector2(0, 0);
        selectedState = null;
        

        
        debt -= debtSpending + money;

        yield return new WaitForSeconds(0.5f);
        turn += 1;

        for(int q = 0; q < nationalRollouts.Count; q++){
            if(!nationalRollouts[q].national){
        for(int i = 0; i < 12; i++)
        {
            int Rand = Random.Range(0, 16);
                if (!statesNation[Rand].CheckIfHasRollout(nationalRollouts[q].Name) && nationalRollouts[q].Cprogress <= 0)
                {
                    statesNation[Rand].thingsHere.Add(Rollout.NewRolloutFromRollout(nationalRollouts[q]));
                    statesNation[Rand].thingsHere[statesNation[Rand].thingsHere.Count - 1].location = statesNation[Rand];
                    //statesNation[Rand].doThingy();
                    //yield return new WaitForSeconds(0.1f);
                }
        }
            }
        nationalRollouts[q].Cprogress--;
        if(nationalRollouts[q].Cprogress <= 0 && nationalRollouts[q].national)
            {
                nationalRollouts[q].Complete();
            }
        }

        
        money = 0;

        for(int i = 0; i < statesNation.Count; i++)
        {
            statesNation[i].GiveIncome();
            CameraMover.me.TargetPos = statesNation[i].Centroid;
            yield return new WaitForSeconds(Random.Range(0f, 0.25f));
        }

        yield return new WaitForSeconds(0.5f);

        for(int i = 0; i < statesNation.Count; i++)
        {
            statesNation[i].SubtractBaseSpendingCosts();
            CameraMover.me.TargetPos = statesNation[i].Centroid;
            yield return new WaitForSeconds(Random.Range(0f, 0.1f));
        }
        

        
        money -= nationalSpendingBase;

        yield return new WaitForSeconds(0.5f);

        money -= debtSpending;

        yield return new WaitForSeconds(0.5f);

        money -= nationalSpendingExtra;

        for(int i = 0; i < statesNation.Count; i++)
        {
            if(statesNation[i].CanDoSomething()){
            statesNation[i].DoTurn();
            CameraMover.me.TargetPos = statesNation[i].Centroid;
            yield return new WaitForSeconds(Random.Range(0f, 0.25f));
            }
        }

        yield return new WaitForSeconds(0.5f);

        
        
        //CameraMover.me.targeting = false;
        doingTurnStuff = false;
        selectedState = null;
        PolicyManager.me.UpdateAllTechs();
        CameraMover.me.screenPos = new Vector2(0, 0);
        

        while(mailQueue.Count > 0)
        {
            nextMail();
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
    void FixedUpdate()
    {
        moneyShown = Mathf.Round(Mathf.Lerp(moneyShown, money, 0.1f) * 1000f) / 1000f;
        nationalSpendingBase = Mathf.Round(nationalSpendingBase * 1000f) / 1000f;
         nationalSpendingExtra = Mathf.Round(nationalSpendingExtra * 1000f) / 1000f;
        buttons[0].interactable = mailQueue.Count <= 0 && openThings <= 0 && !doingTurnStuff;
         buttons[1].interactable = mailQueue.Count > 0 && !doingTurnStuff;
         text[0].text = "x" + mailQueue.Count;
         string str = "";
        switch (turn % 12)
        {
            case 0:
                str += "jan";
                break;
            case 1:
                str += "feb";
                break;
            case 2:
                str += "mar";
                break;
            case 3:
                str += "apr";
                break;
            case 4:
                str += "may";
                break;
            case 5:
                str += "jun";
                break;
            case 6:
                str += "jul";
                break;
            case 7:
                str += "aug";
                break;
            case 8:
                str += "sep";
                break;
            case 9:
                str += "oct";
                break;
            case 10:
                str += "nov";
                break;
            case 11:
                str += "dec";
                break;
            default:
                str += "?";
                break;
            }

            str += " ";
            str += ((turn / 12) + 2002) + "";
          text[1].text = str;
           text[2].text = "$" + moneyShown + "b";
    }
}
