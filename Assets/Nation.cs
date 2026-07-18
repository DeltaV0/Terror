using UnityEngine;
using System.Collections.Generic;


public class Nation : MonoBehaviour
{
    public List<State> states = new List<State>();

    public GameObject statePf;

    public static Nation me;

    public Transform canvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        me = this;
        for(int i = 0; i < 16; i++)
        {
            GameObject g = Instantiate(statePf, new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f), 0), Quaternion.identity);
            states.Add(g.GetComponent<State>());
            states[i].ID = i;
            states[i].outside = false;
            states[i].init();
          g.transform.SetParent(canvas, true);
            Debug.Log(i);
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

            Debug.Log(i + "a");

        }

        for(int i = 0; i < 16; i++)
        {
            float q = Random.Range(0, 2f * Mathf.PI);
            GameObject g = Instantiate(statePf, new Vector3(Random.Range(6f, 10f) * Mathf.Cos(q), Random.Range(6f, 10f) * Mathf.Sin(q), 0), Quaternion.identity);
            states.Add(g.GetComponent<State>());
            states[i+ 24].ID = i + 24;
            states[i+ 24].outside = true;
            states[i+ 24].init();
            g.transform.SetParent(canvas, true);

            Debug.Log(i + "b");
        }
        /*for(int i = 0; i < states.Count; i++)
        {
            states[i].InitVectorDrawers();
            states[i].RenderLines();
        }*/
        GraphicsRenderer.me.initGraphics();
    }

    public State getStateFromColour(Color col)
    {
        for(int i = 0; i < states.Count; i++)
        {
            if((Vector4)states[i].c == (Vector4)col)
            {
                return states[i];
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
