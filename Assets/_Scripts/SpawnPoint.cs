using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public Unit toPlace;
    public int team;
 
	// Use this for initialization
	void Start () {
        SetColor();
	}
	
	public Unit AddUnit()
    {

        Unit addedUnit = Instantiate(toPlace, new Vector3(transform.position.x, transform.position.y, -5), new Quaternion(0,0,0,0));
        addedUnit.SetTeam(team);
        addedUnit.row = (GameMapping.map.unitMap.GetLength(0) - 1) - (int)transform.position.y;
        addedUnit.column = (int)transform.position.x;
        addedUnit.setWalkRangeGrid();
        addedUnit.setActionRangeGrid(addedUnit.minRng, addedUnit.maxRng, addedUnit.row, addedUnit.column, 5);
        GameMapping.map.AddUnit(team, addedUnit);

        Destroy(this.gameObject);

        return addedUnit;

    }

    public void SetColor()
    {
        SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
        Color color = new Color(0, 0, 0);
        switch (team)
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

}
