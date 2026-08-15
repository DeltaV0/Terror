using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;



public class Congress : MonoBehaviour
{
    public static Congress me;
    public List<Senator> Senate = new List<Senator>();

    public bool meeting;

    public Image[] CongressBG;

    public CongressLog log;
    public GameObject logParent;

    public bool voting;

    //public int LogCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        me = this;
    }

    public void Meet()
    {
        StartCoroutine(CongressMeeting());

    }

     public IEnumerator CongressMeeting()
    {
        if(Nation.me.policiesToVoteOn.Count > 0){
        CongressBG[0].gameObject.SetActive(true);
        CongressBG[1].gameObject.SetActive(true);
        CongressBG[2].gameObject.SetActive(true);
        Nation.me.politiview = true;
        meeting = true;
        yield return new WaitForSeconds(0.5f);
        Instantiate(log.Initialize("Begin Meeting!"), logParent.transform).transform.position = Vector3.zero;
        for(int i = 0; i < Senate.Count; i++)
        {
            float deg = (Mathf.Repeat(i, 8f) / 8f) * Mathf.PI * 2;
            float dist = ((int)(i/8) * 1.5f) + 3f;
            float off = 0f;//(0.5f * Mathf.PI) + ((-1f/16f) * Mathf.PI);
            Senate[i].home.targetPosBubble = new Vector3(Mathf.Sin(deg - off) * dist, Mathf.Cos(deg - off) * dist, 0f); 
            yield return new WaitForSeconds(1f/16f);

        }
        yield return new WaitForSeconds(2f);
        Instantiate(log.Initialize("Meeting adjurned!"), logParent.transform).transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.5f);
        meeting = false;
        for(int i = 0; i < Senate.Count; i++)
        {
            Senate[i].home.targetPosBubble = Senate[i].home.Centroid;
            yield return new WaitForSeconds(1f/16f);

        }
        logParent.BroadcastMessage("KN");

        yield return new WaitForSeconds(0.5f);
        }
        Nation.me.politiview = false;
        CongressBG[0].gameObject.SetActive(false);
        CongressBG[1].gameObject.SetActive(false);
        CongressBG[2].gameObject.SetActive(false);
        Nation.me.StartTurn();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (meeting)
        {
            CongressBG[0].color = Color.Lerp(CongressBG[0].color, new Color(0.2f, 0.2f, 0.2f, 1f), 0.1f);
            CongressBG[2].color = Color.Lerp(CongressBG[2].color, new Color(0.4f, 0.4f, 0.4f, 1f), 0.1f);

            CongressBG[1].transform.position = Vector3.Lerp(CongressBG[1].transform.position, Vector3.zero, 0.1f);
        } else
        {
            CongressBG[0].color = Color.Lerp(CongressBG[0].color, new Color(0.2f, 0.2f, 0.2f, 0f), 0.1f);
            CongressBG[2].color = Color.Lerp(CongressBG[2].color, new Color(0.4f, 0.4f, 0.4f, 0f), 0.1f);

            CongressBG[1].transform.position = Vector3.Lerp(CongressBG[1].transform.position, new Vector3(0f, -100f, 0f), 0.1f);

        }
    }
}
