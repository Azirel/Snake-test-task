using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeHeadCellView : CellView
{
    public override int CompareTo(object obj)
    {
        //return base.CompareTo(obj);
        if (obj is SnakeHeadCellView == true)
        {
            return 0;
        }
        return -1;
    }

    public override bool Equals(object other)
    {
        //return base.Equals(other);
        Debug.Log("Equation asking in SnakeHeadCellView");
        if (other is SnakeHeadCellView == true)
        {
            return true;
        }
        return false;
    }

    //public static bool operator ==(CellView a, CellView b)
    //{
    //    return System.Object.ReferenceEquals(a, b);
    //}

    //public static bool operator !=(CellView a, CellView b)
    //{
    //    return System.Object.ReferenceEquals(a, b);
    //}
}
