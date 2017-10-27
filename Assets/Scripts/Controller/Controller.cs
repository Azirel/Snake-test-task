using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SnakeModel;

public class Controller : MonoBehaviour
{
    [Range(3, 99)]
    public int gridRows;
    [Range(3, 99)]
    public int gridColumns;

    [SerializeField]
    View view;

    [SerializeField]
    UILabel gameOverLabel;

    public CellView emptyCellViewPrefab;
    public CellView snakeHeadCellViewPrefab;
    public CellView snakeBodyCellViewPrefab;
    public CellView snakeBodySizeIncreaserPrefab;
    public CellView snakeBodySizeDecreaserPrefab;
    public CellView timeAcceleratorPrefab;
    public CellView timeDeceleratorPrefab;
    public CellView headToTailSwitchPrefab;

    public List<Transform> cellGridPositions;

    [Space(22)]
    public KeyCode up = KeyCode.UpArrow;
    public KeyCode down = KeyCode.DownArrow;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode right = KeyCode.RightArrow;

    [Range(0.05f, 99f)]
    public float moveTime = 0.3f;
    [Range(2, 33)]
    public uint snakeStartSize = 3;
    [Range(0.05f, 99f)]
    public float accelerationTimemultiplier = 1.3f;
    [Range(0.05f, 99f)]
    public float decelerationTimeDevider = 1.3f;
    [Range(0.05f, 99f)]
    public float timeImpactDuration = 5f;

    public event EventHandler updateEvent = delegate { };

    Model model;
    Dictionary<CellModelState, CellView> viewToModelBinds;
    MoveDirection currentDirection = MoveDirection.Right;
    bool isContinue = true;

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
        //Getting localPositions from transforms
        Vector3[,] cellPositions = new Vector3[gridRows, gridColumns];
        for (int i = 0; i < gridRows; ++i)
        {
            for (int j = 0; j < gridColumns; ++j)
            {
                cellPositions[i, j] = cellGridPositions[i * gridColumns + j].localPosition;
            }
        }

        //Model initialization
        model = new Model();
        model.Initialize(gridRows, gridColumns, (int)snakeStartSize);
        model.onSnakeClosure += RestartGame;
        model.onSnakeEats += OnSnakeEatsHandler;

        //View initialization
        view.Initialize(emptyCellViewPrefab.GetComponent<CellView>()/*, cellPositions*/, rows: gridRows, columns: gridColumns);
        viewToModelBinds = new Dictionary<CellModelState, CellView>();
        viewToModelBinds.Add(CellModelState.Empty, emptyCellViewPrefab);
        viewToModelBinds.Add(CellModelState.SnakeHead, snakeHeadCellViewPrefab);
        viewToModelBinds.Add(CellModelState.SnakeBody, snakeBodyCellViewPrefab);
        viewToModelBinds.Add(CellModelState.SizeIncreaser, snakeBodySizeIncreaserPrefab);
        viewToModelBinds.Add(CellModelState.SizeDecreaser, snakeBodySizeDecreaserPrefab);
        viewToModelBinds.Add(CellModelState.TimeAccelerator, timeAcceleratorPrefab);
        viewToModelBinds.Add(CellModelState.TimeDecelerator, timeDeceleratorPrefab);
        viewToModelBinds.Add(CellModelState.SnakeHeadSwitch, headToTailSwitchPrefab);

        //Assigning input commands
        updateEvent += DirectionChangeHandler;

        //Launching game loop
        StartCoroutine(GameLoop());
    }

    //Handling things Model do not know about
    private void OnSnakeEatsHandler(CellModelState state)
    {
        switch (state)
        {
            case CellModelState.TimeAccelerator:
                moveTime /= accelerationTimemultiplier;
                StartCoroutine(MultiplyMoveTime(timeImpactDuration, accelerationTimemultiplier));
                break;
            case CellModelState.TimeDecelerator:
                moveTime *= decelerationTimeDevider;
                StartCoroutine(MultiplyMoveTime(timeImpactDuration, 1 / decelerationTimeDevider));
                break;
        }
    }

    public void RestartGame()
    {
        StopAllCoroutines();
        gameOverLabel.enabled = true;
        model.Restart();
        StartCoroutine(GameLoop(5f));
    }

    //Simple looping
    IEnumerator GameLoop(float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        gameOverLabel.enabled = false;
        do
        {
            model.Progress(currentDirection);
            UpdateView(view.Field, model.Field);
            yield return new WaitForSeconds(moveTime);
        } while (isContinue == true);

    }

    //Updating view in accordance with model
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

    //Dumb as hell solution, I know, but I'm too tired
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

    IEnumerator MultiplyMoveTime(float delay, float multiplier)
    {
        yield return new WaitForSeconds(delay);
        moveTime *= multiplier;
    }

}
