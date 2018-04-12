using System.Collections.Generic;
using System;
using UnityEngine;

public class GameMapping : MonoBehaviour
{

    public int sizeX;
    public int sizeY;
    public  bool[,] occupied;
    public int[,] teamControlled; //0 is no team, 1 is team 1, 2 is team 2
    public Unit[,] unitMap;
    public static GameMapping map;
    public float tileOffset = 1f; //broken...It sets everything to 0 for some reason...
    public int turn = 1;
    public int numPlayers = 0;
    public enum gameState {MOVING, PLACING, ATTACKING};
    public gameState gs = gameState.MOVING;
    private Stack<LocNode> path;
    public LocNode goalNode;
    private LocNode currentNode;

    public GameObject infantry;

    public GameObject square;

    public Player EnemyAI;
    public Player User;

    public Player[] Players;

    public SquareInteract selectedSquare;


    // Use this for initialization
    void Start()
    {
        selectedSquare = null;
        if (map == null)
        {
            map = this;


        }
        else if (map != this)
        {
            Destroy(this.gameObject);
        }

        Players = new Player[numPlayers];

        occupied = new bool[sizeY, sizeX];
        teamControlled = new int[sizeY, sizeX];
        unitMap = new Unit[sizeY, sizeX];
        turn = 0;

        Players[0] = new HumanControl();
        Players[1] = new HumanControl();


        for (int i = 2; i < Players.Length; i++)
        {
            AddAI(i);
        }

        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Spawnpoint");

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Unit unitAdded = spawnPoints[i].GetComponent<SpawnPoint>().AddUnit();
            Players[unitAdded.team].AddUnit(unitAdded);
        }

        gs = gameState.MOVING;
        tileOffset = 1f;
    }

    // Update is called once per frame
    void Update()
    {

        if(selectedSquare != null)
        {
            selectedSquare.SetColor(new Color(200, 200, 0, 100));
        }
        switch (gs)
        {
            case gameState.MOVING: //when the game is moving units
                //does nothing
                break;
            case gameState.PLACING: //when the game is placing units
                if (path == null)
                {
                    path = map.Players[turn].GetSelectedUnit().getPath(goalNode.row, goalNode.column);
                }
                Debug.Log(map.Players[turn].GetSelectedUnit().transform.position.x != goalNode.x || map.Players[turn].GetSelectedUnit().transform.position.y != goalNode.y);
                if (map.Players[turn].GetSelectedUnit().transform.position.x != goalNode.x || map.Players[turn].GetSelectedUnit().transform.position.y != goalNode.y)
                {
                    if (currentNode == null)
                    {
                        currentNode = path.Pop();
                    }
                    map.Players[turn].GetSelectedUnit().moveToPoint(currentNode.x, currentNode.y);
                    if (map.Players[turn].GetSelectedUnit().transform.position.x == currentNode.x && map.Players[turn].GetSelectedUnit().transform.position.y == currentNode.y)
                    {
                        currentNode = null;
                    }
                }
                if (map.Players[turn].GetSelectedUnit().transform.position.x == goalNode.x && map.Players[turn].GetSelectedUnit().transform.position.y == goalNode.y)
                {
                    map.Players[turn].GetSelectedUnit().placeUnit((map.unitMap.GetLength(0) - 1) - (int)transform.position.y, (int)transform.position.x);
                    path = null;
                    currentNode = null;
                    map.gs = gameState.MOVING;
                }
                break;
            case gameState.ATTACKING: //when a unit is attacking another unit
                break;
        }

    }

    public void ChangeSelected(SquareInteract square)
    {
        if (selectedSquare != null)
        {
            selectedSquare.ResetColor();
        }
        selectedSquare = square;
    }

    public void NextTurn()
    {

        for(int i = 0; i < Players[turn].GetUnitList().Length; i++)
        {
            if (Players[turn].GetUnitList()[i] != null)
            {
                Players[turn].GetUnitList()[i].SetMoved(false);
            }
        }

        turn++;

        if (turn >= numPlayers)
        {
            turn = 0;
        }

        Player playerToMove = Players[turn];

        if (playerToMove.GetSpec() == 0)
        {
           playerToMove.performAction();
        }
        else
        {

        }

    }

    public void AddAI(int idx)
    {

        Players[idx] = new EnemyAI();
        

    }

    public void MakeBoard()
    {

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                Instantiate(square, new Vector3(i, j, 0), new Quaternion(0, 0, 0, 0));
            }
        }

    }

    public void AddUnit(int team, Unit unit)
    {
        
        Players[team].AddUnit(unit);
        occupied[unit.row, unit.column] = true;
        unitMap[unit.row, unit.column] = unit;

    }

}
