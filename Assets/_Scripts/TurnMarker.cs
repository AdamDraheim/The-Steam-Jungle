using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnMarker : MonoBehaviour {


	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<Text>().text = "Turn " + GameMapping.map.turn;
	}
}
