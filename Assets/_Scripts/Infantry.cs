using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infantry : Unit {

	// Use this for initialization
	void Start () {

        SetColor();

	}
	
	// Update is called once per frame
	void Update () {
		
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
