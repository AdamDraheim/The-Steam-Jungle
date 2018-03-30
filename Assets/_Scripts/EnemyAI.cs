using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Player {

    private int numUnits = 0;
    private int team;
    public int money;

    // Use this for initialization
    void Start() {
        spec = 1;
    }

    // Update is called once per frame
    void Update() {

    }

    override
    public void performAction()
    {
        MoveUnits();
    }

    public void MoveUnits()
    {

        for (int i = 0; i < numUnits; i++)
        {

            Unit unit = unitList[i];

            int[,] squareValues = GetSquareValues(unit);

            PerformAction(unit, squareValues);

        }


        //GameMapping.map.NextTurn();

    }

    int[,] GetSquareValues(Unit unit)
    {

        int unitPosX = (int)unit.transform.position.x;
        int unitPosY = (int)unit.transform.position.y;

        int[,] squareValues = new int[7, 7];

        for (int i = 0; i <= 6; i++)
        {
            for (int j = 0; j <= 6; j++)
            {

                int adjustX = unitPosX + (i - 3);
                int adjustY = unitPosY + (j - 3);

                if (Mathf.Abs(adjustX) + Mathf.Abs(adjustY) <= 3 && !GameMapping.map.occupied[adjustX, adjustY])
                {
                    int points = 0;

                    if (unit.GetComponent<Infantry>() != null)
                    {

                        points = InfantryPlanning(adjustX, adjustY, unitPosX, unitPosY);

                    }
                    squareValues[i, j] = points;

                }
                else
                {
                    squareValues[i, j] = -1000;
                }
            }
        }

        return squareValues;

    }

    public int getEnemyLocationX()
    {

        return 3;

    }

    public int getEnemyLocationY()
    {

        return 3;

    }

    public int InfantryPlanning(int adjustX, int adjustY, int unitPosX, int unitPosY)
    {

        int points = 0;

        if (Positioning.AllyNearby(adjustX, adjustY, 3, team))
        {
            points += 5;
        }

        points += Positioning.SquaresAdvanced(adjustX, adjustY, unitPosX, unitPosY, 0, 0);

        return points;

    }

    public void PerformAction(Unit unit, int[,] squareValues)
    {

        int unitPosX = (int)unit.transform.position.x;
        int unitPosY = (int)unit.transform.position.y;

        int[] bestMoves = new int[3] { -1000, -1000, -1000};
        for(int i = 0; i < squareValues.LongLength; i++)
        {
            int value = squareValues[i / squareValues.Length, i % squareValues.Length];
            for (int j = 0; j < bestMoves.Length; j++)
            {
                if (bestMoves[j] < value)
                {
                    bestMoves[j] = value;
                    j = 3;
                } 
                 
            }

        }

        int valueToUse = (int)(Random.Range(0, bestMoves.Length));
        if(bestMoves[valueToUse] < -50)
        {
            valueToUse--;
            if (bestMoves[valueToUse] < -50)
            {
                valueToUse = bestMoves[0];
            }
        }

        unit.MoveTo((valueToUse / squareValues.Length) + unitPosX, (valueToUse % squareValues.Length) + unitPosY);
    }
   

}