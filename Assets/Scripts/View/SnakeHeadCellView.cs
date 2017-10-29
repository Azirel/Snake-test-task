using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeHeadCellView : CellView
{
    public override bool Equals(object other)
    {
        return other is SnakeHeadCellView ? true : false;
    }

    public override void RemoveView()
    {
        //base.RemoveView();
    }
}
