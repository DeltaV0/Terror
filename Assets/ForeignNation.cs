using UnityEngine;
using System.Collections.Generic;


public class ForeignNation : MonoBehaviour
{
    public List<State> subStates = new List<State>();

    public Vector3 pos;

    public SpriteRenderer mySprite;

    public Sprite spr2;
    public Texture2D t2;

    public Color col;

    public string name;

    public Color32[] GA;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Init()
    {
        name = NameGenerator.me.NationNameGenerate();
       Nation.me.NeighborNations.Add(this); 
       t2 = new Texture2D(1280*GraphicsRenderer.me.xS2, 720*GraphicsRenderer.me.yS2); 
       GA = new Color32[t2.width * t2.height];
       t2.filterMode = FilterMode.Point;
       spr2 = Sprite.Create(t2, new Rect(0.0f, 0.0f, t2.width, t2.height), new Vector2(0.5f, 0.5f), 100.0f);
        mySprite.sprite = spr2;
        int rnd = Random.Range(0, NameGenerator.me.avalNationColors.Count);
        col = NameGenerator.me.avalNationColors[rnd];//Color.HSVToRGB(Random.Range(0f, 1f) < 0.25f ? Random.Range(0f, 25f) : Random.Range(0.5f, 1f), 1f, 0.2f, true);
        NameGenerator.me.avalNationColors.RemoveAt(rnd);
    }

    public Vector3 avgPos()
    {
        Vector3 vect = subStates[0].transform.position;
        for(int i = 1; i < subStates.Count; i++)
        {
            vect += Vector3.Lerp(vect, subStates[i].transform.position, 0.5f);
        }
        return vect;
    }

    public float DistSqr(Vector3 v)
    {
        return ((v - pos)).sqrMagnitude;
         
         
   }

    public float ClosestDistance(Vector3 v)
    {
        //Vector3 vect = Vector3.zero;
        float d = 100;
        for(int i = 0; i < subStates.Count; i++)
        {
            if((v - subStates[i].transform.position).sqrMagnitude < d)
            {
                d = (v - subStates[i].transform.position).sqrMagnitude;
                //vect = subStates[i].transform.position;
            }
        }
        return d;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
