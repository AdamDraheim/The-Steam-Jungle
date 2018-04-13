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
	}
}
