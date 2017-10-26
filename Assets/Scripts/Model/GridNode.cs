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
    GridNode north;
    public GridNode North { get { return north; } }
    GridNode south;
    public GridNode South { get { return south; } }
    GridNode west;
    public GridNode West { get { return west; } }
    GridNode east;
    public GridNode East { get { return east; } }
    public GridNode[] Connections { get { return new GridNode[] { North, South, West, East }; } }

    public void Initialize(GridNode north, GridNode south, GridNode west, GridNode east, int rowNumber = 0, int columnNumber = 0, CellModelState currentState = CellModelState.Empty)
    {
        this.north = north;
        this.south = south;
        this.west = west;
        this.east = east;
        this.rowNumber = rowNumber;
        this.columnNumber = columnNumber;
        this.currentState = currentState;
    }
}
