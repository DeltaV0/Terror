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

    public TMP_Text[] text;//usually not important

    void Awake()
    {
       closeButton.interactable =  canClose;
       Nation.me.seenEvents.Add(Identifier);
       Nation.me.openThings++;
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



    
    public void Close()
    {
        Nation.me.openThings--;
        UnHover();
        Destroy(gameObject);
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
            default:
                Debug.Log(data + " | notification response doesnt exist?");
                //dont close it! might be useful for debugging...
                break;
            

        }
    }
}
