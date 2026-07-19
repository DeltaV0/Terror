using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Vector2 screenPos;

    public Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screenPos = Vector2.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

       if(Nation.me.selectedState == null)
        {
            transform.position = Vector3.Lerp(transform.position, Vector3.Lerp(Vector2.zero, new Vector3(worldPosition.x, worldPosition.y, -10), 0.1f), 0.1f);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 5f, 0.1f);
                   
        } else
        {
                  
                transform.position = Vector3.Lerp(transform.position, Vector3.Lerp(Nation.me.selectedState.transform.position, new Vector3(worldPosition.x, worldPosition.y, -10), 0.02f), 0.1f); 
                   cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 2f, 0.1f);
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
