using UnityEngine;
using TMPro;
using System.Collections;

public class CongressLog : MonoBehaviour
{
    public TMP_Text text;

    public string str;
    public GameObject Initialize(string s)
    {
        str = s;
       // StartCoroutine(go(s));
        return gameObject;
    }

    public void Awake()
    {
        StartCoroutine(go(str));
    } 

    public void FixedUpdate()
    {
        if (!Congress.me.meeting)
        {
            text.color = Color.Lerp(text.color, new Color(1f, 1f, 1f, 0f), 0.1f);
 
        }
    }

    void KN()
    {
        StopAllCoroutines();
            StartCoroutine(KillNow());
    }


    public IEnumerator go(string s)
    {
        for(int i = 0; i < s.Length; i++)
        {
            text.text += s[i];
            yield return new WaitForSeconds(1f/(float)s.Length);

        }
        StartCoroutine(kill());

    }

    public IEnumerator KillNow()
    {
        for(int i = 0; i < text.text.Length - 1; i++)
        {
            text.text.Remove(text.text.Length - 1);
            yield return new WaitForSeconds(0.25f/(float)str.Length);

        }
        
        Destroy(gameObject);
    }

    public IEnumerator kill()
    {
        yield return new WaitForSeconds(3f);
        for(int i = 0; i < text.text.Length - 1; i++)
        {
            text.text.Remove(text.text.Length - 1);
            yield return new WaitForSeconds(1f/(float)str.Length);

        }
        
        Destroy(gameObject);
        
    }
}
