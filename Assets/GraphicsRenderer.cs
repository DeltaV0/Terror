using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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
                Color color = Color.black;
                color.a = 1;
                float d = 2 * PN(x, y) + 2;
                for(int i = 0; i < Nation.me.states.Count; i++)
                {
                    float distCurr = Taxicab(pixelPos, (Vector2)Nation.me.states[i].transform.position) + PN(x, y);//(pixelPos - (Vector2)Nation.me.states[i].transform.position).sqrMagnitude;
                    if(d > distCurr)
                    {
                        d = distCurr;
                        //s = Nation.me.states[i];
                        color = Nation.me.states[i].c;
                        
                    }
                }
                t.SetPixel(x, y, color);
            }
        }
        t.Apply(); 
        for (int y = 0; y < t.height; y++)
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
        t.Apply(); 
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
