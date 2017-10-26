using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CellModelState
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

public class Model
{

    public void Initialize(CellModelState defaultState, int rows, int columns)
    {
        field = new CellModelState[rows, columns];
        GridNode[,] nodesOrigin = new GridNode[rows, columns];
        nodes = new List<GridNode>();
        GridNode tempNorth;
        GridNode tempSouth;
        GridNode tempWest;
        GridNode tempEast;
        for (int i = 0; i < rows; ++i)
        {
            for (int j = 0; j < columns; ++j)
            {
                tempNorth = nodesOrigin[(2 * rows - 1 + i) % rows, j];
                tempSouth = nodesOrigin[(2 * rows + 1 + i) % rows, j];
                tempWest = nodesOrigin[i, (2 * columns - 1 - j) % columns];
                tempEast = nodesOrigin[i, (2 * columns + 1 - j) % columns];
            }
        }
    }

    CellModelState[,] field;
    CellModelState[,] Field
    {
        get { return field; }
    }

    List<GridNode> nodes;
}
