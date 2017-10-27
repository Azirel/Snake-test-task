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

    public event Action onSnakeClosure = delegate { };

    List<GridNode> nodesSingleList;
    List<List<GridNode>> nodesDoubleLists;
    Snake snake;
    public void Initialize(CellModelState defaultState, int rows, int columns)
    {
        field = new CellModelState[rows, columns];
        GridNode[,] nodesOrigin = new GridNode[rows, columns];
        for (int i = 0; i < rows; ++i)
        {
            for (int j = 0; j < columns; j++)
            {
                nodesOrigin[i, j] = new GridNode();
            }
        }
        nodesSingleList = new List<GridNode>();
        nodesDoubleLists = new List<List<GridNode>>();
        GridNode tempUp;
        GridNode tempDown;
        GridNode tempLeft;
        GridNode tempRight;
        for (int i = 0; i < rows; ++i)
        {
            nodesDoubleLists.Add(new List<GridNode>());
            for (int j = 0; j < columns; ++j)
            {
                tempUp = nodesOrigin[(rows - 1 + i) % rows, j];
                tempDown = nodesOrigin[(rows + 1 + i) % rows, j];
                tempLeft = nodesOrigin[i, (columns - 1 + j) % columns];
                tempRight = nodesOrigin[i, (columns + 1 + j) % columns];
                nodesOrigin[i, j].Initialize(tempUp, tempDown, tempLeft, tempRight, i, j, defaultState);
                nodesSingleList.Add(nodesOrigin[i, j]);
                nodesDoubleLists[i].Add(nodesOrigin[i, j]);
                field[i, j] = defaultState;
            }
        }
        snake = new Snake();
        snake.Initialize(nodesSingleList, 3);
        snake.onSnakeEatsItsBody += () => onSnakeClosure();
        onSnakeClosure += () => Debug.Log("End of story");
    }

    CellModelState[,] field;
    public CellModelState[,] Field
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

    public void Move(MoveDirection direction)
    {
        snake.MoveSnake(direction);
    }

}
