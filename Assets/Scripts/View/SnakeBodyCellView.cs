using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeBodyCellView : CellView
{

    public override int CompareTo(object obj)
    {
        //return base.CompareTo(obj);
        if (obj is SnakeBodyCellView == true)
        {
            return 0;
        }
        return -1;
    }

    public override bool Equals(object other)
    {
        //return base.Equals(other);
        Debug.Log("Equation asking in SnakeBodyCellView");
        if (other is SnakeBodyCellView == true)
        {
            return true;
        }
        return false;
    }
}
