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
    int columnNumber;
    GridNode up;
    public GridNode Up { get { return up; } }
    GridNode down;
    public GridNode Down { get { return down; } }
    GridNode left;
    public GridNode Left { get { return left; } }
    GridNode right;
    public GridNode Right { get { return right; } }
    public GridNode[] Connections { get { return new GridNode[] { Up, Down, Left, Right }; } }

    public void Initialize(GridNode north, GridNode south, GridNode west, GridNode east, int rowNumber = 0, int columnNumber = 0, CellModelState currentState = CellModelState.Empty)
    {
        this.up = north;
        this.down = south;
        this.left = west;
        this.right = east;
        this.rowNumber = rowNumber;
        this.columnNumber = columnNumber;
        this.currentState = currentState;
    }
}
