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
        int turn = GameMapping.map.turn;
        if (GameMapping.map.Players[turn].GetSelectedUnit() == null) {
            
            GameMapping.map.Players[turn].SelectedUnit(GameMapping.map.unitMap[(int)transform.position.x, (int)transform.position.y]);


            //If enemy unit sets selected back to null
            if(GameMapping.map.Players[turn].GetSelectedUnit() != null && GameMapping.map.Players[turn].GetSelectedUnit().team != turn)
            {
                GameMapping.map.Players[turn].SelectedUnit(null);
            }
        }
        else
        {

            GameMapping.map.Players[turn].GetSelectedUnit().MoveTo((int)transform.position.x, (int)transform.position.y);

        }
    }
}
