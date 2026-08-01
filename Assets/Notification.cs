using UnityEngine;
using TMPro;

public class Notification : MonoBehaviour
{
    public string Identifier;
    private Vector3 offset;

    //public TMP_Text[] texts;



    
    public void Close()
    {
        Destroy(gameObject);
    }
    public void StartDrag()
    {
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
        Nation.me.mailQueue.Add(Identifier);
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
}
