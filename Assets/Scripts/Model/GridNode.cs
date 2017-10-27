using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridNode
{
    public CellModelState currentState;
    int rowNumber;
    public int RowNumber { get { return rowNumber; } }
    int columnNumber;
    public int ColumnNumber { get { return columnNumber; } }
    GridNode up;
    public GridNode Up { get { return up; } }
    GridNode down;
    public GridNode Down { get { return down; } }
    GridNode left;
    public GridNode Left { get { return left; } }
    GridNode right;
    public GridNode Right { get { return right; } }
    public GridNode[] Connections { get { return new GridNode[] { Up, Down, Left, Right }; } }

    public void Initialize(GridNode up, GridNode down, GridNode left, GridNode right, int rowNumber = 0, int columnNumber = 0, CellModelState currentState = CellModelState.Empty)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
        this.rowNumber = rowNumber;
        this.columnNumber = columnNumber;
        this.currentState = currentState;
    }
}
