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

    public SpriteRenderer NationRenderer;

    public Texture2D t2;
    private Sprite spr2;

    public GameObject NeighborNationPf;

    public static GraphicsRenderer me;

    public int xS2;
        public int yS2;

    public int xS; //resolution
        public int yS;

    public static bool DoneLoading;

    public static Color32[] BigAssArrayClear;

    public float waterDistance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        me = this;

    }

    public IEnumerator initGraphicsCrtne()
    { 

        for(int i = 0; i < Nation.me.states.Count ; i++)
                {
                    Nation.me.states[i].beforeRendering();
                }


                yield return new WaitForSeconds(0);
                t = new Texture2D(1280 / resolution.x * xS2, 720 / resolution.y * xS2); 
       t.filterMode = FilterMode.Point;
       spr = Sprite.Create(t, new Rect(0.0f, 0.0f, t.width, t.height), new Vector2(0.5f, 0.5f), 100.0f);

       t2 = new Texture2D(1280 / resolution.x * xS2, 720 / resolution.y * xS2); 
       t2.filterMode = FilterMode.Point;
       spr2 = Sprite.Create(t2, new Rect(0.0f, 0.0f, t2.width, t2.height), new Vector2(0.5f, 0.5f), 100.0f);
        NationRenderer.sprite = spr2;
       image.sprite = spr;

       Vector2 pixelPos = Vector2.zero;

       Camera cam = CameraMover.me.cam;
        Vector2 origin = new Vector2(1280f/2f, 720f/2f);
        State s = null;
        float d = 0;
        Color color = new Color(0.1f, 0.25f, 0.75f, 1f);
        Vector2 RandomOffset = new Vector2(Random.Range(0, 1000000), Random.Range(0, 1000000));
        yield return new WaitForSeconds(0);
       for (int y = 0; y < t.height / yS; y++)
        {
            for (int x = 0; x < t.width / xS; x++)
            {
                int yOff = y * yS;
                int xOff = x * xS;
                
                for(int i = 0; i < Nation.me.NeighborNations.Count; i++)
                {
                    for(int a = 0; a < xS; a++)
                    {
                   for(int q = 0; q < yS; q++)
                    {
                        Nation.me.NeighborNations[i].t2.SetPixel(xOff + a, yOff + q, Color.clear);
                    } 
                    }
                }
                pixelPos = ConvertToWorldPos(new Vector2(xOff - (xS2 > 1 ? t.width/xS2/2 : 0), yOff - (yS2 > 1 ? t.height/xS2/2 : 0)));
                color = new Color(0.1f, 0.25f, 0.75f, 1f);
                d = 3 * PN(xOff * 0.4f, yOff * 0.4f) + waterDistance + (5 * PN(xOff * 0.1f + RandomOffset.x, yOff * 0.1f + RandomOffset.y) - 0.5f);
                s = null;
                Vector2 StatePos = Vector2.zero;
                for(int i = 0; i < Nation.me.states.Count; i++)
                {
                    StatePos = Nation.me.states[i].transform.position;
                    float distCurr = 1f * Chebyshev(pixelPos, StatePos + new Vector2(0.1f * PN(xOff * 1f, yOff * 1f), 0.1f * PN(xOff * 1f + 1000, yOff * 1f + 1000))) + 0.25f * Regular(pixelPos, StatePos + new Vector2(0.1f * PN(xOff * 1f,yOff * 1f), 0.1f * PN(xOff * 1f + 1000, yOff * 1f + 1000)));
                   // float distCurr = Regular(pixelPos, StatePos);
                    if(d > distCurr)
                    {
                        d = distCurr;
                        s = Nation.me.states[i];
                        color = Color.white;
                        
                        
                    }
                }
                d = 1000;
                if(s == null)
                {
                    for(int i = 0; i < Nation.me.Waterstates.Count; i++)
                    {
                    StatePos = Nation.me.Waterstates[i].transform.position;
                    float distCurr = 1f * Chebyshev(pixelPos, StatePos + new Vector2(0.1f * PN(xOff * 1f, yOff * 1f), 0.1f * PN(xOff * 1f + 1000, yOff * 1f + 1000))) + 0.25f * Regular(pixelPos, StatePos + new Vector2(0.1f * PN(xOff * 1f,yOff * 1f), 0.1f * PN(xOff * 1f + 1000, yOff * 1f + 1000)));
                   // float distCurr = Regular(pixelPos, StatePos);
                    if(d > distCurr)
                    {
                        d = distCurr;
                        s = Nation.me.Waterstates[i];
                        color = Color.white;
                        
                        
                    }
                    }
                }
                for(int i = 0; i < xS; i++)
                {
                   for(int q = 0; q < yS; q++)
                    {
                        t2.SetPixel(xOff + i, yOff + q, Color.clear);
                    } 
                }
                

                if(s != null && !s.water){
                Vector2 Difference = (origin - s.screenPosDefault);

                for(int i = 0; i < xS; i++)
                {
                   for(int q = 0; q < yS; q++)
                    {
                        s.tex.SetPixel(xOff + (int)Difference.x + i, yOff + (int)Difference.y + q, Color.white);
                    } 
                }

                s.hasData = true;
                    if (!s.outside)
                    {
                        for(int i = 0; i < xS; i++)
                        {
                        for(int q = 0; q < yS; q++)
                            {
                            t2.SetPixel(xOff + i, yOff + q, Color.white);
                            } 
                        }


                    } else
                    {
                        for(int i = 0; i < xS; i++)
                        {
                        for(int q = 0; q < yS; q++)
                            {
                            s.memberOf.t2.SetPixel(xOff + i, yOff + q, Color.white);
                            } 
                        }

                    }

                } else if(s != null && s.water)
                {
                    Vector2 Difference = (origin - s.screenPosDefault);

                for(int i = 0; i < xS; i++)
                {
                   for(int q = 0; q < yS; q++)
                    {
                        s.tex.SetPixel(xOff + (int)Difference.x + i, yOff + (int)Difference.y + q, Color.white);
                    } 
                }
                s.hasData = true;
                }
                t.SetPixel(xOff, yOff, color);
            }
        }
        yield return new WaitForSeconds(0);
        for(int i = 0; i < Nation.me.NeighborNations.Count; i++)
                {
                    Nation.me.NeighborNations[i].t2.Apply();
                }
        t2.Apply();
        t.Apply(); 
    yield return new WaitForSeconds(0);
        for(int i = 0; i < Nation.me.states.Count; i++)
                {
                    Nation.me.states[i].finishRendering();
                }
                for(int i = 0; i < Nation.me.Waterstates.Count; i++)
                {
                    Nation.me.Waterstates[i].waterRendering();
                }
        yield return new WaitForSeconds(0);
        for(int i = 0; i < Nation.me.states.Count; i++)
                {
                    Nation.me.states[i].GetNeighbors();
                }
                for(int i = 0; i < Nation.me.Waterstates.Count; i++)
                {
                    Nation.me.Waterstates[i].GetNeighbors();
                }
        

        Nation.me.ConnectRoads();
        yield return new WaitForSeconds(0);
        DoneLoading = true;
        Nation.me.debtSpending = Mathf.Round(Nation.me.GetTotalStateIncome() * 0.20f);
        Nation.me.debt = Mathf.Round(Nation.me.GetTotalStateIncome() * 25f);
        Nation.me.nationalSpendingBase = Nation.me.debtSpending * 0.25f;
        Nation.me.money = Nation.me.GetTotalStateIncome() - Nation.me.debtSpending - Nation.me.nationalSpendingBase - Nation.me.GetTotalStateSpending();
        Nation.me.StartTurn();
        yield return new WaitForSeconds(0);
        
    }

    public void initGraphics()
    {
      StartCoroutine(initGraphicsCrtne());
    }

    public Vector2 ConvertToWorldPos(Vector2 v)
    {
        Camera c = CameraMover.me.cam;
        Vector2 Temp = v;

        Temp = new Vector2((((float)Temp.x/(float)1280)* 20f) - 10f, ((((float)Temp.y/(float)720) * 20f) - 10f) / (16f/9f));
        
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
