using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shape : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    public List<GameObject> ShapeSquares { get; set; }
    public int ShapeRow { get; set; }
    public int ShapeColumn { get; set; }
    public GameObject squareShapeImage;
    public Vector3 shapeSelectedScale;
    public Vector2 offset = new Vector2(0f, 0f);

    [HideInInspector]
    public ShapeData CurrentShapeData;
    public int TotalSquareNumber { get; set; }

    private List<GameObject> _currentShape = new List<GameObject>();
    private Vector3 _shapeStartScale;
    private RectTransform _transform;
    private Canvas _canvas;
    private Vector3 _startPosition;
    private bool _shapeActive = true;

    private void Awake()
    {
        _shapeStartScale = GetComponent<RectTransform>().localScale;
        _transform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _startPosition = _transform.localPosition;
        _shapeActive = true;
    }

    private void OnEnable()
    {
        GameEvent.MoveShapeToStartPosition += MoveShapeToStartPosition;
        GameEvent.SetShapeInactive += SetShapeInactive;
    }

    private void OnDisable()
    {
        GameEvent.MoveShapeToStartPosition -= MoveShapeToStartPosition;
        GameEvent.SetShapeInactive -= SetShapeInactive;
    }

    public bool IsOnStartPosition()
    {
        return _transform.localPosition == _startPosition;
    }

    public bool IsAnyOfShapeSquareActive()
    {
        foreach (var square in _currentShape)
        {
            if (square.gameObject.activeSelf)
            {
                Debug.Log("Square in shape is active");
                return true;
            }
        }
        Debug.Log("No active squares in shape");
        return false;
    }

    public void DeactivateShape()
    {
        if (_shapeActive)
        {
            foreach (var square in _currentShape)
            {
                square?.GetComponent<ShapeSquare>().DeactivateShape();
            }
        }
        _shapeActive = false;
    }

    private void SetShapeInactive()
    {
        if (IsOnStartPosition() == false && IsAnyOfShapeSquareActive())
        {
            foreach (var square in _currentShape)
            {
                square.gameObject.SetActive(false);
            }
        }
    }

    public void ActivateShape()
    {
        if (!_shapeActive)
        {
            foreach (var square in _currentShape)
            {
                square?.GetComponent<ShapeSquare>().ActivateShape();
            }
        }
        _shapeActive = true;
    }

    public void RequestNewShape(ShapeData shapeData)
    {
        _transform.localPosition = _startPosition;
        CreateShape(shapeData);
    }

    public void CreateShape(ShapeData shapeData)
    {
        CurrentShapeData = shapeData;
        TotalSquareNumber = GetNumberOfSquares(shapeData);

        while (_currentShape.Count < TotalSquareNumber)
        {
            _currentShape.Add(Instantiate(squareShapeImage, transform) as GameObject);
        }

        foreach (var square in _currentShape)
        {
            square.transform.position = Vector3.zero;
            square.SetActive(false);
        }

        var squareRect = squareShapeImage.GetComponent<RectTransform>();
        var moveDistance = new Vector2(squareRect.rect.width * squareRect.localScale.x, squareRect.rect.height * squareRect.localScale.y);

        int currentIndexInList = 0;

        for (var row = 0; row < shapeData.rows; row++)
        {
            for (var column = 0; column < shapeData.columns; column++)
            {
                if (shapeData.board[row].column[column])
                {
                    _currentShape[currentIndexInList].SetActive(true);
                    _currentShape[currentIndexInList].GetComponent<RectTransform>().localPosition =
                        new Vector2(GetXPositionForShapeSquare(shapeData, column, moveDistance),
                                    GetYPositionForShapeSquare(shapeData, row, moveDistance));

                    currentIndexInList++;
                }
            }
        }
    }

    private float GetYPositionForShapeSquare(ShapeData shape, int row, Vector2 moveDistance)
    {
        float shiftOnY = 0f;

        if (shape.rows > 1)
        {
            if (shape.rows % 2 != 0)
            {
                var middleSquareIndex = (shape.rows - 1) / 2;
                var multiplier = (shape.rows - 1) / 2;

                if (row < middleSquareIndex)
                {
                    shiftOnY = moveDistance.y * 1;
                    shiftOnY *= multiplier;
                }
                else if (row > middleSquareIndex)
                {
                    shiftOnY = moveDistance.y * 1;
                    shiftOnY *= multiplier;
                }
            }
            else
            {
                var middleSquareIndex2 = (shape.rows == 2) ? 1 : (shape.rows / 2);
                var middleSquareIndex1 = (shape.rows == 2) ? 0 : shape.rows - 2;
                var multiplier = shape.rows / 2;

                if (row == middleSquareIndex1 || row == middleSquareIndex2)
                {
                    if (row == middleSquareIndex1)
                    {
                        shiftOnY = moveDistance.y / 2;
                    }
                    if (row == middleSquareIndex2)
                    {
                        shiftOnY = (moveDistance.y / 2) * -1;
                    }
                }
                if (row < middleSquareIndex1 && row < middleSquareIndex2)
                {
                    shiftOnY = moveDistance.y * 1;
                    shiftOnY *= multiplier;
                }
                else if (row > middleSquareIndex1 && row > middleSquareIndex2)
                {
                    shiftOnY = moveDistance.y * -1;
                    shiftOnY *= multiplier;
                }
            }
        }
        return shiftOnY;
    }

    private float GetXPositionForShapeSquare(ShapeData shape, int column, Vector2 moveDistance)
    {
        float shiftOnX = 0f;

        if (shape.columns > 1)
        {
            if (shape.columns % 2 != 0)
            {
                var middleSquareIndex = (shape.columns - 1) / 2;
                var multiplier = (shape.columns - 1) / 2;
                if (column < middleSquareIndex)
                {
                    shiftOnX = moveDistance.x * -1;
                    shiftOnX *= multiplier;
                }
                else if (column > middleSquareIndex)
                {
                    shiftOnX = moveDistance.x * 1;
                    shiftOnX *= multiplier;
                }
            }
            else
            {
                var middleSquareIndex2 = (shape.columns == 2) ? 1 : (shape.columns / 2);
                var middleSquareIndex1 = (shape.columns == 2) ? 0 : shape.columns - 1;
                var multiplier = shape.columns / 2;

                if (column == middleSquareIndex1 || column == middleSquareIndex2)
                {
                    if (column == middleSquareIndex2)
                    {
                        shiftOnX = moveDistance.x / 2;
                    }
                    if (column == middleSquareIndex1)
                    {
                        shiftOnX = (moveDistance.x / 2) * -1;
                    }
                }
                if (column < middleSquareIndex1 && column < middleSquareIndex2)
                {
                    shiftOnX = moveDistance.x * -1;
                    shiftOnX *= multiplier;
                }
                else if (column > middleSquareIndex1 && column > middleSquareIndex2)
                {
                    shiftOnX = moveDistance.x * 1;
                    shiftOnX *= multiplier;
                }
            }
        }
        return shiftOnX;
    }

    private int GetNumberOfSquares(ShapeData shapeData)
    {
        int number = 0;

        foreach (var rowData in shapeData.board)
        {
            foreach (var cell in rowData.column)
            {
                if (cell)
                    number++;
            }
        }
        return number;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Handle click event if needed
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Handle pointer up event if needed
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _transform.localScale = shapeSelectedScale;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = eventData.position;
        _transform.position = mousePosition + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _transform.localScale = _shapeStartScale;
        GameEvent.CheckIfShapeCanBePlaced();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Handle pointer down event if needed
    }

    private void MoveShapeToStartPosition()
    {
        _transform.transform.localPosition = _startPosition;
    }
}
