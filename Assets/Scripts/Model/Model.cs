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
    SizeDecreaser,
    TimeAccelerator,
    TimeDecelerator,
    SnakeHeadSwitch
}

public enum MoveDirection
{
    Up,
    Down,
    Left,
    Right
}

public class Model
{
    List<GridNode> nodesSingleList;
    List<List<GridNode>> nodesDoubleLists;
    public void Initialize(CellModelState defaultState, int rows, int columns)
    {
        field = new CellModelState[rows, columns];
        GridNode[,] nodesOrigin = new GridNode[rows, columns];
        nodesSingleList = new List<GridNode>();
        nodesDoubleLists = new List<List<GridNode>>();
        GridNode tempNorth;
        GridNode tempSouth;
        GridNode tempWest;
        GridNode tempEast;
        for (int i = 0; i < rows; ++i)
        {
            nodesDoubleLists.Add(new List<GridNode>());
            for (int j = 0; j < columns; ++j)
            {
                tempNorth = nodesOrigin[(2 * rows - 1 + i) % rows, j];
                tempSouth = nodesOrigin[(2 * rows + 1 + i) % rows, j];
                tempWest = nodesOrigin[i, (2 * columns - 1 - j) % columns];
                tempEast = nodesOrigin[i, (2 * columns + 1 - j) % columns];
                nodesOrigin[i, j].Initialize(tempNorth, tempSouth, tempWest, tempEast, i, j, defaultState);
                nodesSingleList.Add(nodesOrigin[i, j]);
                nodesDoubleLists[i].Add(nodesOrigin[i, j]);
                field[i, j] = defaultState;
            }
        }
    }

    CellModelState[,] field;
    CellModelState[,] Field
    {
        get
        {
            for (int i = 0; i < field.GetLength(0); ++i)
            {
                for (int j = 0; j < field.GetLength(1); ++j)
                {
                    field[i, j] = nodesDoubleLists[i][j].currentState;
                }
            }
            return field;
        }
    }

}
