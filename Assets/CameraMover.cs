using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public static CameraMover me;
    public Vector2 screenPos;

    public Camera cam;

    public bool moving;


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
                   
        } else
        {
                  
                transform.position = Vector3.Lerp(transform.position, Vector3.Lerp(Nation.me.selectedState.Centroid, new Vector3(worldPosition.x, worldPosition.y, -10), 0.02f), 0.1f); 
                   cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 2f, 0.1f);
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
