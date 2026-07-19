using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
//using UnityEngine.UIElements;


public class State : MonoBehaviour
{
    [Header("Drawers")]
    public PolygonCollider2D collider;

    public Color c;

    public bool hasData;

    [Header("Data")]

    public int ID;

    public bool outside;

    public Texture2D tex;

    public Vector4 texSize;

    private Sprite spr;

    public SpriteRenderer spriteR;

    private float normalSize;

    public bool hovered;

    public List<State> neighbors;

    public List<State> neighborsOutside;

    public SpriteMask mask;

    public bool selected;

    public SpriteRenderer[] sprRenders;

  //  public List<Road> roads;

    //public Button button;
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
        
       // spriteR.alphaHitTestMinimumThreshold = 0.75f;

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
        spriteR.sortingOrder = 64;
        for(int i = 0; i < sprRenders.Length; i++)
        {
            sprRenders[i].sortingOrder = 65;
        }
        if (!outside)
        {
            Nation.me.hoverState = this;
            hovered = true;
        }
    }

    public void MoveToBack(){
        spriteR.sortingOrder = 48 - ID;
        hovered = false;
        for(int i = 0; i < sprRenders.Length; i++)
        {
            sprRenders[i].sortingOrder = 48 - ID + 1;
        }
    }

    public void Select()
    {
        if(!outside && hovered)
        {
            selected = !selected;
            if(Nation.me.selectedState != null)
            {
                Nation.me.selectedState.selected = false;
                Nation.me.selectedState = null;

            }
            if(selected)
            {
                Nation.me.selectedState = this;
            } else
            {
                Nation.me.selectedState = null;
            }
        }
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
        mask.sprite = spr;
        MoveToBack();
        if (outside)
        {
            spriteR.color = new Color(1, 1, 1, 0.1f);
        }
        if (hasData)
        {
            spriteR.sprite = spr;
            for(int i = 0; i < sprRenders.Length; i++)
        {
            sprRenders[i].color = spriteR.color;
            sprRenders[i].sprite = spr;
            sprRenders[i].enabled = false;
        }
            
        } else
        {
            spriteR.color = new Color(1, 1, 1, 0.0f);
        }
        
        if(Random.Range(0f, 1f) < 0.25f)
        {
            sprRenders[0].enabled = true;
        } else if(Random.Range(0f, 1f) < 0.25f && !outside)
        {
            sprRenders[1].enabled = true;
        }
        if(ID == 0)
        {
            sprRenders[0].enabled = false;
            sprRenders[1].enabled = true;
        }
        
        collider = gameObject.AddComponent<PolygonCollider2D>();
        normalSize = transform.localScale.x;
        //selected = true;
        

        
        //return input;
    }

    public void GetNeighbors()
    {
     //   if (!outside)
        //{
            transform.localScale = new Vector3(normalSize*1.1f, normalSize*1.1f, 1f);
            List<Collider2D> hit = new List<Collider2D>();
            collider.Overlap(ContactFilter2D.noFilter, hit);
            for(int i = 0; i < hit.Count; i++)
            {
                State s = hit[i].transform.gameObject.GetComponent<State>();
                if(s != null && !s.outside && !outside)
                {
                    neighbors.Add(s);
                } else if (s.outside || outside)
                {
                    neighborsOutside.Add(s);
                }
            
            }
            //selected = false;
            transform.localScale = new Vector3(normalSize, normalSize, 1f);
       // }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(!outside){
        if (hovered)
        {
            
           // transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(normalSize * 1.5f, normalSize * 1.5f, 1), 0.1f);
            spriteR.color = Color.Lerp(spriteR.color, new Color(1, 0.5f, 0.5f), 0.1f);
            for(int i = 0; i < sprRenders.Length; i++)
            {
                sprRenders[i].color = spriteR.color;
            }
        } else
        {
            spriteR.color = Color.Lerp(spriteR.color, new Color(1, 1f, 1f), 0.1f);
            for(int i = 0; i < sprRenders.Length; i++)
            {
                sprRenders[i].color = spriteR.color;
            }
           // transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(normalSize * 1f, normalSize * 1f, 1), 0.1f);

        }
        }
    }

}
