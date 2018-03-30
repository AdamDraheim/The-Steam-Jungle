using UnityEngine;

public class GameMapping : MonoBehaviour
{

    public int sizeX;
    public int sizeY;
    public bool[,] occupied;
    public int[,] teamControlled; //0 is no team, 1 is team 1, 2 is team 2
    public Unit[,] unitMap;
    public static GameMapping map;
    public int turn = 1;
    public int numPlayers = 0;

    public GameObject infantry;

    public GameObject square;

    public Player EnemyAI;
    public Player User;

    public Player[] Players;


    // Use this for initialization
    void Start()
    {

        if (map == null)
        {
            map = this;


        }
        else if (map != this)
        {
            Destroy(this.gameObject);
        }

        Players = new Player[numPlayers];

        occupied = new bool[sizeX, sizeY];
        teamControlled = new int[sizeX, sizeY];
        unitMap = new Unit[sizeX, sizeY];
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


    }

    // Update is called once per frame
    void Update()
    {

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
        occupied[(int)unit.transform.position.x, (int)unit.transform.position.y] = true;
        unitMap[(int)unit.transform.position.x, (int)unit.transform.position.y] = unit;

    }

}
