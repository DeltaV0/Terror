using UnityEngine;
using TMPro;
using System.Collections.Generic;


public class DataManager : MonoBehaviour
{
    public TMP_Text econText;

    public static DataManager me;

    public LineRenderer lr;
    public LineRenderer lr2;

    private List<float> moneyChart = new List<float>();

    private List<float> supportChart = new List<float>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        me = this;
    }

    public void UpdateCharts()
    {
        moneyChart.Add(Nation.me.money);
        supportChart.Add(Nation.me.GetTotalSupport());
        float max = 0;
        foreach(float f in moneyChart)
        {
            if(f > max)
            {
                max = f;
            }
        }
        float maxSupport = 0;
        foreach(float f in supportChart)
        {
            if(f > maxSupport)
            {
                maxSupport = f;
            }
        }
        lr.positionCount = moneyChart.Count;
        Vector3[] points = new Vector3[lr.positionCount];
        for(int i = 0; i < points.Length; i++)
        {
            points[i] = new Vector3(Mathf.Lerp(-123f, 123f, i/(float)points.Length), Mathf.Lerp(-44f, 44f, moneyChart[i]/max), 0f);
        }
        lr.SetPositions(points);

        lr2.positionCount = supportChart.Count;
        Vector3[] points2 = new Vector3[lr2.positionCount];
        for(int i = 0; i < points2.Length; i++)
        {
            points2[i] = new Vector3(Mathf.Lerp(-123f, 123f, i/(float)points2.Length), Mathf.Lerp(-44f, 44f, supportChart[i]/maxSupport), 0f);
        }
        lr2.SetPositions(points2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //float q = 
        econText.text = "Economy:\n\nDebt: " + Nation.me.debt + "\nIncome: <color=green>" + Nation.me.GetTotalStateIncome() + "</color>\nSpending: <color=red>" + Nation.me.GetTotalStateSpending() + "</color>\n\n" + Nation.me.GetTotalStateIncome() + "\n-" + Nation.me.GetTotalStateSpending() + "<size=20> (" + Nation.me.GetProjectStateSpending() + " from projects)</size>\n-" + Nation.me.debtSpending + "<size=20> (Debt minimum)</size>\n--------\n" + (Nation.me.GetTotalStateIncome() - Nation.me.GetTotalStateSpending() - Nation.me.debtSpending) + "<size=20> for spending</size>\n--------\n" + (Nation.me.debtSpending + Mathf.Clamp(Nation.me.GetTotalStateIncome() - Nation.me.GetTotalStateSpending() - Nation.me.debtSpending, 0, 100000)) + "<size=20> towards debt</size>";
    }
}
