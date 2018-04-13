using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareInteract : MonoBehaviour {

    // Use this for initialization
    SpriteRenderer sr;
    private Color start;

	void Start () {
        sr = gameObject.GetComponent<SpriteRenderer>();
        start = sr.color;
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

                if (GameMapping.map.Players[turn].GetSelectedUnit() != null && !GameMapping.map.Players[turn].GetSelectedUnit().active)
                {
                    GameMapping.map.Players[turn].SelectedUnit(null);
                }
                GameMapping.map.ChangeSelected(this);
            }
            else
            {
                if (GameMapping.map.Players[turn].GetSelectedUnit().isInWalkingRange((GameMapping.map.unitMap.GetLength(0) - 1) - (int)transform.position.y, (int)transform.position.x))
                {
                    GameMapping.map.goalNode = GameMapping.map.Players[turn].GetSelectedUnit().getWalkGrid().getNode((GameMapping.map.unitMap.GetLength(0) - 1) - (int)transform.position.y, (int)transform.position.x);
                    GameMapping.map.gs = GameMapping.gameState.PLACING;
                    GameMapping.map.ChangeSelected(null);
                }

            }
        }
    }

    public void SetColor(Color color)
    {
        sr.color = color;
    }

    public void ResetColor()
    {
        sr.color = start;
    }
}
