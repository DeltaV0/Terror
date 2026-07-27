using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;


public class GraphicsRenderer : MonoBehaviour
{
    public Texture2D t;

    public Image image;
    private Sprite spr;

    public Vector2Int resolution;

    public static GraphicsRenderer me;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        me = this;

    }

    public void initGraphics()
    {
                t = new Texture2D(1280 / resolution.x, 720 / resolution.y); 
       t.filterMode = FilterMode.Point;
       spr = Sprite.Create(t, new Rect(0.0f, 0.0f, t.width, t.height), new Vector2(0.5f, 0.5f), 100.0f);
       image.sprite = spr;

       Vector2 pixelPos = Vector2.zero;

       Camera cam = CameraMover.me.cam;
        Vector2 origin = new Vector2(1280f/2f, 720f/2f);
        State s = null;
        float d = 0;
        Color color = new Color(0.1f, 0.25f, 0.75f, 1f);
       for (int y = 0; y < t.height; y++)
        {
            for (int x = 0; x < t.width; x++)
            {
                pixelPos = ConvertToWorldPos(new Vector2(x, y));
                color = new Color(0.1f, 0.25f, 0.75f, 1f);
                d = 4 * PN(x * 0.5f, y * 0.5f) + 3.5f;
                s = null;
                Vector2 StatePos = Vector2.zero;
                for(int i = 0; i < Nation.me.states.Count; i++)
                {
                    StatePos = Nation.me.states[i].transform.position;
                   // float distCurr = 1f * Chebyshev(pixelPos, StatePos + new Vector2(0.1f * PN(x * 1f, y * 1f), 0.1f * PN(x * 1f + 1000, y * 1f + 1000))) + 0.25f * Regular(pixelPos, StatePos + new Vector2(0.1f * PN(x * 1f, y * 1f), 0.1f * PN(x * 1f + 1000, y * 1f + 1000)));
                    float distCurr = Regular(pixelPos, StatePos);
                    if(d > distCurr)
                    {
                        d = distCurr;
                        s = Nation.me.states[i];
                        color = s.c;
                        
                        
                    }
                }
                if(s != null){
                Vector2 Difference = (origin - s.screenPosDefault);
                s.tex.SetPixel(x + (int)Difference.x, y + (int)Difference.y, color);
                s.hasData = true;
                
                if(d < 0.1f){
                  //  s.tex.SetPixel(x + (int)Difference.x, y + (int)Difference.y, Color.white);
                }

                }
                //t.SetPixel(x, y, color);
            }
        }
        //t.Apply(); 

        for(int i = 0; i < Nation.me.states.Count; i++)
                {
                    Nation.me.states[i].finishRendering();
                }
        for(int i = 0; i < Nation.me.states.Count; i++)
                {
                    Nation.me.states[i].GetNeighbors();
                }

        Nation.me.ConnectRoads();
        
    }

    public Vector2 ConvertToWorldPos(Vector2 v)
    {
        Camera c = CameraMover.me.cam;
        Vector2 Temp = v;

        Temp = new Vector2((((float)Temp.x/(float)1280)* 10) - 5, ((((float)Temp.y/(float)720) * 10) - 5) / (16f/9f));
        
        return Temp;
    }

    private float PN(float x, float y)
    {
        return Mathf.PerlinNoise(x * 0.05f + 10000f, y * 0.05f + 100000f);
    }

    private float Taxicab(Vector2 a, Vector2 b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    private float Chebyshev(Vector2 a, Vector2 b)
    {
        return Mathf.Max(Mathf.Abs(a.x - b.x), Mathf.Abs(a.y - b.y));
    }

    private float Regular(Vector2 a, Vector2 b)
    {
        return (a-b).sqrMagnitude;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
