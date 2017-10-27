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



namespace SnakeModel
{
    public static class MyExtensions
    {
        public static T GetRandom<T>(this List<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        public static T GetLast<T>(this List<T> list)
        {
            return list[list.Count - 1];
        }
    }

    public class Model
    {
        public event Action onSnakeClosure = delegate { };
        public event Action<CellModelState> onSnakeEats = delegate { };

        List<GridNode> nodesSingleList;
        List<List<GridNode>> nodesDoubleList;
        Snake snake;
        int minImpactsOnField = 2;
        int maxImpactsOnField = 7;
        List<CellModelState> impacts;
        int newImpactAppearancePossibility = 20;
        List<GridNode> empties
        {
            get
            {
                return nodesSingleList.FindAll((node) => { return node.currentState == CellModelState.Empty ? true : false; });
            }
        }
        public void Initialize(int rows, int columns, int startSize = 3, CellModelState defaultState = CellModelState.Empty)
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
            nodesDoubleList = new List<List<GridNode>>();
            GridNode tempUp;
            GridNode tempDown;
            GridNode tempLeft;
            GridNode tempRight;
            for (int i = 0; i < rows; ++i)
            {
                nodesDoubleList.Add(new List<GridNode>());
                for (int j = 0; j < columns; ++j)
                {
                    tempUp = nodesOrigin[(rows - 1 + i) % rows, j];
                    tempDown = nodesOrigin[(rows + 1 + i) % rows, j];
                    tempLeft = nodesOrigin[i, (columns - 1 + j) % columns];
                    tempRight = nodesOrigin[i, (columns + 1 + j) % columns];
                    nodesOrigin[i, j].Initialize(tempUp, tempDown, tempLeft, tempRight, i, j, defaultState);
                    nodesSingleList.Add(nodesOrigin[i, j]);
                    nodesDoubleList[i].Add(nodesOrigin[i, j]);
                    field[i, j] = defaultState;
                }
            }
            snake = new Snake();
            snake.Initialize(nodesSingleList, startSize);
            snake.onSnakeEats += SnakeImpactedBy;

            impacts = new List<CellModelState>();
            impacts.Add(CellModelState.SizeIncreaser);
            impacts.Add(CellModelState.SizeDecreaser);

            for (int i = 0; i < minImpactsOnField; ++i)
            {
                empties.GetRandom().currentState = impacts.GetRandom();
            }
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
                        field[i, j] = nodesDoubleList[i][j].currentState;
                    }
                }
                return field;
            }
        }

        public void Progress(MoveDirection direction)
        {
            snake.MoveSnake(direction);

            int currentImpactsCount = nodesSingleList.FindAll((node) => { return node.currentState == CellModelState.Empty || node.currentState == CellModelState.SnakeBody || node.currentState == CellModelState.SnakeHead ? false : true; }).Count;

            if (currentImpactsCount < minImpactsOnField)
            {
                for (int i = currentImpactsCount; i <= minImpactsOnField; i++)
                {
                    empties.GetRandom().currentState = impacts.GetRandom();
                }
                currentImpactsCount = minImpactsOnField;
            }

            if (currentImpactsCount < maxImpactsOnField)
            {
                if (UnityEngine.Random.Range(0, newImpactAppearancePossibility) == newImpactAppearancePossibility - 1)
                {
                    empties.GetRandom().currentState = impacts.GetRandom();
                }
            }

        }

        public void SnakeImpactedBy(CellModelState state)
        {
            onSnakeEats(state);
            switch (state)
            {
                case CellModelState.SnakeBody:
                    onSnakeClosure();
                    break;
                case CellModelState.SizeIncreaser:
                    snake.IncreaseBodySize();
                    break;
                case CellModelState.SizeDecreaser:
                    snake.DecreaseBodySize();
                    break;
                default:
                    break;
            }
        }

    }
}
