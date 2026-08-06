using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
//using UnityEngine.UIElements;


public class State : MonoBehaviour
{
    [Header("Drawers")]
    public PolygonCollider2D col;

    public bool hasData;

    public Vector2 screenPosDefault;

    public Vector3 Centroid;

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

    public ForeignNation memberOf;

    public ParticleSystem[] parts;

    [Header("Info")]

    public string Name;

    public List<Rollout> thingsHere = new List<Rollout>();
    public List<Rollout> Infrastructure = new List<Rollout>();

    public float income;

    public int size;

    public float BaseSpending;
    public float ExtraSpending;

    //public float income;

  //  public List<Road> roads;

    //public Button button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
    }

    public float CalculateSpending()
    {
        float simMoney = Nation.me.GetTotalStateIncome();
        simMoney -= BaseSpending;
        float f = 0;
        int p = 1;
        for(int i = 0; i < thingsHere.Count; i++)
        {
            if(thingsHere[i].progress > 0 && CheckIfHasAllPrerequisites(thingsHere[i]) && (simMoney - thingsHere[i].cost) >= 0)
            {
               // thingsHere[i].progress--;
                simMoney -= thingsHere[i].cost;
                f += thingsHere[i].cost;
                p--;
            }
            if(p <= 0)
            {
                break;
            }
        }
        return f;
    }

    public void SubtractBaseSpendingCosts()
    {
        Nation.me.money -= BaseSpending;
        TextParticle.NewParticle(1f, "<color=red>-$"  + BaseSpending + "b</color>", Centroid + (Vector3)Random.insideUnitCircle * 0.2f);
    }

    public void GiveIncome()
    {
        Nation.me.money += income;
        TextParticle.NewParticle(1f, "<color=green>+$"  + income + "b</color>", Centroid + (Vector3)Random.insideUnitCircle * 0.2f);
    }

    public void DoTurn()
    {
        
        int p = 1;
        for(int i = 0; i < thingsHere.Count; i++)
        {
            if(thingsHere[i].progress > 0 && CheckIfHasAllPrerequisites(thingsHere[i]) && (Nation.me.money - thingsHere[i].cost) >= 0)
            {
                thingsHere[i].progress--;
                Nation.me.money -= thingsHere[i].cost;
                TextParticle.NewParticle(1f, "<color=red>-$"  + thingsHere[i].cost + "b</color>", Centroid + (Vector3)Random.insideUnitCircle * 0.2f);
                if(thingsHere[i].progress <= 0)
                {
                    Infrastructure.Add(thingsHere[i]);
                    thingsHere.RemoveAt(i);
                    i--;
                    doThingy();
                }
                p--;
            }
            if(p <= 0)
            {
                break;
            }
        }
    }

    public void doThingy()
    {
        transform.localScale *= 1.1f;
    }

    public Rollout GetRollout(string s)
    {
        for(int i = 0; i < thingsHere.Count; i++)
        {
            if (thingsHere[i].Name.Equals(s))
            {
                return thingsHere[i];
            }
        }
        return null;
    }

    public bool CheckIfHasAllPrerequisites(Rollout r)
    {
        for(int i = 0; i < r.preRequisites.Count; i++)
        {
            if (!CheckIfFinishedRollout(r.preRequisites[i]))
            {
                return false;
            }
        }
        return true;
    }

    public bool CheckIfFinishedRollout(string s)
    {
        for(int i = 0; i < Infrastructure.Count; i++)
        {
            if (Infrastructure[i].Name.Equals(s))
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckIfHasRollout(string s)
    {
        for(int i = 0; i < thingsHere.Count; i++)
        {
            if (thingsHere[i].Name.Equals(s))
            {
                return true;
            }
        }
        for(int i = 0; i < Infrastructure.Count; i++)
        {
            if (Infrastructure[i].Name.Equals(s))
            {
                return true;
            }
        }
        return false;
    }

    public void SetScreenPos()
    {
        screenPosDefault = (Vector2)Camera.main.WorldToScreenPoint(transform.position);
        
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

    public void beforeRendering()
    {
        if(outside && memberOf == null)
        {
            ForeignNation n = Nation.me.NeighborNations[0];
            foreach(ForeignNation q in Nation.me.NeighborNations)
            {
                    if(q.DistSqr(transform.position) < n.DistSqr(transform.position))
                    {
                        n = q;
                    }
            }
            memberOf = n;
            memberOf.subStates.Add(this);
        }
    }

    public void init()
    {
        SetScreenPos();
        if(outside && (ID == 17 || ID == 19 || ID == 22 ))
        {
            GameObject g = Instantiate(GraphicsRenderer.me.NeighborNationPf, Vector3.zero, Quaternion.identity);
            memberOf = g.GetComponent<ForeignNation>();
            memberOf.pos = transform.position;
            memberOf.Init();
            memberOf.subStates.Add(this);
            
        }
        tex = new Texture2D(1280, 720); 
        tex.filterMode = FilterMode.Point;
        tex = SetClearImage(tex);
        
        
       // spriteR.alphaHitTestMinimumThreshold = 0.75f;

       // c = Color.red;
        
        
    }

    public void MoveToFront(){
        
        if (!outside)
        {
            spriteR.sortingOrder = 64;
            for(int i = 0; i < sprRenders.Length; i++)
            {
             sprRenders[i].sortingOrder = 65;
            }
            Nation.me.hoverState = this;
            hovered = true;
        }
    }

    public void MoveToBack(){
        spriteR.sortingOrder = 48 - ID;
        hovered = false;
        if(Nation.me.hoverState == this)
        {
            Nation.me.hoverState = null;
        }
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
        Vector2 v = new Vector2(1280f/2f, 720f/2f);
        tex.Apply();
        spr = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f, 0, SpriteMeshType.Tight);

        mask.sprite = spr;
        MoveToBack();
        
        if (outside)
        {
            spriteR.color = memberOf.col;
            //Debug.Log()
        } else
        {
            spriteR.color = new Color(0, 0.1f, 0, 1f);

        }
        /*  if (outside)
          {
              spriteR.color = new Color(1, 1, 1, 0.1f);

          }*/
        if (hasData)
        {
            spriteR.sprite = spr;
        }
        
            col = gameObject.AddComponent<PolygonCollider2D>();
            Centroid = gameObject.GetComponent<Rigidbody2D>().worldCenterOfMass;

        if (hasData)
        {
            
            for(int i = 0; i < sprRenders.Length; i++)
            {
            sprRenders[i].color = new Color(1, 1, 1, 0.5f);
            sprRenders[i].sprite = spriteR.sprite;
            sprRenders[i].enabled = false;
            }
            for(int i = 0; i < parts.Length; i++)
            {
                var shape = parts[i].shape;
                shape.sprite = spriteR.sprite;
                shape.texture = tex;
                shape.textureColorAffectsParticles = false;
                shape.textureClipThreshold = 0.5f;
                shape.textureAlphaAffectsParticles = false;
                shape.textureBilinearFiltering = false;

                var em = parts[i].emission;
                em.SetBursts(
                    new ParticleSystem.Burst[]{
                        new ParticleSystem.Burst(0f, col.bounds.extents.magnitude * 10f * (ID == 0 ? 2 : 1))
                    });
                //parts[i].enabled = false;
            }
            
        } else
        {
            spriteR.color = Color.clear;
            for(int i = 0; i < sprRenders.Length; i++)
            {
            //sprRenders[i].color = new Color(1, 1, 1, 0.5f);
            sprRenders[i].sprite = null;
            sprRenders[i].enabled = false;
            }
            for(int i = 0; i < parts.Length; i++)
            {
                var shape = parts[i].shape;
                shape.sprite = null;

                
                //parts[i].enabled = false;
            }
        }
        size = 1;
        if(!outside){
        sprRenders[0].enabled = true;
        sprRenders[0].color = new Color(1f, 1f, 1f);
        } else
        {
            sprRenders[0].enabled = true;
            sprRenders[0].color = spriteR.color;
        }

        if(hasData){
        if(Random.Range(0f, 1f) < 0.25f)
        {
            parts[0].Play();
            size = 0;
        } else if(Random.Range(0f, 1f) < 0.5f)
        {
            if(Random.Range(0f, 1f) < 0.25f)
            {
                size = 3;
                parts[3].Play();
            } else
            {
                size = 2;
                parts[2].Play();    
            }
            
        } else
            {
                parts[1].Play();
            }
        }
        if(ID == 0)
        {
            parts[0].Stop();
            parts[1].Stop();
            parts[2].Stop();
            parts[3].Play();
            size = 4;
        }

        
        
        
        normalSize = transform.localScale.x;

        Name = "Sector " + NameGenerator.me.SectorNameGen[Random.Range(0, NameGenerator.me.SectorNameGen.Length)] + "-" + NameGenerator.me.SectorNameGen[Random.Range(0, NameGenerator.me.SectorNameGen.Length)] + "-" + NameGenerator.me.SectorNameGen[Random.Range(0, NameGenerator.me.SectorNameGen.Length)];
        //selected = true;
        BaseSpending = 3 + size;
        income = 1 + (size * 4);
        

        
        //return input;
    }

    public void GetNeighbors()
    {
     //   if (!outside)
        //{
            transform.localScale = new Vector3(normalSize*1.01f, normalSize*1.01f, 1f);
            List<Collider2D> hit = new List<Collider2D>();
            col.Overlap(ContactFilter2D.noFilter, hit);
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
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(normalSize * 1f, normalSize * 1f, 1), 0.1f);

        if(!outside){
        if (hovered)
        {
            
           // transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(normalSize * 1.5f, normalSize * 1.5f, 1), 0.1f);
            spriteR.color = Color.Lerp(spriteR.color, new Color(0.1f, 0.2f, 0.1f), 0.1f);
            sprRenders[0].color = Color.Lerp(sprRenders[0].color, new Color(1f, 0.75f, 0.75f), 0.1f);
           /* for(int i = 1; i < sprRenders.Length; i++)
            {
                sprRenders[i].color = Color.Lerp(sprRenders[i].color, new Color(1, 0.5f, 0.5f, 0.5f), 0.1f);
            }*/
        } else if(Nation.me.selectedState != this)
        {
            MoveToBack();
            spriteR.color = Color.Lerp(spriteR.color, new Color(0, 0.1f, 0), 0.1f);
            sprRenders[0].color = Color.Lerp(sprRenders[0].color, new Color(1f, 1f, 1f), 0.1f);
            for(int i = 1; i < sprRenders.Length; i++)
            {
                sprRenders[i].color = Color.Lerp(sprRenders[i].color, new Color(1, 1f, 1f, 0.5f), 0.1f);
            }

        } else
            {
               spriteR.color = Color.Lerp(spriteR.color, new Color(0.2f, 0.4f, 0.2f), 0.1f);
                sprRenders[0].color = Color.Lerp(sprRenders[0].color, new Color(1f, 0.5f, 0.5f), 0.1f); 
                spriteR.sortingOrder = 64;
                    for(int i = 0; i < sprRenders.Length; i++)
                    {
                        sprRenders[i].sortingOrder = 65;
                    }
            }
        }
    }

}
