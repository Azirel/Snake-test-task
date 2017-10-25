using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public int gridRows;
    public int gridColumns;

    public View view;

    public GameObject emptyCellViewPrefab;
    public GameObject snakeHeadCellViewPrefab;
    public GameObject snakeBodyCellViewPrefab;

    public List<Transform> cellGridPositions;

    // Use this for initialization
    void Start()
    {

    }

    public void Initialize()
    {
        Dictionary<CellViewState, GameObject> viewStatesToPrefabsBinds = new Dictionary<CellViewState, GameObject>();
        viewStatesToPrefabsBinds.Add(CellViewState.Empty, emptyCellViewPrefab);
        viewStatesToPrefabsBinds.Add(CellViewState.SnakeHead, snakeHeadCellViewPrefab);
        viewStatesToPrefabsBinds.Add(CellViewState.SnakeBody, snakeBodyCellViewPrefab);
        Vector3[,] cellPositions = new Vector3[gridRows, gridColumns];
        for (int i = 0; i < cellPositions.GetLength(0); ++i)
        {
            for (int j = 0; j < cellPositions.GetLength(1); ++j)
            {
                cellPositions[i, j] = cellGridPositions[cellPositions.GetLength(0) * i + j].localPosition;
            }
        }
        view.Initialize(emptyCellViewPrefab.GetComponent<CellView>(), cellPositions, viewStatesToPrefabsBinds);
    }

}
