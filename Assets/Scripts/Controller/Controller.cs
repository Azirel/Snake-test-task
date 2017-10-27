using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SnakeModel;

public class Controller : MonoBehaviour
{
    public int gridRows;
    public int gridColumns;

    public View view;

    public CellView emptyCellViewPrefab;
    public CellView snakeHeadCellViewPrefab;
    public CellView snakeBodyCellViewPrefab;
    public CellView snakeBodySizeIncreaserPrefab;
    public CellView snakeBodySizeDecreaserPrefab;

    public List<Transform> cellGridPositions;

    [Space(22)]
    public KeyCode up = KeyCode.UpArrow;
    public KeyCode down = KeyCode.DownArrow;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode right = KeyCode.RightArrow;

    public float delayTime = 0.3f;
    public uint snakeStartSize = 3;

    public event EventHandler updateEvent = delegate { };

    Model model;
    Dictionary<CellModelState, CellView> viewToModelBinds;
    MoveDirection currentDirection = MoveDirection.Right;
    // Use this for initialization
    void Start()
    {
        Initialize();
    }

    private void Update()
    {
        updateEvent(this, null);
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

        model = new Model();
        model.Initialize(gridRows, gridColumns, (int)snakeStartSize);
        model.onSnakeClosure += () => Debug.Log("Game over");

        view.Initialize(emptyCellViewPrefab.GetComponent<CellView>(), cellPositions);
        viewToModelBinds = new Dictionary<CellModelState, CellView>();
        viewToModelBinds.Add(CellModelState.Empty, emptyCellViewPrefab);
        viewToModelBinds.Add(CellModelState.SnakeHead, snakeHeadCellViewPrefab);
        viewToModelBinds.Add(CellModelState.SnakeBody, snakeBodyCellViewPrefab);
        viewToModelBinds.Add(CellModelState.SizeIncreaser, snakeBodySizeIncreaserPrefab);
        viewToModelBinds.Add(CellModelState.SizeDecreaser, snakeBodySizeDecreaserPrefab);

        updateEvent += DirectionChangeHandler;

        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        do
        {
            model.Progress(currentDirection);
            UpdateView(view.Field, model.Field);
            yield return new WaitForSeconds(delayTime);
        } while (true);

    }

    void UpdateView(CellView[,] viewGrid, CellModelState[,] modelGrid)
    {
        for (int i = 0; i < modelGrid.GetLength(0); ++i)
        {
            for (int j = 0; j < modelGrid.GetLength(1); ++j)
            {
                //view.UpdateCellView(viewGrid[i, j], viewToModelBinds[modelGrid[i, j]]);
                view.UpdateCellView(i, j, viewToModelBinds[modelGrid[i, j]]);
            }
        }
    }

    void DirectionChangeHandler(object sender, EventArgs args)
    {
        if (Input.GetKeyDown(up) == true)
        {
            currentDirection = MoveDirection.Up;
        }
        else if (Input.GetKeyDown(down) == true)
        {
            currentDirection = MoveDirection.Down;
        }
        else if (Input.GetKeyDown(left) == true)
        {
            currentDirection = MoveDirection.Left;
        }
        else if (Input.GetKeyDown(right) == true)
        {
            currentDirection = MoveDirection.Right;
        }
    }

}
