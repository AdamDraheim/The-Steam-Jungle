using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareInteract : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void OnMouseDown()
    {
        IsClicked();
    }

    public void IsClicked()
    {
        if (GameMapping.map.gs == GameMapping.gameState.MOVING)
        {
            int turn = GameMapping.map.turn;
            if (GameMapping.map.Players[turn].GetSelectedUnit() == null)
            {

                GameMapping.map.Players[turn].SelectedUnit(GameMapping.map.unitMap[(GameMapping.map.unitMap.GetLength(0) - 1) - (int)transform.position.y, (int)transform.position.x]);


                //If enemy unit sets selected back to null
                if (GameMapping.map.Players[turn].GetSelectedUnit() != null && GameMapping.map.Players[turn].GetSelectedUnit().team != turn)
                {
                    GameMapping.map.Players[turn].SelectedUnit(null);
                }
            }
            else
            {
                if (GameMapping.map.Players[turn].GetSelectedUnit().isInWalkingRange((GameMapping.map.unitMap.GetLength(0) - 1) - (int)transform.position.y, (int)transform.position.x))
                {
                    GameMapping.map.goalNode = GameMapping.map.Players[turn].GetSelectedUnit().getWalkGrid().getNode((GameMapping.map.unitMap.GetLength(0) - 1) - (int)transform.position.y, (int)transform.position.x);
                    GameMapping.map.gs = GameMapping.gameState.PLACING;
                }

            }
        }
    }
}
