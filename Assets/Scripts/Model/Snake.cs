using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake
{
    public void Initialize(List<GridNode> nodes, int startSize, MoveDirection startDirection = MoveDirection.Right)
    {
        List<GridNode> emptiesNodes = nodes.FindAll((x) => { return x.currentState == CellModelState.Empty ? true : false; });
        GridNode randomNode = emptiesNodes[UnityEngine.Random.Range(0, emptiesNodes.Count)];

    }
}
