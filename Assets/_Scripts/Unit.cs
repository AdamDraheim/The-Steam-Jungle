using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public int team = 0;
    public bool hasMoved = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetTeam(int set)
    {
        team = set;
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
}
