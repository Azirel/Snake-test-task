using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake
{
    GridNode head;
    GridNode tail;
    List<GridNode> body;
    MoveDirection currentDirection;
    Dictionary<MoveDirection, MoveDirection> opposites;
    Dictionary<MoveDirection, Func<GridNode, GridNode>> getNodeBinds;
    public event Action onSnakeEatsItsBody = delegate { };

    public void Initialize(List<GridNode> nodes, int startSize, MoveDirection startDirection = MoveDirection.Right)
    {
        opposites = new Dictionary<MoveDirection, MoveDirection>();
        opposites.Add(MoveDirection.Up, MoveDirection.Down);
        opposites.Add(MoveDirection.Down, MoveDirection.Up);
        opposites.Add(MoveDirection.Left, MoveDirection.Right);
        opposites.Add(MoveDirection.Right, MoveDirection.Left);

        getNodeBinds = new Dictionary<MoveDirection, Func<GridNode, GridNode>>();
        getNodeBinds.Add(MoveDirection.Up, (node) => { return node.Up; });
        getNodeBinds.Add(MoveDirection.Down, (node) => { return node.Down; });
        getNodeBinds.Add(MoveDirection.Left, (node) => { return node.Left; });
        getNodeBinds.Add(MoveDirection.Right, (node) => { return node.Right; });


        body = new List<GridNode>();
        List<GridNode> emptyNodes = nodes.FindAll((x) => { return x.currentState == CellModelState.Empty ? true : false; });
        head = emptyNodes[UnityEngine.Random.Range(0, emptyNodes.Count)];
        //Debug.Log("Head coords: " + head.RowNumber + " : " + head.ColumnNumber);
        //body.Add(head);
        //for (int i = 1; i < startSize; i++)
        //{
        //    body.Add(body[i - 1].Left);
        //    body[i].currentState = CellModelState.SnakeBody;
        //}
        currentDirection = MoveDirection.Right;
    }

    public void MoveSnake(MoveDirection direction)
    {

        if (direction == opposites[currentDirection])
        {
            direction = currentDirection;
        }
        currentDirection = direction;
        head.currentState = CellModelState.Empty;
        head = getNodeBinds[direction](head);
        head.currentState = CellModelState.SnakeHead;
        //GridNode tailGrid = body[body.Count - 1];
        //for (int i = body.Count - 1; i > 0; --i)
        //{
        //    body[i] = body[i - 1];
        //    body[i].currentState = CellModelState.Empty;
        //}
        //tailGrid.currentState = CellModelState.Empty;
        //body[0].currentState = CellModelState.SnakeBody;
        //body[0] = getNodeBinds[direction](body[0]);
        //if (body[0].currentState == CellModelState.SnakeHead)
        //{
        //    onSnakeEatsItsBody();
        //}
        //body[0].currentState = CellModelState.SnakeHead;

    }

}
