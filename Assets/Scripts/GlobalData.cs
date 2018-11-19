using System.Collections.Generic;
using UnityEngine;

class GlobalData
{
    public static List<GameObject> ItemsOnSkewer = new List<GameObject>();

    public static int Score = 0;
    public static int Lives = 3;

    public static float Acceleration = 1f;

    public static float FirstItemPos = -3.4f;
    public static float ItemDistance = 0.5f;

    public static long MaxScore = 0;
}
