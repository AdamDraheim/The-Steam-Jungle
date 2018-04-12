using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infantry : Unit {

	// Use this for initialization
	void Awake () {
        unitName = NameGenerator.generate();
        hp = Random.Range(8, 13);
        atk = Random.Range(5, 11);
        spd = Random.Range(3, 8);
        mvt = 3;
        minRng = 1;
        maxRng = 2;
        active = true;
        isAttacker = true;
        isHealer = false;
        isCommerce = false;
        isSpawner = false;
        isSpawner = false;
        canRally = false;
        isCaptain = false;
        SetColor();
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
