using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    public string Identifier;
    private Vector3 offset;

    public Button closeButton;
    public bool canClose;

    public State Location;//if location is important

    public Policy policy;//if this is for a policy proposal

    public TMP_Text[] text;//usually not important

    public Notification Parent;

    public LineRenderer Connector;

    public bool Closed;

    public bool Minimized;

    void Awake()
    {
        if(closeButton != null)
        {
           closeButton.interactable =  canClose; 
        }
        if(Nation.me != null){
       Nation.me.seenEvents.Add(Identifier);
       Nation.me.openThings++;
        }
       transform.localScale = new Vector3(0,0,0);
    }

    public object Shallowcopy()
    {
        return this.MemberwiseClone();
    }

    //public TMP_Text[] texts;

    public Notification SetLocation(State s)
    {
        Location = s;
        return this;
    }

    public Notification SetPolicy(Policy s)
    {
        policy = s;
        return this;
    }

    public Notification SetDescription(string s)
    {
        text[1].text = s;
        return this;
    }

    public Notification SetTitle(string s)
    {
        text[0].text = s;
        return this;
    }

    void FixedUpdate()
    {
        if(Connector != null){
        if(Parent != null && !Closed && !Minimized)
        {
            Connector.SetPosition(0, transform.position);
            Connector.SetPosition(1, Parent.transform.position);
        }  else
            {
                Connector.SetPosition(0, transform.position);
            Connector.SetPosition(1, transform.position);
            }
        }

        transform.localScale = Vector3.Lerp(transform.localScale, Closed || Minimized ? new Vector3(transform.localScale.x, 0, 0): new Vector3(transform.localScale.x, 1, 1), Closed ||Minimized ? 0.3f : 0.2f);
        transform.localScale = Vector3.Lerp(transform.localScale, Closed || Minimized ? new Vector3(0, transform.localScale.y, 0): new Vector3(1, transform.localScale.y, 1), Closed||Minimized ? 0.2f : 0.3f);

        if(Closed && transform.localScale.x < 0.05f)
        {
            Destroy(gameObject);
        }
    }



    
    public void Close()
    {
        Nation.me.openThings--;
        UnHover();
        Closed = true;
        
    }

    public void Minimize()
    {
        Nation.me.openThings--;
        UnHover();
        Minimized = true;
        
    }

    public void UnMinimize()
    {
        Nation.me.openThings++;
        //Hover();
        Minimized = false;
        
    }
    public void StartDrag()
    {
        transform.SetAsLastSibling();
        Vector3 worldPosition = CameraMover.me.cam.ScreenToWorldPoint(Input.mousePosition);
        offset = worldPosition - transform.position;
    }

    public void Drag()
    {
        Vector3 worldPosition = CameraMover.me.cam.ScreenToWorldPoint(Input.mousePosition);
        
        transform.position = new Vector3(worldPosition.x - offset.x, worldPosition.y - offset.y, 0);// - new Vector3(770/2, 505/2, 0);
    }

    public void BookMark()
    {
        Nation.me.openThings--;
        UnHover();
        Nation.me.mailQueue.Add(this);
        Destroy(gameObject);   
    }

    public void Hover(string t)
    {
        CameraMover.me.hoverEnabled = true;
        CameraMover.me.hoverText = t;

    }

    public void UnHover()
    {
        CameraMover.me.hoverEnabled = false;
        CameraMover.me.hoverText = "";
    }

    public Notification SetNewParent(Notification n){
        Parent = n;
        return this;
    }

    public void Response(string data)
    {
        switch (data)
        {
            case "":
                Debug.Log("what is this, question? no data.");
                Close();
                break;
            case "hotelStateBuildYes":
                Location.thingsHere.Add( Rollout.NewRollout("hotel", null, 4, 0.25f, 0, 0, 0, false));
                Location.thingsHere[Location.thingsHere.Count - 1].location = Location;
                Location.localSupportLevel += 0.02f;
                Close();
                break;
            case "hotelStateBuildNo":
                Location.localSupportLevel -= 0.01f;
              //  Debug.Log("what is this, question? no data.");
                Close();
                break;
            case "hospitalStateBuildYes":
                Location.thingsHere.Add( Rollout.NewRollout("hospital", null, 5, 1, 0, 0, 0, false));
                Location.thingsHere[Location.thingsHere.Count - 1].location = Location;
                Location.localSupportLevel += 0.01f;
                Close();
                break;
            case "hospitalStateBuildNo":
                Location.localSupportLevel -= 0.05f;
              //  Debug.Log("what is this, question? no data.");
                Close();
                break;
            case "schoolStateBuildYes":
                Location.thingsHere.Add( Rollout.NewRollout("school", null, 5, 0.75f, 0, 0, 0, false));
                Location.thingsHere[Location.thingsHere.Count - 1].location = Location;
                Location.localSupportLevel += 0.01f;
                Close();
                break;
            case "schoolStateBuildNo":
                Location.localSupportLevel -= 0.04f;
              //  Debug.Log("what is this, question? no data.");
                Close();
                break;
            case "OpenSeniorAdvisor":
                Nation.me.OpenImmediately("SeniorAdvisorMenu").SetNewParent(this);//.Parent = this;
                //Close();
                break;
            case "IndustrialAdvisorRecPolicy":
                Nation.me.Unminimize("IndustrialPolicySelector", this);//.SetNewParent(this);//.Parent = this;
                //Close();
                break;
            case "OpenIndustrialAdvisor":
                Nation.me.OpenImmediately("IndustrialAdvisorMenu").SetNewParent(this);//.Parent = this;
                //Close();
                break;
            case "SeniorAdvisorRecPolicy":
                Nation.me.OpenImmediately("Proposal").SetPolicy(Policy.NewPolicy("test", 0.25f, -1f)).SetNewParent(this);
                //Close();
                break;
            case "LGTM":
                //Nation.me.OpenImmediately("SeniorAdvisorMenu");
                //pass this policy into senate! not implemented yet
                Nation.me.policiesToVoteOn.Add(policy);
                Close(); 
                break;
            default:
                Debug.Log(data + " | notification response doesnt exist?");
                //dont close it! might be useful for debugging...
                break;
            

        }
    }
}
