using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jackpot : MonoBehaviour {

	public List<myLight> allLights = new List<myLight> ();
	public myLight current;
	public int currentID;
	// Use this for initialization
	void Start () {
		assignLights ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void assignLights(){
		myLight[] lights = GameObject.FindObjectsOfType (typeof(myLight)) as myLight[];
		for (int i = 0; i < 36; ++i) {
			foreach(myLight m in lights){
				if (lights [i].id == i) {
					allLights.Add (lights [i]);
					lights [i].deactivate ();
				}
			}
		}
		current = allLights [0];
		currentID = 0;
		current.activate ();
		InvokeRepeating ("nextLight", .2f, .2f);
	}

	public void nextLight(){
		current.deactivate ();
		if (currentID < 35) {
			++currentID;
		} else {
			currentID = 0;
		}
		//allLights [currentID].activate();
		current = allLights [currentID];
		current.activate ();
		Debug.Log ("current id is " + currentID);
	}
}
