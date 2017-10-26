using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Extensions
{

}

public static class MyExtensions
{
    public static T GetRandom<T>(List<T> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count)];
    }
}
