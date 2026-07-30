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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        me = this;
        screenPos = Vector2.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       Vector3 worldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        if(worldPosition.y < -5.5f + transform.position.y)
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
        } else if(worldPosition.y > 5.5f + transform.position.y)
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
       if(Nation.me.selectedState == null)
        {

            transform.position = Vector3.Lerp(transform.position, Vector3.Lerp(Vector2.zero + new Vector2(screenPos.x, screenPos.y * 6f), new Vector3(worldPosition.x, worldPosition.y, -10), 0.1f), 0.1f);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 6f, 0.1f);
            UI[0].localPosition = Vector3.Lerp(UI[0].localPosition, new Vector3(-820f, 0, 0), 0.1f);
            UI[1].localPosition = Vector3.Lerp(UI[1].localPosition, new Vector3(820f, 0, 0), 0.1f);
            UI[2].localPosition = Vector3.Lerp(UI[2].localPosition, new Vector3(0, 432, 0), 0.1f);
            UI[3].localPosition = Vector3.Lerp(UI[3].localPosition, new Vector3(0, -432, 0), 0.1f);
                   
        } else
        {
                  text[0].text = Nation.me.selectedState.Name;
                transform.position = Vector3.Lerp(transform.position, Vector3.Lerp(Nation.me.selectedState.Centroid, new Vector3(worldPosition.x, worldPosition.y, -10), 0.02f), 0.1f); 
                   cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 2f, 0.1f);
                 UI[0].localPosition = Vector3.Lerp(UI[0].localPosition, new Vector3(-480, 0, 0), 0.1f);
                UI[1].localPosition = Vector3.Lerp(UI[1].localPosition, new Vector3(480, 0, 0), 0.1f);
                UI[2].localPosition = Vector3.Lerp(UI[2].localPosition, new Vector3(0, 288, 0), 0.1f);
            UI[3].localPosition = Vector3.Lerp(UI[3].localPosition, new Vector3(0, -288, 0), 0.1f);
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
