using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmptyCellView : CellView
{
    public override int CompareTo(object obj)
    {
        return obj is EmptyCellView ? 0 : -1;
    }

    public override bool Equals(object other)
    {
        return other is EmptyCellView ? true : false;
    }
}
