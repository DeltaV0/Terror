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
    }

    void Update()
    {
        Vector3 worldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Pointer.position = new Vector3(worldPosition.x, worldPosition.y, -2f);// - new Vector3(16, 16, 0);
        Debug.Log(Input.mousePosition);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
       MouseHovererRect.sizeDelta = mousehoverText.rectTransform.sizeDelta + new Vector2(20, 20); 
       MouseHovererRect.GetChild(0).GetComponent<RectTransform>().sizeDelta = mousehoverText.rectTransform.sizeDelta + new Vector2(10, 10);
       if(!Nation.me.doingTurnStuff){
        if(worldPosition.y < -5.5f + transform.position.y && worldPosition.x > -6 && worldPosition.x < 6)
        {
            if(!moving)
            {
                if(screenPos.y == 0){
                screenPos = new Vector2(0, -2);
                } else if(screenPos.y == 2){
                screenPos = new Vector2(0, 0);
                }
            }
            moving = true;
        } else if(worldPosition.y > 5.5f + transform.position.y && worldPosition.x > -6 && worldPosition.x < 6)
        {
            if(!moving)
            {
                if(screenPos.y == 0){
                screenPos = new Vector2(0, 2);
                } else if(screenPos.y == -2){
                screenPos = new Vector2(0, 0);
                }
            }
            moving = true;
        } else {
            moving = false;
        }
       } else
        {
           moving = false; 
        }
        if(!targeting){
       if(Nation.me.selectedState == null || Nation.me.doingTurnStuff)
        {

            transform.position = Vector3.Lerp(transform.position, Vector3.Lerp(Vector2.zero + new Vector2(screenPos.x, screenPos.y * 6f), new Vector3(worldPosition.x, worldPosition.y, -10), 0.1f), 0.1f);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 5f + (Vector3.Distance(worldPosition, Vector3.zero) * 0.1f), 0.1f);
            UI[0].localPosition = Vector3.Lerp(UI[0].localPosition, new Vector3(-820f, 0, 0), 0.1f);
            UI[1].localPosition = Vector3.Lerp(UI[1].localPosition, new Vector3(820f, 0, 0), 0.1f);
            UI[2].localPosition = Vector3.Lerp(UI[2].localPosition, new Vector3(0, 432, 0), 0.1f);
            UI[3].localPosition = Vector3.Lerp(UI[3].localPosition, new Vector3(0, -432, 0), 0.1f);
            UI[4].localPosition = Vector3.Lerp(UI[4].localPosition, new Vector3(-560, 288, 0), 0.1f);
            UI[5].localPosition = Vector3.Lerp(UI[5].localPosition, new Vector3(-560, -350, 0), 0.1f);

                   
        } else
        {
                  text[0].text = Nation.me.selectedState.Name;
                transform.position = Vector3.Lerp(transform.position, Vector3.Lerp(Nation.me.selectedState.Centroid, new Vector3(worldPosition.x, worldPosition.y, -10), 0.02f), 0.1f); 
                   cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, Nation.me.selectedState.col.bounds.extents.magnitude * 1.5f, 0.1f);
                 UI[0].localPosition = Vector3.Lerp(UI[0].localPosition, new Vector3(-480, 0, 0), 0.1f);
                UI[1].localPosition = Vector3.Lerp(UI[1].localPosition, new Vector3(480, 0, 0), 0.1f);
                UI[2].localPosition = Vector3.Lerp(UI[2].localPosition, new Vector3(0, 288, 0), 0.1f);
            UI[3].localPosition = Vector3.Lerp(UI[3].localPosition, new Vector3(0, -288, 0), 0.1f);
            UI[4].localPosition = Vector3.Lerp(UI[4].localPosition, new Vector3(-960, 576, 0), 0.1f);
            UI[5].localPosition = Vector3.Lerp(UI[5].localPosition, new Vector3(-960, -576, 0), 0.1f);

        }
        } else
        {
            transform.position = Vector3.Lerp(transform.position, Vector3.zero, 0.01f);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 4f, 0.05f);
            UI[0].localPosition = Vector3.Lerp(UI[0].localPosition, new Vector3(-820f, 0, 0), 0.1f);
            UI[1].localPosition = Vector3.Lerp(UI[1].localPosition, new Vector3(820f, 0, 0), 0.1f);
            UI[2].localPosition = Vector3.Lerp(UI[2].localPosition, new Vector3(0, 432, 0), 0.1f);
            UI[3].localPosition = Vector3.Lerp(UI[3].localPosition, new Vector3(0, -432, 0), 0.1f);
            UI[4].localPosition = Vector3.Lerp(UI[4].localPosition, new Vector3(-560, 288, 0), 0.1f);

        }
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
