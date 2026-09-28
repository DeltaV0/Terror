using UnityEngine;
using TMPro;

public class CameraMover : MonoBehaviour
{
    public static CameraMover me;
    public Vector2 screenPos;

    public Camera cam;

    public bool moving;
    [Header("UI")]
    public Transform[] UI; 

    public TMP_Text[] text;

    public Transform MouseHoverer;
    public RectTransform MouseHovererRect;
    public Transform Pointer;

    public string hoverText;
    public TMP_Text mousehoverText;
    public bool hoverEnabled;

    public bool targeting;
    public Vector3 TargetPos;

    public float camSize;

    //public GameObject hoveredObject;

    public void HoverOverSomething(string s)
    {
        hoverEnabled = true;
        hoverText = s;
    }

    public void StopHovering()
    {
        hoverEnabled = false;
        hoverText = "";
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
        me = this;
        screenPos = Vector2.zero;
        camSize = 6;
    }

    void Update()
    {
        Vector3 worldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Pointer.position = new Vector3(worldPosition.x, worldPosition.y, -2f);// - new Vector3(16, 16, 0);
        //Debug.Log(Input.mousePosition);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GraphicsRenderer.DoneLoading){
        mousehoverText.text = hoverText;
       Vector3 worldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
       Vector3 DifferencePosition = worldPosition - transform.position;
       MouseHoverer.gameObject.SetActive(hoverEnabled);
       if(Input.mousePosition.y >= 720/2){
       if(Input.mousePosition.x >= 1280 / 2)
        {
            MouseHoverer.localPosition = Vector3.Lerp(MouseHoverer.localPosition, new Vector3(Input.mousePosition.x - 1280/2 - 16 - MouseHovererRect.sizeDelta.x, Input.mousePosition.y - 720/2 - 16 - MouseHovererRect.sizeDelta.y, -1f), 0.1f);
        } else
        {
            MouseHoverer.localPosition = Vector3.Lerp(MouseHoverer.localPosition, new Vector3(Input.mousePosition.x - 1280/2 + 16, Input.mousePosition.y - 720/2 - 16 - MouseHovererRect.sizeDelta.y, -1f), 0.1f);

        }
       } else
        {
           if(Input.mousePosition.x >= 1280 / 2)
        {
            MouseHoverer.localPosition = Vector3.Lerp(MouseHoverer.localPosition, new Vector3(Input.mousePosition.x - 1280/2 - 16 - MouseHovererRect.sizeDelta.x, Input.mousePosition.y - 720/2 + 16, -1f), 0.1f);
        } else
        {
            MouseHoverer.localPosition = Vector3.Lerp(MouseHoverer.localPosition, new Vector3(Input.mousePosition.x - 1280/2 + 16, Input.mousePosition.y - 720/2 + 16, -1f), 0.1f);

        } 
        }
        camSize -= Input.mouseScrollDelta.y;
        if(camSize > 10)
            {
                camSize = 10;
            } else if(camSize < 3)
            {
                camSize = 3;
            }
       MouseHovererRect.sizeDelta = mousehoverText.rectTransform.sizeDelta + new Vector2(20, 20); 
       MouseHovererRect.GetChild(0).GetComponent<RectTransform>().sizeDelta = mousehoverText.rectTransform.sizeDelta + new Vector2(10, 10);
       if(!Nation.me.doingTurnStuff){
        if(worldPosition.y < -5.5f + transform.position.y + camSize/2f)
        {
            screenPos += new Vector2(0, -0.1f);
        } else if(worldPosition.y > 5.5f + transform.position.y -camSize/2f)
        {
            screenPos += new Vector2(0, 0.1f);
        }

        if(worldPosition.x < -5 + transform.position.x + camSize/2f)
        {
            screenPos += new Vector2(-0.1f, 0);
        } else if(worldPosition.x > 5 + transform.position.x - camSize/2f)
        {
            screenPos += new Vector2(0.1f, 0);
        }
       } else
        {
           moving = false; 
        }
        if(screenPos.x > 16f - camSize*(16f/9f))
            {
                screenPos = new Vector2(16f - camSize*(16f/9f), screenPos.y);
            }
        if(screenPos.x < -16f + camSize*(16f/9f))
            {
                screenPos = new Vector2(-16f + camSize*(16f/9f), screenPos.y);
            }
        if(screenPos.y > 10 - camSize)
            {
                screenPos = new Vector2(screenPos.x, 10f - camSize);
            }
        if(screenPos.y < -10 + camSize)
            {
                screenPos = new Vector2(screenPos.x, -10f + camSize);
            }
        //if(!targeting){
       if(Nation.me.selectedState == null || Nation.me.doingTurnStuff)
        {
            if (Nation.me.doingTurnStuff)
            {
                UI[4].localPosition = Vector3.Lerp(UI[4].localPosition, new Vector3(-960, 576, 0), 0.1f);
            } else
            {
                UI[4].localPosition = Vector3.Lerp(UI[4].localPosition, new Vector3(-560, 288, 0), 0.1f);
            }
            if (Congress.me.meeting)
            {
                transform.position = Vector3.Lerp(transform.position, Vector3.Lerp(Vector2.zero + new Vector2(3f, 0), new Vector3(worldPosition.x, worldPosition.y, -10), 0.025f), 0.1f);
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 7f, 0.1f);

            } else
            {
                transform.position = Vector3.Lerp(transform.position, Vector3.Lerp(Vector2.zero + new Vector2(screenPos.x, screenPos.y), new Vector3(worldPosition.x, worldPosition.y, -10), 0.1f), 0.1f);
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, camSize, 0.1f);
            }
            UI[0].localPosition = Vector3.Lerp(UI[0].localPosition, new Vector3(-820f, 0, 0), 0.1f);
            UI[1].localPosition = Vector3.Lerp(UI[1].localPosition, new Vector3(820f, 0, 0), 0.1f);
            UI[2].localPosition = Vector3.Lerp(UI[2].localPosition, new Vector3(0, 432, 0), 0.1f);
            UI[3].localPosition = Vector3.Lerp(UI[3].localPosition, new Vector3(0, -432, 0), 0.1f);
            UI[5].localPosition = Vector3.Lerp(UI[5].localPosition, new Vector3(-560, -350, 0), 0.1f);

                   
        } else
        {
                  text[0].text = Nation.me.selectedState.Name;
                  text[1].text = "of " + ((Nation.me.selectedState.memberOf != null) ? Nation.me.selectedState.memberOf.name : Nation.me.name);
                transform.position = Vector3.Lerp(transform.position, Vector3.Lerp(Nation.me.selectedState.Centroid, new Vector3(worldPosition.x, worldPosition.y, -10), 0.02f), 0.1f); 
                   cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, Nation.me.selectedState.col.bounds.extents.magnitude * 1.5f, 0.1f);
                 UI[0].localPosition = Vector3.Lerp(UI[0].localPosition, new Vector3(-480, 0, 0), 0.1f);
                UI[1].localPosition = Vector3.Lerp(UI[1].localPosition, new Vector3(480, 0, 0), 0.1f);
                UI[2].localPosition = Vector3.Lerp(UI[2].localPosition, new Vector3(0, 288, 0), 0.1f);
            UI[3].localPosition = Vector3.Lerp(UI[3].localPosition, new Vector3(0, -288, 0), 0.1f);
            UI[4].localPosition = Vector3.Lerp(UI[4].localPosition, new Vector3(-960, 576, 0), 0.1f);
            UI[5].localPosition = Vector3.Lerp(UI[5].localPosition, new Vector3(-960, -576, 0), 0.1f);

        }
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
    }

    public void TogglePolitiview()
    {
        Nation.me.politiview = !Nation.me.politiview;
    }
}
