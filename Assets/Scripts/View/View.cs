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
    Vector3[,] cellsPositions;
    Func<CellView> getCellViewInstance;
    Action<CellView, CellView> replaceView;
    public void Initialize(CellView defaultCellView, Vector3[,] cellsPositions)
    {
        replaceView = ReplaceView;
        this.cellsPositions = cellsPositions;
        field = new CellView[this.cellsPositions.GetLength(0), this.cellsPositions.GetLength(1)];
        for (int i = 0; i < field.GetLength(0); ++i)
        {
            for (int j = 0; j < field.GetLength(1); ++j)
            {
                field[i, j] = NGUITools.AddChild(gameObject, defaultCellView.gameObject).GetComponent<CellView>();
                field[i, j].transform.localPosition = this.cellsPositions[i, j];
            }
        }
    }

    CellView[,] field;
    public CellView[,] Field
    {
        get { return field; }
        set
        {
            for (int i = 0; i < field.GetLength(0); ++i)
            {
                for (int j = 0; j < field.GetLength(1); ++j)
                {
                    if (field[i, j].Equals(value[i, j]) == false)
                    {
                        UpdateCellView(field[i, j], value[i, j]);
                    }
                }
            }
        }
    }

    public void UpdateCellView(CellView oldCell, CellView newCellPrefab)
    {
        //if (newCellPrefab.Equals(oldCell) == false)
        //{
        //    replaceView(oldCell, newCellPrefab);
        //    oldCell
        //}
        //if (((IComparable)oldCell).CompareTo(newCellPrefab) != 0)
        //{

        //}
    }

    public void UpdateCellView(int row, int column, CellView newCellPrefab)
    {
        if (newCellPrefab.Equals(field[row, column]) == false)
        {
            field[row, column].RemoveView();
            field[row, column] = NGUITools.AddChild(gameObject, newCellPrefab.gameObject).GetComponent<CellView>();
            field[row, column].transform.localPosition = cellsPositions[row, column];
        }
    }

    void ReplaceView(CellView replacedCellView, CellView newViewPrefab)
    {
        var temp = replacedCellView;
        replacedCellView = NGUITools.AddChild(gameObject, newViewPrefab.gameObject).GetComponent<CellView>();
        replacedCellView.transform.localPosition = temp.transform.localPosition;
        temp.RemoveView();
    }

}
