using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class displayCurrentPlayer : MonoBehaviour {
    //variables
    Text textComp;

	// Use this for initialization
	void Awake () {
        textComp = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        int cp = GameMapping.map.turn + 1;
        textComp.text = "CurrentPlayer: Player " + cp;
    }
}
