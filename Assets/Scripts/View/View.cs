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
    //Dictionary<CellViewState, CellView> binds;
    Vector3[,] cellsPositions;
    Func<CellView> getCellViewInstance;
    Action<CellView, CellView> replaceView;
    public void Initialize(CellView defaultCellView, Vector3[,] cellsPositions)
    {
        replaceView = ReplaceView;
        this.cellsPositions = cellsPositions;
        field = new CellView[this.cellsPositions.GetLength(0), this.cellsPositions.GetLength(1)];
        int counter = 0;
        for (int i = 0; i < field.GetLength(0); ++i)
        {
            for (int j = 0; j < field.GetLength(1); ++j)
            {
                field[i, j] = NGUITools.AddChild(gameObject, defaultCellView.gameObject).GetComponent<CellView>();
                field[i, j].transform.localPosition = this.cellsPositions[i, j];
                ++counter;
            }
        }
        Debug.Log(counter);
    }

    CellView[,] field;
    public CellView[,] Field
    {
        get { return field; }
        set
        {
            IComparable tempCompable;
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
        if (oldCell.Equals(newCellPrefab) == false)
        {
            replaceView(oldCell, newCellPrefab);
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
