using UnityEngine;

public class Lib
{
    public static float NormRand(float center, float stdev)
    {
        float f = 0;
        for(int i = -2; i < 2; i++)
        {
            f+= (Random.value - 0.5f ) * stdev * Mathf.Pow(2, i);
        }
        f += center;
        return f;
    }
}
