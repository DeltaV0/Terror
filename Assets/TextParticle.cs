using UnityEngine;
using TMPro;

public class TextParticle : MonoBehaviour
{
    private float t;

    public float time;

    public TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        t = 1f;
    }

    public void Init(float t, string txt)
    {
        t = 1f;
        time = t;
        //text.SetColor(col);
        text.text = txt;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        t -= (1f/time) * 0.02f;
        text.alpha = t;
        if(t <= 0)
        {
            Destroy(gameObject);
        }
    }

    public static void NewParticle(float t, string txt, Vector3 pos)
    {
        GameObject g = Instantiate(Nation.me.textpf, pos, Quaternion.identity);
        g.GetComponent<TextParticle>().Init(t, txt);
    }
}
