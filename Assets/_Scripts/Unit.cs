using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Unit : MonoBehaviour {
    //variables
    public int team = 0;
    public bool hasMoved = false;
    ///<summary>The name of the unit</summary>
    public string unitName;
    ///<summary>The hit points of the unit</summary>
    public int hp;
    ///<summary>The attack power of the Unit</summary>
    public int atk;
    ///<summary>The Speed of the unit</summary>
    public int spd;
    ///<summary>The movement of the unit</summary>
    public int mvt;
    ///<summary>The minimum attack/heal range of the  of the unit</summary>
    public int minRng;
    ///<summary>The maximum attack/heal range of the unit</summary>
    public int maxRng;
    ///<summary>The row the unit is on</summary>
    public int row;
    ///<summary>The column the unit is on</summary>
    public int column;
    ///<summary>States whether or not the unit is active</summary>
    public bool active;
    ///<summary>States whether or not the unit can attack</summary>
    public bool isAttacker;
    ///<summary>States whether or not the unit can heal</summary>
    public bool isHealer;
    ///<summary>States whether or not the unit can generate commerce</summary>
    public bool isCommerce;
    ///<summary>States whether or not the unit can spawn units</summary>
    public bool isSpawner;
    ///<summary>States whether or not the unit can rally</summary>
    public bool canRally;
    ///<summary>States whether or not the unit is the captain</summary>
    public bool isCaptain;
    ///<summary>The graph representing the unit's walking distance</summary>
    private LocGraph walkGraph;
    ///<summary>The graph representing the unit's action distance</summary>
    private LocGraph actionGraph;

    public void SetTeam(int set)
    {
        team = set;
    }

    public bool canAttack(Unit other)
    {
        return (this.isAttacker && this.team != other.team);
    }

    public bool canHeal(Unit other)
    {
        return (this.isHealer && this.team == other.team);
    }

    public bool isInWalkingRange(int _row, int _col)
    {
        return (walkGraph.nodeExists(_row, _col));
    }

    public bool isInActionRange(int _row, int _col)
    {
        return (actionGraph.nodeExists(_row, _col));
    }

    public Stack<LocNode> getPath(int _row, int _col)
    {
        LocNode node = walkGraph.BFS(row, column, _row, _col);
        Stack<LocNode> stack = new Stack<LocNode>();
        while (node.row != row || node.column != column)
        {
            stack.Push(node);
            node = node.cameFrom;
        }
        stack.Push(node);
        return stack;
    }

    public void SetColor(int _team)
    {
        SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
        Color color = new Color(0, 0, 0);
        switch (_team)
        {
            case 0:
                color = new Color(255, 0, 0);
                break;
            case 1:
                color = new Color(0, 0, 255);
                break;
            case 2:
                color = new Color(0, 255, 0);
                break;
        }

        sr.color = color;
    }

    public void moveToPoint(float nextX, float nextY)
    {
        if (transform.position.x < nextX - 0.005f || transform.position.x > nextX + 0.005f)
        {
            transform.position += new Vector3(0.05f * Math.Sign(nextX - transform.position.x), 0, 0);
        }
        else if (transform.position.y < nextY - 0.005f || transform.position.y > nextY + 0.005f)
        {
            transform.position += new Vector3(0, 0.05f * Math.Sign(nextY - transform.position.y), 0);
        }

        if (transform.position.x > nextX - 0.005f && transform.position.x < nextX + 0.005f && transform.position.x != nextX)
        {
            transform.position = new Vector3(nextX, transform.position.y, transform.position.z);
        }
        else if (transform.position.y > nextY - 0.005f && transform.position.y < nextY + 0.005f && transform.position.y != nextY)
        {

            transform.position = new Vector3(transform.position.x, nextY, transform.position.z);
        }
    }

    public bool moveUnit(int _row, int _col)
    {
        if (isInWalkingRange(_row, _col))
        {
            transform.position = new Vector3(walkGraph.getNode(_row, _col).x, walkGraph.getNode(_row, _col).y, transform.position.z);
            setActionRangeGrid(minRng, maxRng, _row, _col, 5);
            return true;
        }
        return false;
    }

    public void placeUnit(int _row, int _col)
    {
        GameMapping.map.occupied[row, column] = false;
        GameMapping.map.unitMap[row, column] = null;
        row = _row;
        column = _col;
        Debug.Log("Row: " + row + " Column: " + column);
        GameMapping.map.occupied[row, column] = true;
        GameMapping.map.unitMap[row, column] = this;
        setWalkRangeGrid();
        active = false;
    }

    public void MoveTo(int x, int y)
    {

        if (!hasMoved)
        {
            GameMapping.map.unitMap[(int)transform.position.x, (int)transform.position.y] = null;

            transform.position = new Vector3(x, y, -5);

            GameMapping.map.unitMap[(int)transform.position.x, (int)transform.position.y] = this;

            GameMapping.map.Players[GameMapping.map.turn].SelectedUnit(null);

            this.hasMoved = true;
        }
    }

    public void SetMoved(bool moved)
    {
        hasMoved = moved;
    }

    public LocGraph getWalkGrid()
    {
        return walkGraph;
    }

    public LocGraph getActionGrid()
    {
        return actionGraph;
    }

    public void setActionRangeGrid(int minR, int maxR, int _row, int _column, int val)
    {
        actionGraph = new LocGraph(maxR, _row, _column);
        actionGraph.setDrawX(transform.position.x);
        actionGraph.setDrawY(transform.position.y);
        while (minR <= maxR)
        {
            for (int i = -1; i <= 1; i += 2)
            {
                if (_row + (minR * i) < GameMapping.map.occupied.GetLength(0) && _row + (minR * i) >= 0
                && ((minR * i) + maxR) < actionGraph.getHeight() && ((minR * i) + maxR) >= 0 )
                {
                    actionGraph.addNode(_row + (minR * i), _column, (minR * i), 0, val);
                }
                if (_column + (minR * i) < GameMapping.map.occupied.GetLength(1) && _column + (minR * i) >= 0
                && ((minR * i) + maxR) < actionGraph.getWidth() && ((minR * i) + maxR) >= 0)
                {
                    actionGraph.addNode(_row, _column + (minR * i), 0, (minR * i), val);
                }
            }
            for (int j = 0; j < minR - 1; j++)
            {
                if ((_row - minR) + j < GameMapping.map.occupied.GetLength(0) && (_row - minR) + j >= 0
                && _column + j < GameMapping.map.occupied.GetLength(1) && _column + j >= 0)
                {
                    actionGraph.addNode((_row - minR) + j, _column + j, _column + j, ((GameMapping.map.unitMap.GetLength(0) - 1) - _row - minR) + j, val);
                }
                if (_row - j < GameMapping.map.occupied.GetLength(0) && _row - j >= 0
                && (_column - minR) + j < GameMapping.map.occupied.GetLength(1) && (_column - minR) + j >= 0)
                {
                    actionGraph.addNode(_row - j, (_column - minR) + j, (_column - minR) + j, ((GameMapping.map.unitMap.GetLength(0) - 1) - _row) - j, val);
                }
                if ((_row + minR) - j < GameMapping.map.occupied.GetLength(0) && (_row + minR) - j >= 0
                && _column - j < GameMapping.map.occupied.GetLength(1) && _column - j >= 0)
                {
                    actionGraph.addNode((_row + minR) - j, _column - j, _column - j, ((GameMapping.map.unitMap.GetLength(0) - 1) - (_row + minR)) - j, val);
                }
                if (_row + j < GameMapping.map.occupied.GetLength(0) && _row + j >= 0
                && (_column + minR) - j < GameMapping.map.occupied.GetLength(1) && (_column + minR) - j >= 0)
                {
                    actionGraph.addNode(_row + j, (_column + minR) - j, (_column + minR) - j, ((GameMapping.map.unitMap.GetLength(0) - 1) - _row) + j, val);
                }
            }
            minR++;
        }
    }

    public void setWalkRangeGrid()
    {
        walkGraph = new LocGraph(mvt, row, column);
        walkGraph.setDrawX(transform.position.x);
        walkGraph.setDrawY(transform.position.y);
        Queue<LocNode> queue = new Queue<LocNode>();
        walkGraph.addNode(row, column, column, (GameMapping.map.unitMap.GetLength(0) - 1) - row, 0);
        queue.Enqueue(walkGraph.getNode(row, column));
        int count = queue.Count;
        int idx = 0;
        while (idx < mvt)
        {
            while (count > 0)
            {
                LocNode node = queue.Dequeue();
                count--;
                if (node.row - 1 >= 0
                && !GameMapping.map.occupied[node.row - 1, node.column]
                && !walkGraph.nodeExists(node.row - 1, node.column))
                {
                    walkGraph.addNode(node.row - 1, node.column, node.x, node.y + 1, idx);
                    queue.Enqueue(walkGraph.getNode(node.row - 1, node.column));
                }
                if (node.column + 1 < GameMapping.map.occupied.GetLength(1)
                && !GameMapping.map.occupied[node.row, node.column + 1]
                && !walkGraph.nodeExists(node.row, node.column + 1))
                {
                    walkGraph.addNode(node.row, node.column + 1, node.x + 1, node.y, idx);
                    queue.Enqueue(walkGraph.getNode(node.row, node.column + 1));
                }
                if (node.row + 1 < GameMapping.map.occupied.GetLength(0)
                && !GameMapping.map.occupied[node.row + 1, node.column]
                && !walkGraph.nodeExists(node.row + 1, node.column))
                {
                    walkGraph.addNode(node.row + 1, node.column, node.x, node.y - 1, idx);
                    queue.Enqueue(walkGraph.getNode(node.row + 1, node.column));
                }
                if (node.column - 1 >= 0
                && !GameMapping.map.occupied[node.row, node.column - 1]
                && !walkGraph.nodeExists(node.row, node.column - 1))
                {
                    walkGraph.addNode(node.row, node.column - 1, node.x - 1, node.y, idx);
                    queue.Enqueue(walkGraph.getNode(node.row, node.column - 1));
                }
            }
            count = queue.Count;
            idx++;
        }
    }
}
