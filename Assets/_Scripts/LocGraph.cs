using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocGraph {
    private LocNode[,] nodes;
    private bool[,] elementsExists;
    private int rows;
    private int columns;
    private int mapRow;
    private int mapCol;
    private int centerIdx;
    private float drawX;
    private float drawY;

    public LocGraph(int rng, int row, int col)
    {
        rows = columns = rng * 2 + 1;
        nodes = new LocNode[rows, columns];
        elementsExists = new bool[rows, columns];
        mapRow = row;
        mapCol = col;
        centerIdx = rng;
    }

    public void addNode(int row, int column, float x, float y, int val)
    {
        LocNode node = new LocNode();
        node.row = row;
        node.column = column;
        node.x = x;
        node.y = y;
        node.val = val;
        int placedRow = (row - mapRow) + centerIdx;
        int placedColumn = (column - mapCol) + centerIdx;
        nodes[placedRow, placedColumn] = node;
        elementsExists[placedRow, placedColumn] = true;
    }

    public void setDrawX(float x)
    {
        drawX = x;
    }

    public void setDrawY(float y)
    {
        drawY = y;
    }

    public bool nodeExists(int row, int column)
    {
        int currentRow = (row - mapRow) + centerIdx;
        int currentCol = (column - mapCol) + centerIdx;
        if (currentRow < rows && currentRow >= 0 && currentCol < columns && currentCol >= 0)
        {
            return elementsExists[currentRow, currentCol];
        }
        return false;
    }

    public LocNode getNode(int row, int column)
    {
        int currentRow = (row - mapRow) + centerIdx;
        int currentCol = (column - mapCol) + centerIdx;
        return nodes[currentRow, currentCol];
    }

    public int getHeight()
    {
        return rows;
    }

    public int getWidth()
    {
        return columns;
    }

    public LocNode BFS(int startRow, int startCol, int row, int column)
    {
        Queue<LocNode> queue = new Queue<LocNode>();
        queue.Enqueue(getNode(startRow, startCol));
        while(queue.Count != 0)
        {
            LocNode node = queue.Dequeue();
            if (node.row == row && node.column == column)
            {
                return node;
            }

            for (int i = -1; i <= 1; i += 2)
            {
                enqueueNodeBFS(queue, node, i, 0);
                enqueueNodeBFS(queue, node, 0, i);
            }
        }
        return null;
    }

    private void enqueueNodeBFS(Queue<LocNode> queue, LocNode node, int rowRel, int columnRel)
    {
        if (nodeExists(node.row + rowRel, node.column + columnRel) && !getNode(node.row + rowRel, node.column + columnRel).visited)
        {
            getNode(node.row + rowRel, node.column + columnRel).visited = true;
            getNode(node.row + rowRel, node.column + columnRel).cameFrom = node;
            queue.Enqueue(getNode(node.row + rowRel, node.column + columnRel));
        }
    }

    public string toString()
    {
        string str = "";
        for (int i = 0; i < nodes.GetLength(0); i++)
        {
            for (int j = 0; j < nodes.GetLength(1); j++)
            {
                if (elementsExists[i, j] == true)
                {
                    str += "[" + nodes[i, j].x + ", " + nodes[i, j].y + "]";
                }
                else
                {
                    str += "[-, -]";
                }
            }
            str += "\n";
        }
        return str;
    }
}
