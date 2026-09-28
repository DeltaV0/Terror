using UnityEngine;
using System.Collections.Generic;

public class NameGenerator : MonoBehaviour
{
    public string[] SectorNameGen;

    public string[] NationNameGen;

    public List<Color> avalNationColors;
    public static NameGenerator me;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        me = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string NationNameGenerate()
    {
        string q = "";
        for(int i = 0; i < Random.Range(2, 5); i++)
        {
            q += NationNameGen[Random.Range(0, NationNameGen.Length)];
        }
        return q;
    }
}
