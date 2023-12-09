using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolMath : MonoBehaviour
{
    public static bool a;
    public static bool b;
    public static bool c;
    public static bool d;
    public static bool e;
    public bool f = true;

    void Update()
    {
        if (((((!a && b) || (!c && e)) && ((d && !e) || (a && c)))) == f)
        {
            print("asdasdasdasdasdasdasd");
        }
    }
}
