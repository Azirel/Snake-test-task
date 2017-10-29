using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CellViewState
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

public class View : MonoBehaviour
{
    [SerializeField]
    UIGrid gridFormer;

    [SerializeField]
    UIWidget gridFormerWidget;

    [Space(22)]
    [SerializeField]
    UILabel gameOverLabel;

    [SerializeField]
    UILabel gameSuccessLabel;

    [Range(10, 111)]
    public int cellWidth;

    [Range(10, 111)]
    public int cellHeight;

    Vector3[,] cellsPositions;
    Func<CellView, CellView> getCellViewInstance; // Pooling objects intented, but I'm too tired, maybe later

    List<CellView> cellList
    {
        get
        {
            List<CellView> result = new List<CellView>();
            foreach (var cell in field)
            {
                result.Add(cell);
            }
            return result;
        }
    }

    public void Initialize(CellView defaultCellView, Vector3[,] cellsPositions = null, int rows = 11, int columns = 19)
    {
        if (cellsPositions == null || cellsPositions.Length < 1)
        {
            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < columns; ++j)
                {
                    NGUITools.AddChild(gridFormer.gameObject, gridFormerWidget.gameObject).gameObject.SetActive(true);
                }
            }
            gridFormer.cellWidth = cellWidth;
            gridFormer.cellHeight = cellHeight;
            gridFormer.maxPerLine = columns;
            gridFormer.Reposition();
            cellsPositions = new Vector3[rows, columns];
            List<Transform> cells = new List<Transform>(gridFormer.GetComponentsInChildren<Transform>());
            cells.RemoveAt(0);
            for (int i = 0; i < cells.Count; ++i)
            {
                cellsPositions[i / columns, i % columns] = cells[i].localPosition;
            }
        }

        this.cellsPositions = cellsPositions;
        field = new CellView[this.cellsPositions.GetLength(0), this.cellsPositions.GetLength(1)];
        for (int i = 0; i < field.GetLength(0); ++i)
        {
            for (int j = 0; j < field.GetLength(1); ++j)
            {
                field[i, j] = NGUITools.AddChild(gameObject, defaultCellView.gameObject).GetComponent<CellView>();
                field[i, j].transform.localPosition = this.cellsPositions[i, j];
            }
        }

    }

    CellView[,] field;
    public CellView[,] Field
    {
        get { return field; }
        set
        {
            for (int i = 0; i < field.GetLength(0); ++i)
            {
                for (int j = 0; j < field.GetLength(1); ++j)
                {
                    if (field[i, j].Equals(value[i, j]) == false)
                    {
                        UpdateCellView(field[i, j], value[i, j]);
                    }
                }
            }
        }
    }

    //Does not work
    public void UpdateCellView(CellView oldCell, CellView newCellPrefab)
    {
        if (newCellPrefab.Equals(oldCell) == false)
        {

            var temp = oldCell;
            oldCell = NGUITools.AddChild(gameObject, newCellPrefab.gameObject).GetComponent<CellView>();
            oldCell.transform.localPosition = temp.transform.localPosition;
            temp.RemoveView();
        }
    }

    CellView snakeHead;
    //This works fine
    public void UpdateCellView(int row, int column, CellView newCellPrefab)
    {
        if (newCellPrefab.Equals(field[row, column]) == false)
        {
            field[row, column].RemoveView();
            if (newCellPrefab is SnakeHeadCellView)
            {
                if (snakeHead == null)
                {
                    snakeHead = NGUITools.AddChild(gameObject, newCellPrefab.gameObject).GetComponent<CellView>();
                }
                field[row, column] = snakeHead;
            }
            else
            {
                field[row, column] = NGUITools.AddChild(gameObject, newCellPrefab.gameObject).GetComponent<CellView>(); 
            }
            field[row, column].SetView(cellsPositions[row, column]);
        }
    }

    public void ShowGameOverLabel(bool value)
    {
        gameOverLabel.enabled = value;
    }

    public void ShowGameSuccessLabel(bool value)
    {
        gameSuccessLabel.enabled = value;
    }

}
