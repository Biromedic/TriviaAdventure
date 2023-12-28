using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Grid_Code : MonoBehaviour
{
    public ShapeStorage shapeStorage;
    public Text scoreText;
    public int columns = 0;
    public int rows = 0;
    public float squaresGap = 0.1f;
    public GameObject gridSquare;
    public Vector2 startPosition = new Vector2(0.0f, 0.0f);
    public float squareScale = 0.5f;
    public float everySquareOffset = 0.0f;

    private Vector2 _offset = new Vector2(0.0f, 0.0f);
    private List<GameObject> _gridSquares = new List<GameObject>();
    private LineInducator _lineInducator;

    private void OnDisable()
    {
        GameEvent.CheckIfShapeCanBePlaced -= CheckIfShapeCanBePlaced;
    }

    private void OnEnable()
    {
        GameEvent.CheckIfShapeCanBePlaced += CheckIfShapeCanBePlaced;
    }

    void Start()
    {
        _lineInducator = GetComponent<LineInducator>();
        CreateGrid();
    }

    private void Update()
    {
        // Check if the player lost on every frame
        CheckIfPlayerLost();
    }

    private void CreateGrid()
    {
        SpawnGridSquares();
        SetGridSquaresPosition();
    }

    private void SpawnGridSquares()
    {
        int squareIndex = 0;

        for (int row = 0; row < rows; ++row)
        {
            for (int column = 0; column < columns; ++column)
            {
                GameObject newSquare = Instantiate(gridSquare);
                GridSquare gridSquareComponent = newSquare.GetComponent<GridSquare>();

                _gridSquares.Add(newSquare);

                gridSquareComponent.SquareIndex = squareIndex;
                newSquare.transform.SetParent(transform);
                newSquare.transform.localScale = new Vector3(squareScale, squareScale, squareScale);
                gridSquareComponent.SetImage(_lineInducator.GetGridSquareIndex(squareIndex) % 2 == 0);

                squareIndex++;
            }
        }
    }

    private void SetGridSquaresPosition()
    {
        int columnNumber = 0;
        int rowNumber = 0;
        Vector2 squareGapNumber = new Vector2(0.0f, 0.0f);
        bool rowMoved = false;

        RectTransform squareRect = _gridSquares[0].GetComponent<RectTransform>();

        _offset.x = squareRect.rect.width * squareRect.transform.localScale.x + everySquareOffset;
        _offset.y = squareRect.rect.height * squareRect.transform.localScale.y + everySquareOffset;

        foreach (GameObject square in _gridSquares)
        {
            if (columnNumber + 1 > columns)
            {
                squareGapNumber.x = 0;
                columnNumber = 0;
                rowNumber++;
                rowMoved = false;
            }

            float posXOffset = _offset.x * columnNumber + (squareGapNumber.x * squaresGap);
            float posYOffset = _offset.y * rowNumber + (squareGapNumber.y * squaresGap);

            if (columnNumber > 0 && columnNumber % 3 == 0)
            {
                squareGapNumber.x++;
                posXOffset += squaresGap;
            }

            if (rowNumber > 0 && rowNumber % 3 == 0 && !rowMoved)
            {
                rowMoved = true;
                squareGapNumber.y++;
                posYOffset += squaresGap;
            }

            Vector2 anchoredPosition = new Vector2(startPosition.x + posXOffset, startPosition.y - posYOffset);

            square.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;

            columnNumber++;
        }
    }

    private void CheckIfShapeCanBePlaced()
    {
        List<int> squareIndexes = new List<int>();

        foreach (GameObject square in _gridSquares)
        {
            GridSquare gridSquareComponent = square.GetComponent<GridSquare>();

            if (gridSquareComponent.Selected && !gridSquareComponent.SquareOccupied)
            {
                squareIndexes.Add(gridSquareComponent.SquareIndex);
                gridSquareComponent.Selected = false;
            }
        }

        Shape currentSelectedShape = shapeStorage.GetCurrentSelectedShape();

        if (currentSelectedShape == null)
        {
            return;
        }

        if (currentSelectedShape.TotalSquareNumber == squareIndexes.Count)
        {
            foreach (int squareIndex in squareIndexes)
            {
                _gridSquares[squareIndex].GetComponent<GridSquare>().PlaceShapeOnBoard();
            }

            int shapeLeft = 0;

            foreach (Shape shape in shapeStorage.shapeList)
            {
                if (shape.IsOnStartPosition() && shape.IsAnyOfShapeSquareActive())
                {
                    shapeLeft++;
                }
            }

            if (shapeLeft == 0)
            {
                GameEvent.RequestNewShapes();
            }
            else
            {
                GameEvent.SetShapeInactive();
            }

            CheckIfAnyLineCompleted();
        }
        else
        {
            GameEvent.MoveShapeToStartPosition();
        }
    }

    void CheckIfAnyLineCompleted()
    {
        List<int[]> lines = new List<int[]>();

        //int counter = 0;

        // Columns
        for (int column = 0; column < 9; column++)
        {
            int[] data = new int[9];

            for (int index = 0; index < 9; index++)
            {
                data[index] = _lineInducator.line_data[index, column];
            }

            lines.Add(data);
        }

        // Rows
        for (int row = 0; row < 9; row++)
        {
            int[] data = new int[9];

            for (int index = 0; index < 9; index++)
            {
                data[index] = _lineInducator.line_data[row, index];
            }

            lines.Add(data);
        }

        // Squares
        for (int square = 0; square < 9; square++)
        {
            int[] data = new int[9];

            for (int index = 0; index < 9; index++)
            {
                data[index] = _lineInducator.square_data[square, index];
            }

            lines.Add(data);
        }

        int completedLines = CheckIfSquaresCompleted(lines);

        if (completedLines >= 2)
        {
            // TODO: Play bonus animation
            GameEvent.ShowCongrulationWritings();
        }
        /*else if (scoreText.text == "50" && counter == 0)
        {
            GameEvent.ShowCongrulationWritings();
            counter++;
        }
        else if(scoreText.text == "100" && counter == 1)
        {
            GameEvent.ShowCongrulationWritings();
            counter++;
        }
        else if(scoreText.text == "150" && counter == 2)
        {
            GameEvent.ShowCongrulationWritings();
            counter++;
        }
        else if(scoreText.text == "200" && counter == 3)
        {
            GameEvent.ShowCongrulationWritings();
            counter++;
        }
        else if(scoreText.text == "250" && counter == 4)
        {
            GameEvent.ShowCongrulationWritings();
            counter++;
        }*/
        // Add Scores
        int totalScores = completedLines * 10;
        GameEvent.AddScores(totalScores);
    }

    private int CheckIfSquaresCompleted(List<int[]> data)
    {
        List<int[]> completedLines = new List<int[]>();
        int linesCompleted = 0;

        foreach (int[] line in data)
        {
            bool lineCompleted = true;

            foreach (int squareIndex in line)
            {
                GridSquare gridSquareComponent = _gridSquares[squareIndex].GetComponent<GridSquare>();

                if (!gridSquareComponent.SquareOccupied)
                {
                    lineCompleted = false;
                    break;
                }
            }

            if (lineCompleted)
            {
                completedLines.Add(line);
            }
        }

        foreach (int[] line in completedLines)
        {
            foreach (int squareIndex in line)
            {
                GridSquare gridSquareComponent = _gridSquares[squareIndex].GetComponent<GridSquare>();
                gridSquareComponent.DeactivateSquare();
            }

            foreach (int squareIndex in line)
            {
                GridSquare gridSquareComponent = _gridSquares[squareIndex].GetComponent<GridSquare>();
                gridSquareComponent.ClearOccupied();
            }

            linesCompleted++;
        }

        return linesCompleted;
    }

    private void CheckIfPlayerLost()
    {
        int validShapesCount = 0;

        foreach (var shape in shapeStorage.shapeList)
        {
            var isShapeActive = shape.IsAnyOfShapeSquareActive();

            // Check if the shape can be placed on the grid
            if (CheckIfShapeCanBePlacedOnGrid(shape) && isShapeActive)
            {
                shape?.ActivateShape();
                validShapesCount++;
            }
        }

        Debug.Log("Valid Shapes: " + validShapesCount); // Add this line for debugging

        if (validShapesCount == 0)
        {
            // Game Over
            StartCoroutine(DelayedSceneLoad("RandomQuestions", 3f));
        }
    }



    // Coroutine for delaying scene load
    private IEnumerator<object> DelayedSceneLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }




   /*private bool CheckIfShapeCanBePlacedOnGrid(Shape currentShape)
    {
        ShapeData currentShapeData = currentShape.CurrentShapeData;
        int shapeColumns = currentShapeData.columns;
        int shapeRows = currentShapeData.rows;

        // All indexes of filled up squares
        List<int> originalShapeFilledUpSquares = new List<int>();
        int squareIndex = 0;

        for (int rowIndex = 0; rowIndex < shapeRows; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < shapeColumns; columnIndex++)
            {
                if (currentShapeData.board[rowIndex].column[columnIndex])
                {
                    originalShapeFilledUpSquares.Add(squareIndex);
                }
                squareIndex++;
            }
        }

        if (currentShape.TotalSquareNumber != originalShapeFilledUpSquares.Count)
        {
            Debug.Log("Number of filled up squares is not equal to the total number of squares");
        }

        List<int[]> squareList = GetAllSquaresCombination(shapeColumns, shapeRows);
        bool canBePlaced = false;

        foreach (int[] number in squareList)
        {
            bool shapeCanBePlacedOnTheBoard = true;

            foreach (int squareIndexToCheck in originalShapeFilledUpSquares)
            {
                GridSquare comp = _gridSquares[number[squareIndexToCheck]].GetComponent<GridSquare>();

                // Check if the square is occupied
                if (comp.SquareOccupied)
                {
                    //Debug.Log("Square is occupied");
                    shapeCanBePlacedOnTheBoard = false;
                    break;
                }
            }

            if (shapeCanBePlacedOnTheBoard)
            {
                Debug.Log("Shape can be placed on the board");
                canBePlaced = true;
                break;
            }
        }

        return canBePlaced;
    }*/
    private bool CheckIfShapeCanBePlacedOnGrid(Shape currentShape)
    {
        // Extract shape data
        ShapeData currentShapeData = currentShape.CurrentShapeData;
        int shapeColumns = currentShapeData.columns;
        int shapeRows = currentShapeData.rows;

        // Get filled squares in the shape
        List<int> filledSquares = GetFilledSquares(currentShapeData, shapeColumns, shapeRows);

        // Check if filled squares match total squares in shape
        if (currentShape.TotalSquareNumber != filledSquares.Count)
        {
            Debug.Log("Number of filled up squares is not equal to the total number of squares");
        }

        // Get all square combinations
        List<int[]> squareCombinations = GetAllSquaresCombination(shapeColumns, shapeRows);

        // Check if shape can be placed on the grid
        return CanShapeBePlacedOnGrid(squareCombinations, filledSquares);
    }

    private List<int> GetFilledSquares(ShapeData currentShapeData, int shapeColumns, int shapeRows)
    {
        List<int> filledSquares = new List<int>();
        int squareIndex = 0;

        for (int rowIndex = 0; rowIndex < shapeRows; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < shapeColumns; columnIndex++)
            {
                if (currentShapeData.board[rowIndex].column[columnIndex])
                {
                    filledSquares.Add(squareIndex);
                }
                squareIndex++;
            }
        }

        return filledSquares;
    }

    private bool CanShapeBePlacedOnGrid(List<int[]> squareCombinations, List<int> filledSquares)
    {
        foreach (int[] combination in squareCombinations)
        {
            if (IsCombinationValid(combination, filledSquares))
            {
                Debug.Log("Shape can be placed on the board");
                return true;
            }
        }

        return false;
    }

    private bool IsCombinationValid(int[] combination, List<int> filledSquares)
    {
        foreach (int squareIndex in filledSquares)
        {
            GridSquare comp = _gridSquares[combination[squareIndex]].GetComponent<GridSquare>();

            // Check if the square is occupied
            if (comp.SquareOccupied)
            {
                return false;
            }
        }

        return true;
    }
    private List<int[]> GetAllSquaresCombination(int columns, int rows)
    {
        List<int[]> squareList = new List<int[]>();
        int lastColumnIndex = 0;
        int lastRowIndex = 0;
        int safeIndex = 0;

        while (lastRowIndex + (rows - 1) < 9)
        {
            List<int> rowData = new List<int>();

            for (int row = lastRowIndex; row < lastRowIndex + rows; row++)
            {
                for (int column = lastColumnIndex; column < lastColumnIndex + columns; column++)
                {
                    rowData.Add(_lineInducator.line_data[row, column]);
                }
            }

            squareList.Add(rowData.ToArray());

            lastColumnIndex++;

            if (lastColumnIndex + (columns - 1) >= 9)
            {
                lastColumnIndex = 0;
                lastRowIndex++;
            }

            safeIndex++;

            if (safeIndex > 100)
            {
                break;
            }
        }
        return squareList;
    }
}
