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

    public CellView emptyCellViewPrefab;
    public CellView snakeHeadCellViewPrefab;
    public CellView snakeBodyCellViewPrefab;

    public List<Transform> cellGridPositions;

    // Use this for initialization
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        Vector3[,] cellPositions = new Vector3[gridRows, gridColumns];
        for (int i = 0; i < gridRows; ++i)
        {
            for (int j = 0; j < gridColumns; ++j)
            {
                cellPositions[i, j] = cellGridPositions[i * gridColumns + j].localPosition;
            }
        }
        view.Initialize(emptyCellViewPrefab.GetComponent<CellView>(), cellPositions);
        view.UpdateCellView(view.Field[0, 0], snakeHeadCellViewPrefab);

        for (int i = 0; i < 11; ++i)
        {
            Debug.Log("i = " + i.ToString() + " ,North = " + ((2 * 11 - 1 + i) % 11).ToString() + " ,South = " + ((2 * 11 + 1 + i) % 11).ToString());
            //Debug.Log("i = " + i.ToString() + " ,South = " + ((2 * 11 + 1 + i) % 11).ToString());
        }

    }

}
