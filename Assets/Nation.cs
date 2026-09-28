using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using TMPro;


public class Nation : MonoBehaviour
{
    
    public List<Notification> mail = new List<Notification>();

    public List<Notification> mailQueue = new List<Notification>();

    public List<string> seenEvents = new List<string>();

    public List<ForeignNation> NeighborNations = new List<ForeignNation>();

    public List<ForeignNation> BorderNations = new List<ForeignNation>();


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
    public int miniTurn;

        public int microTurn;

        public int nanoTurn;


    public bool politiview;

    public List<Policy> policiesToVoteOn = new List<Policy>();

    public int speed;

    public string name;

    readonly int[] DaysInAMonth = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};




    public IEnumerator StartCrtne()
    {
        
        GraphicsRenderer.DoneLoading = false;
        name = NameGenerator.me.NationNameGenerate();
        speed = 2;
        turn-=1;
        CorruptionDelta = 1f;
        me = this;
        AddToQueue("intro advisor");
        AddToQueue("intro");

        GraphicsRenderer.BigAssArrayClear = new Color32[(1280*GraphicsRenderer.me.xS2) * (720*GraphicsRenderer.me.yS2)];
        //foreach() 

        yield return new WaitForSeconds(0);
        for(int i = 0; i < 16; i++)
        {
            GameObject g = Instantiate(statePf, new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f), 0), Quaternion.identity);
            statesNation.Add(g.GetComponent<State>());
            states.Add(g.GetComponent<State>());
            states[i].ID = i;
            states[i].outside = false;
            //states[i].init();
            g.transform.SetParent(canvas, true);
            //Debug.Log(i);
        }
        yield return new WaitForSeconds(0);
        for(int i = 0; i < 8; i++)
        {
            float q = (float)(i / 4f) * Mathf.PI + Random.Range(-0.25f, 0.25f);
            GameObject g = Instantiate(statePf, new Vector3(Random.Range(3f, 5f) * Mathf.Cos(q), Random.Range(3f, 5f) * Mathf.Sin(q), 0), Quaternion.identity);
            states.Add(g.GetComponent<State>());
            states[i+ 16].ID = i + 16;
            states[i+ 16].outside = true;
           // states[i+ 16].init();
           g.transform.SetParent(canvas, true);

           // Debug.Log(i + "a");

        }

        yield return new WaitForSeconds(0);
        for(int i = 0; i < 8; i++)
        {
            float q = (float)(i / 4f) * Mathf.PI + Random.Range(-0.25f, 0.25f);
            GameObject g = Instantiate(statePf, new Vector3(Random.Range(8f, 11f) * Mathf.Cos(q) + Random.Range(-3f, 3f), Random.Range(6f, 8f) * Mathf.Sin(q) + Random.Range(-2f, 2f), 0), Quaternion.identity);
            states.Add(g.GetComponent<State>());
            states[i+ 24].ID = i + 24;
            states[i+ 24].outside = true;
            //states[i+ 24].init();
           g.transform.SetParent(canvas, true);

           // Debug.Log(i + "a");

        }

        
       // states[17].init();
       yield return new WaitForSeconds(0);
        for(int i = 0; i < 24; i++)
        {
            float q = Random.Range(0, 2f * Mathf.PI);
            GameObject g = Instantiate(statePf, new Vector3(Random.Range(6f, 14f) * Mathf.Cos(q), Random.Range(6f, 14f) * Mathf.Sin(q), 0), Quaternion.identity);
            states.Add(g.GetComponent<State>());
            states[i+ 32].ID = i + 32;
            states[i+ 32].outside = true;
            
            g.transform.SetParent(canvas, true);

           // Debug.Log(i + "b");
        }

        yield return new WaitForSeconds(0);

        for(int i = 0; i < 56; i++)
        {
           states[i].init(); 
        }

        
        /*for(int i = 0; i < states.Count; i++)
        {
            states[i].InitVectorDrawers();
            states[i].RenderLines();
        }*/
        yield return new WaitForSeconds(0);
        GraphicsRenderer.me.initGraphics();
        //yield return new WaitForSeconds(0);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        StartCoroutine(StartCrtne());
    }

    public void Phone()
    {
        OpenImmediately("PhoneMenu"); //ughh just use this for now it's easier
        //Consortium.me.ToggleActive();
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
        float sim = GetTotalStateIncome();
        float f = 0;
        for(int i = 0; i < statesNation.Count; i++)
        {
            f += statesNation[i].BaseSpending;
            sim -= statesNation[i].BaseSpending;
        }
        f += nationalSpendingBase;
        sim -= nationalSpendingBase;
        f += GetNationalSpending();
        sim -= GetNationalSpending();
        for(int i = 0; i < statesNation.Count; i++)
        {
            f += statesNation[i].CalculateSpending(sim);
            sim -= statesNation[i].CalculateSpending(sim);
        }
        
        
        return f;
    }

    public Notification AddToQueue(string s)
    {
        
        Notification n = FindEventFromID(s);
        mailQueue.Add(n);
        return n;
    }

    public float GetProjectStateSpending()
    {
        float sim = GetTotalStateIncome();
        float f = 0;
        for(int i = 0; i < statesNation.Count; i++)
        {
           // f += statesNation[i].BaseSpending;
            sim -= statesNation[i].BaseSpending;
        }
       // f += nationalSpendingBase;
        sim -= nationalSpendingBase;
        f += GetNationalSpending();
        sim -= GetNationalSpending();
        for(int i = 0; i < statesNation.Count; i++)
        {
           // f += statesNation[i].BaseSpending;
            f += statesNation[i].CalculateSpending(sim); //only add this
            sim -= statesNation[i].CalculateSpending(sim);
        }
        
        return f;
    }

    public float GetTotalSupport()
    {
        float f = 0;
        foreach(State s in statesNation)
        {
            f += s.localSupportLevel;
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
        GameObject g = Instantiate(mailQueue[0].gameObject, new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0), Quaternion.identity);
        mailQueue.RemoveAt(0);
        g.transform.SetParent(MainCameraCanvas, true);
        g.transform.localScale = new Vector3(1, 1, 1);
        g.transform.position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
        }
    }

    public Notification OpenImmediately(string s)
    {
       // if(mailQueue.Count > 0){
        GameObject g = Instantiate(FindEventFromID(s).gameObject, new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0), Quaternion.identity);
        g.transform.SetParent(MainCameraCanvas, true);
        g.transform.localScale = new Vector3(1, 1, 1);
        g.transform.position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
        return g.GetComponent<Notification>();
        //}
    }

    public void nextTurn()
    {
        StartCoroutine(TurnEnum());
    }

    public void StartTurn()
    {
        StartCoroutine(TurnEnum2());
    }

    public bool TrySpendMoney(float amt)
    {
        if(money - amt < 0)
        {
            return false;
        }
        else
        {
            money -= amt;
            return true;
        }
    }
    public IEnumerator TurnEnum2()
    {
        doingTurnStuff = true;
        turn += 1;

        for(int q = 0; q < nationalRollouts.Count; q++){
            if(!nationalRollouts[q].national){
        for(int i = 0; i < 8; i++)
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

        
        

        for(int i = 0; i < statesNation.Count; i++)
        {
            statesNation[i].GiveIncome();
            CameraMover.me.TargetPos = statesNation[i].Centroid;
            //yield return new WaitForSeconds(Random.Range(0f, 0.25f));
        }

       /// yield return new WaitForSeconds(0.5f);
        DoNotificationStuff();

        for(int i = 0; i < statesNation.Count; i++)
        {
            statesNation[i].localSupportDelta += Random.Range(-0.01f, 0.01f);
            statesNation[i].localSupportLevel += statesNation[i].localSupportDelta;
            statesNation[i].SubtractBaseSpendingCosts();
            CameraMover.me.TargetPos = statesNation[i].Centroid;
            //yield return new WaitForSeconds(Random.Range(0f, 0.1f));
        }
        

        
        money -= nationalSpendingBase;

        //yield return new WaitForSeconds(0.5f);

        money -= debtSpending;

        //yield return new WaitForSeconds(0.5f);

        money -= nationalSpendingExtra;

        for(int i = 0; i < statesNation.Count; i++)
        {
            if(statesNation[i].CanDoSomething()){
            statesNation[i].DoTurn();
            CameraMover.me.TargetPos = statesNation[i].Centroid;
            //yield return new WaitForSeconds(Random.Range(0f, 0.25f));
            }
        }

        yield return new WaitForSeconds(0.5f);

        
        
        //CameraMover.me.targeting = false;
        doingTurnStuff = false;
        selectedState = null;
        //PolicyManager.me.UpdateAllTechs();
        CameraMover.me.screenPos = new Vector2(0, 0);
        

        while(mailQueue.Count > 0)
        {
            nextMail();
            yield return new WaitForSeconds(0.1f);

        }
        speed = 2;
    }

    public IEnumerator TurnEnum()
    {
        speed = 0;
        nationalSpendingExtra = GetNationalSpending();
        doingTurnStuff = true;
        Corruption += CorruptionDelta;
        Corruption = Mathf.Max(0, Corruption);
       // CameraMover.me.targeting = true;
        CameraMover.me.screenPos = new Vector2(0, 0);
        selectedState = null;

        //DataManager.me.UpdateCharts();
        

        
        debt -= debtSpending + money;
        money = 0;

        yield return new WaitForSeconds(0.25f);

        Congress.me.Meet();
        
        
        
    }

    public List<State> GetAllStatesWithTech(string str)
    {
        List<State> states = new List<State>();
        foreach(State s in statesNation)
        {
            if (s.CheckIfFinishedRollout(str))
            {
                states.Add(s);
            }
        }
        return states;
    }

    public bool FinishedNationalTech(string s)
    {
        foreach(Rollout r in nationalRollouts)
        {
            if(r.Name.Equals(s) && r.Cprogress <= 0)
            {
                return true;
            }
        }
        return false;
    }

    public Notification FindEventFromID(string ID)
    {
        for(int i = 0; i < mail.Count; i++)
        {
            if (ID.Equals(mail[i].Identifier))
            {
                return (Notification)mail[i].Shallowcopy();
            }
        }
        return null;
    }

    public void DoNotificationStuff()
    {
        //bullshi
        foreach(State s in statesNation)
        {
            if(s.CheckIfFinishedRollout("Community Outreach"))
            {
                if((s.totalInfra + s.size) > 2.5f){
                if(Random.Range(0f, 1f) < 0.025f)
                {
                    AddToQueue("hotel").SetLocation(s).SetTitle(s.Name).SetDescription(s.Name + " is requesting funding to build a hotel");
                }
                if(Random.Range(0f, 1f) < 0.025f)
                {
                    AddToQueue("school").SetLocation(s).SetTitle(s.Name).SetDescription(s.Name + " is requesting funding to build an elementary school");
                }
                if(Random.Range(0f, 1f) < 0.025f)
                {
                    AddToQueue("hospital").SetLocation(s).SetTitle(s.Name).SetDescription(s.Name + " is requesting funding to build a hospital");
                }
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GraphicsRenderer.DoneLoading){
        nanoTurn += speed * 15;
        if(nanoTurn >= 60 && speed > 0)
        {
            nanoTurn = 0;
            microTurn++;
        }
        if(miniTurn >= DaysInAMonth[turn % 12] && speed > 0 && microTurn >= 24)
        {
            microTurn = 0;
            miniTurn = 0;
            nextTurn();
        }
        if(microTurn >= 24 && speed>0)
        {
            microTurn = 0;
            miniTurn++;
        }
        
        if (Congress.me.meeting)
        {
            politiview = true;
        }
        moneyShown = Mathf.Round(Mathf.Lerp(moneyShown, money, 0.1f) * 1000f) / 1000f;
        nationalSpendingBase = Mathf.Round(nationalSpendingBase * 1000f) / 1000f;
         nationalSpendingExtra = Mathf.Round(nationalSpendingExtra * 1000f) / 1000f;
        buttons[0].interactable = mailQueue.Count <= 0 && openThings <= 0 && !doingTurnStuff;
         buttons[1].interactable = mailQueue.Count > 0 && !doingTurnStuff;
         text[0].text = "x" + mailQueue.Count;
         string str = "";
       /* switch (turn % 12)
        {
            case 0:
                str += "Jan";
                break;
            case 1:
                str += "Feb";
                break;
            case 2:
                str += "Mar";
                break;
            case 3:
                str += "Apr";
                break;
            case 4:
                str += "May";
                break;
            case 5:
                str += "Jun";
                break;
            case 6:
                str += "Jul";
                break;
            case 7:
                str += "Aug";
                break;
            case 8:
                str += "Sep";
                break;
            case 9:
                str += "Oct";
                break;
            case 10:
                str += "Nov";
                break;
            case 11:
                str += "Dec";
                break;
            default:
                str += "Err OOT";
                break;
            }*/

            str += miniTurn.ToString("00") + "/" + ((turn % 12) + 1).ToString("00") + "/";
            str += ((turn / 12) + 2002) + "\n";
            str += microTurn.ToString("00") + ":" + nanoTurn.ToString("00");
          text[1].text = str;
           text[2].text = "$" + moneyShown + "b";
        }
    }
}
