using UnityEngine;
using System.Collections.Generic;


public class Congress : MonoBehaviour
{
    public static Congress me;
    public List<Senator> Senate = new List<Senator>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        me = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
