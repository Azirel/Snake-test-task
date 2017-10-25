using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CellViewState
{
    Empty,
    SnakeHead,
    SnakeBody,
    SizeIncreaser,
    SizeDecreasor,
    TimeAccelerator,
    TimeDecelerator,
    SnakeHeadSwitch
}

public class View : MonoBehaviour
{
    Dictionary<CellViewState, GameObject> binds;
    public void Initialize(CellView defaultCellView, Vector3[,] cellsPositions, Dictionary<CellViewState, GameObject> binds)
    {

    }

    CellView[,] field;
    CellView[,] Field
    {
        get { return field; }
        set
        {
            IComparable tempCompable;
            for (int i = 0; i < field.GetLength(0); ++i)
            {
                for (int j = 0; j < field.GetLength(1); ++j)
                {
                    tempCompable = field[i, j];
                    if (tempCompable.CompareTo(value[i, j]) != 0)
                    {
                        field[i, j].RemoveView();
                        NGUITools.AddChild(gameObject, binds[value[i, j].State]);
                    }
                }
            }
        }
    }

}
