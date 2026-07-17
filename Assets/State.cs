using UnityEngine;
using System.Collections.Generic;


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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        c = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void InitVectorDrawers()
    {
        
        for(int p = 0; p < collider.Length; p++){
        
        
            GameObject g = new GameObject();
            rederobjs.Add(g);
            g.transform.parent = transform;
            
            LineRenderer lr = g.AddComponent(typeof(LineRenderer)) as LineRenderer;
            lr.widthMultiplier = 0.05f;
            lr.material = LineMat;
            lr.loop = true;
            Drawers.Add(lr);
            

        
        }
        
    }

    public void RenderLines()
    {
        PhysicsShapeGroup2D phys = new PhysicsShapeGroup2D();
        for(int p = 0; p < collider.Length; p++){
        
        collider[p].GetShapes(phys);
        Vector2 xAxis = new Vector2(Mathf.Cos(rb.rotation * Mathf.Deg2Rad), Mathf.Sin(rb.rotation * Mathf.Deg2Rad));
        Vector2 yAxis = new Vector2(-1 * xAxis.y, xAxis.x);
        
            List<Vector2> vectors2 = new List<Vector2>();
            vectors2 = points;
            //phys.GetShapeVertices(0, vectors2);
            Drawers[p].positionCount = vectors2.Count;
            Drawers[p].endColor = colour;
            Drawers[p].startColor = colour;
            Drawers[p].sortingOrder = 3 + p;
            for(int q = 0; q < vectors2.Count; q++)
            {
            Drawers[p].SetPosition(q, new Vector3(vectors2[q].x * xAxis.x + vectors2[q].y *yAxis.x,vectors2[q].x * xAxis.y + vectors2[q].y*yAxis.y,0) + transform.position);
            }  
        }
    }
}
