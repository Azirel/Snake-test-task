using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SnakeModel
{

    public class Snake
    {
        List<GridNode> body;
        MoveDirection currentDirection;
        Dictionary<MoveDirection, MoveDirection> opposites;
        Dictionary<MoveDirection, Func<GridNode, GridNode>> getNodeBinds;
        public event Action<CellModelState> onSnakeEats = delegate { };

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

            RespawnSnake(nodes, startSize);

        }

        public void RespawnSnake(List<GridNode> nodes, int startSize)
        {
            body = new List<GridNode>();
            List<GridNode> emptyNodes = nodes.FindAll((x) => { return x.currentState == CellModelState.Empty ? true : false; });
            body.Add(emptyNodes.GetRandom());
            for (int i = 1; i < startSize; i++)
            {
                body.Add(body[i - 1].Left);
                body[i].currentState = CellModelState.SnakeBody;
            }
            currentDirection = MoveDirection.Right;
        }

        public void MoveSnake(MoveDirection direction)
        {

            if (direction == opposites[currentDirection])
            {
                direction = currentDirection;
            }
            currentDirection = direction;

            GridNode tailGrid = body[body.Count - 1];
            for (int i = body.Count - 1; i > 0; --i)
            {
                body[i] = body[i - 1];
                body[i].currentState = CellModelState.SnakeBody;
            }
            tailGrid.currentState = CellModelState.Empty;
            body[0] = getNodeBinds[direction](body[0]);
            onSnakeEats(body[0].currentState);
            body[0].currentState = CellModelState.SnakeHead;
        }

        public void IncreaseBodySize()
        {
            GridNode newBodyPiece = new List<GridNode>(body[body.Count - 1].Connections).FindLast((connection) => { return connection.currentState == CellModelState.Empty ? true : false; });
            if (newBodyPiece != null)
            {
                newBodyPiece.currentState = CellModelState.SnakeBody;
                body.Add(newBodyPiece);
            }
        }

        public void DecreaseBodySize()
        {
            if (body.Count > 1)
            {
                var last = body.GetLast();
                body.Remove(last);
                last.currentState = CellModelState.Empty;
            }
        }

        public void SwithHeadWithTail()
        {
            body.Reverse();
            currentDirection = opposites[currentDirection];
        }

    }
}
