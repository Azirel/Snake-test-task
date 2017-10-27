using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccelerationCellView : CellView
{
    public override bool Equals(object other)
    {
        return other is AccelerationCellView ? true : false;
    }
}
