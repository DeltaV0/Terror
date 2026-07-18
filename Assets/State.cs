using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
//using UnityEngine.UIElements;


public class State : MonoBehaviour
{
    [Header("Drawers")]
    public Collider2D[] collider;

    public Color colour;

    public Material LineMat;

    public List<GameObject> rederobjs = new List<GameObject>();
    public List<LineRenderer> Drawers = new List<LineRenderer>();

    public Rigidbody2D rb;

    public List<Vector2> points = new List<Vector2>();

    public Color c;

    [Header("Data")]

    public int ID;

    public bool outside;

    public Texture2D tex;

    public Vector4 texSize;

    private Sprite spr;

    public Image spriteR;

    public Button button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
    }

    public Texture2D SetClearImage(Texture2D input){
        for (int y = 0; y < input.height; y++)
        {
            for (int x = 0; x < input.width; x++)
            {
                input.SetPixel(x, y,  Color.clear);
               // t.SetPixel(x, y, color);
            }
           // t.Apply(); 
        }
        return input;
    }

    public void init()
    {
        tex = new Texture2D(1280, 720); 
        tex.filterMode = FilterMode.Point;
        tex = SetClearImage(tex);
        spr = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        spriteR.sprite = spr;
        spriteR.alphaHitTestMinimumThreshold = 0.75f;

        c = Color.red;
        if (outside)
        {
            c = new Color(0.1f, 0, 0, 1f);
            //Debug.Log()
        } else
        {
            c = new Color(0, 0.1f ,0, 1f);

        }
    }

    public void MoveToFront(){
        transform.SetSiblingIndex(0);
    }

    public void finishRendering(){
        for (int y = tex.height; y > 0; y--)
        {
            for (int x = tex.width; x > 0; x--)
            {
                Vector2 Difference = ((Vector2)Camera.main.WorldToScreenPoint(Vector3.zero) - (Vector2)Camera.main.WorldToScreenPoint(transform.position));
                if(tex.GetPixel(x + (int)Difference.x + 4,y + (int)Difference.y) == Color.clear && tex.GetPixel(x + (int)Difference.x,y + (int)Difference.y) != Color.clear){
                    tex.SetPixel(x + (int)Difference.x, y + (int)Difference.y, Color.white);

                }
                if(tex.GetPixel(x + (int)Difference.x,y + (int)Difference.y + 4) == Color.clear && tex.GetPixel(x + (int)Difference.x,y + (int)Difference.y) != Color.clear){
                    tex.SetPixel(x + (int)Difference.x, y + (int)Difference.y, Color.white);

                }

                if(tex.GetPixel(x + (int)Difference.x - 1,y + (int)Difference.y) != Color.clear && tex.GetPixel(x + (int)Difference.x,y + (int)Difference.y) == Color.clear){
                    tex.SetPixel(x + (int)Difference.x, y + (int)Difference.y, Color.white);

                }
                if(tex.GetPixel(x + (int)Difference.x,y + (int)Difference.y - 1) != Color.clear && tex.GetPixel(x + (int)Difference.x,y + (int)Difference.y) == Color.clear){
                    tex.SetPixel(x + (int)Difference.x, y + (int)Difference.y, Color.white);

                }
               
            }
           
        }
        //combine at some point!!
        for (int y = 0; y < tex.height; y++)
        {
            for (int x = 0; x < tex.width; x++)
            {
                Vector2 Difference = ((Vector2)Camera.main.WorldToScreenPoint(Vector3.zero) - (Vector2)Camera.main.WorldToScreenPoint(transform.position));
                if(tex.GetPixel(x + (int)Difference.x - 4,y + (int)Difference.y) == Color.clear && tex.GetPixel(x + (int)Difference.x,y + (int)Difference.y) != Color.clear){
                    tex.SetPixel(x + (int)Difference.x, y + (int)Difference.y, Color.white);

                }
                if(tex.GetPixel(x + (int)Difference.x,y + (int)Difference.y - 4) == Color.clear && tex.GetPixel(x + (int)Difference.x,y + (int)Difference.y) != Color.clear){
                    tex.SetPixel(x + (int)Difference.x, y + (int)Difference.y, Color.white);

                }

                if(tex.GetPixel(x + (int)Difference.x + 1,y + (int)Difference.y) != Color.clear && tex.GetPixel(x + (int)Difference.x,y + (int)Difference.y) == Color.clear){
                    tex.SetPixel(x + (int)Difference.x, y + (int)Difference.y, Color.white);

                }
                if(tex.GetPixel(x + (int)Difference.x,y + (int)Difference.y + 1) != Color.clear && tex.GetPixel(x + (int)Difference.x,y + (int)Difference.y) == Color.clear){
                    tex.SetPixel(x + (int)Difference.x, y + (int)Difference.y, Color.white);

                }
               
            }
           
        }
        tex.Apply();
        //return input;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

}
