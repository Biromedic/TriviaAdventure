using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This attribute allows you to create instances of this class through the Unity Editor.
[CreateAssetMenu]
[System.Serializable]
public class ShapeData : ScriptableObject
{
    // Inner class representing a row of boolean values
    [System.Serializable]
    public class Row
    {
        public bool[] column; // Boolean array representing a row
        private int _size = 0; // Size of the row

        // Constructor
        public Row() { }

        // Overloaded constructor that creates a row with a specified size
        public Row(int size)
        {
            CreateRow(size);
        }

        // Method to create a row with a specified size
        public void CreateRow(int size)
        {
            _size = size;
            column = new bool[_size];
            ClearRow();
        }

        // Method to clear the row by setting all values to false
        public void ClearRow()
        {
            for (int i = 0; i < _size; i++)
            {
                column[i] = false;
            }
        }
    }

    public int columns = 0; // Number of columns in the board
    public int rows = 0; // Number of rows in the board
    public Row[] board; // 2D array representing the board

    // Method to clear the entire board by calling ClearRow for each row
    public void Clear()
    {
        for (var i = 0; i < rows; i++)
        {
            board[i].ClearRow();
        }
    }

    // Method to create a new board with the specified number of rows and columns
    public void CreateNewBoard()
    {
        board = new Row[rows];

        for (var i = 0; i < rows; i++)
        {
            board[i] = new Row(columns);
        }
    }
}
