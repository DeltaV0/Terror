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
       for (int y = 0; y < t.height; y++)
        {
            for (int x = 0; x < t.width; x++)
            {
                pixelPos = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0));
                Color color = Color.blue;
                color.a = 1;
                float d = 4 * PN(x * 0.5f, y * 0.5f) + 3.5f;
                State s = null;
                for(int i = 0; i < Nation.me.states.Count; i++)
                {
                    float distCurr = 1f * Chebyshev(pixelPos, (Vector2)Nation.me.states[i].transform.position + new Vector2(0.1f * PN(x * 1f, y * 1f), 0.1f * PN(x * 1f + 1000, y * 1f + 1000))) + 0.5f * Regular(pixelPos, (Vector2)Nation.me.states[i].transform.position + new Vector2(0.1f * PN(x * 1f, y * 1f), 0.1f * PN(x * 1f + 1000, y * 1f + 1000)));// + 2 * PN(x * 0.5f + 10000, y * 0.5f + 10000);//(pixelPos - (Vector2)Nation.me.states[i].transform.position).sqrMagnitude;
                    if(d > distCurr)
                    {
                        d = distCurr;
                        //s = Nation.me.states[i];
                        color = Nation.me.states[i].c;
                        s = Nation.me.states[i];
                        
                    }
                }
                if(s != null){
                Vector2 Difference = ((Vector2)Camera.main.WorldToScreenPoint(Vector3.zero) - (Vector2)Camera.main.WorldToScreenPoint(s.transform.position));
                s.tex.SetPixel(x + (int)Difference.x, y + (int)Difference.y, color);
                
                if(d < 0.1f){
                    s.tex.SetPixel(x + (int)Difference.x, y + (int)Difference.y, Color.white);
                }

                }
                t.SetPixel(x, y, color);
            }
        }
        t.Apply(); 
        /*for (int y = 0; y < t.height; y++)
        {
            for (int x = 0; x < t.width; x++)
            {
                //pixelPos = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0));
                Color color = Color.white;
                color.a = 1;
                if(t.GetPixel(x+3, y) != t.GetPixel(x, y))
                {
                    t.SetPixel(x, y, color);
                }
                if(t.GetPixel(x, y+3) != t.GetPixel(x, y))
                {
                    t.SetPixel(x, y, color);
                }
                
            }
        }
        t.Apply(); */

        for (int y = 0; y < t.height; y++)
        {
            for (int x = 0; x < t.width; x++)
            {
                pixelPos = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0));
                
                State s = Nation.me.getStateFromColour(t.GetPixel(x, y));
                if(s != null){
                
                }
               // t.SetPixel(x, y, color);
            }
           // t.Apply(); 
        }

        for(int i = 0; i < Nation.me.states.Count; i++)
                {
                    Nation.me.states[i].finishRendering();
                }
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
        return Vector2.Distance(a, b);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
