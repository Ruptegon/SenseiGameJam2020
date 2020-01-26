using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScore
{
    public static int ServerScore = 0;

    int voult;
    int visitors;

    public void Init(int playerCount)
    {
        voult = IntPow(2, (uint)(playerCount + 3));
        Debug.Log($"ChestScore initiated by {playerCount} players!");
    }

    public int GetClientScore()
    {
        var score = (int)(voult / 2f);
        voult -= score;
        return score;
    }

    public void GiveAllToServer()
    {
        ServerScore += voult;
        voult = 0;
    }

    int IntPow(int x, uint pow)
    {
        int ret = 1;
        while (pow != 0)
        {
            if ((pow & 1) == 1)
                ret *= x;
            x *= x;
            pow >>= 1;
        }
        return ret;
    }
}
