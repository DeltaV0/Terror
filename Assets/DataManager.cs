using UnityEngine;
using TMPro;

public class DataManager : MonoBehaviour
{
    public TMP_Text econText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //float q = 
        econText.text = "Economy:\n\nDebt: " + Nation.me.debt + "\nIncome: <color=green>" + Nation.me.GetTotalStateIncome() + "</color>\nSpending: <color=red>" + Nation.me.GetTotalStateSpending() + "</color>\n\n" + Nation.me.GetTotalStateIncome() + "\n-" + Nation.me.GetTotalStateSpending() + "<size=20> (" + Nation.me.GetProjectStateSpending() + " from projects)</size>\n-" + Nation.me.debtSpending + "<size=20> (Debt minimum)</size>\n--------\n" + Mathf.Clamp(Nation.me.GetTotalStateIncome() - Nation.me.GetTotalStateSpending() - Nation.me.debtSpending, 0, 100000) + "<size=20> for spending</size>\n--------\n" + (Nation.me.debtSpending + Mathf.Clamp(Nation.me.GetTotalStateIncome() - Nation.me.GetTotalStateSpending() - Nation.me.debtSpending, 0, 100000)) + "<size=20> towards debt</size>";
    }
}
