using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeBodyCellView : CellView
{
    public override bool Equals(object other)
    {
        return other is SnakeBodyCellView ? true : false;
    }
}
