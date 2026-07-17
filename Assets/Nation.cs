using UnityEngine;
using System.Collections.Generic;


public class Nation : MonoBehaviour
{
    public List<State> states = new List<State>();

    public GameObject statePf;

    public static Nation me;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        me = this;
        for(int i = 0; i < 16; i++)
        {
            GameObject g = Instantiate(statePf, new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f), 0), Quaternion.identity);
            states.Add(g.GetComponent<State>());
            states[i].ID = i;
        }
        /*for(int i = 0; i < states.Count; i++)
        {
            states[i].InitVectorDrawers();
            states[i].RenderLines();
        }*/
        GraphicsRenderer.me.initGraphics();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
