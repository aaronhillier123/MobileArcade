using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Train : MonoBehaviour {

	public GameObject Money;
	public GameObject Coal;
	public GameObject EndPanel;
	public bool ended = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EndGame (){
		if(ended==false){
			
			GameObject leadO = GameObject.Find ("Lead");
			Lead lead = leadO.GetComponent<Lead> ();
			lead.power = 0;
			int score = lead.score;
			if(score > PlayerPrefs.GetInt("TrainHighScore")){
				PlayerPrefs.SetInt("TrainHighScore", score);
			}
			Canvas can = FindObjectOfType (typeof(Canvas)) as Canvas;
			ended = true;
			lead.Speed = 0f;
			GameObject endpan = Instantiate (EndPanel) as GameObject;
			endpan.transform.SetParent (can.transform, false);
			//endpan.transform.position = new Vector3(0, 250, 0);
			//Destroy (this);
		}
	}

	public void CreateMoney(){
		float Randx = Random.Range (-6f, 18f);
		float Randy = Random.Range (-3.5f, 9f);
		GameObject money = Instantiate (Money) as GameObject;
		money.transform.position = new Vector3 (Randx, Randy, 0);
	}

	public void CreateCoal(){
		float Randx = Random.Range (-6f, 18f);
		float Randy = Random.Range (-3.5f, 9f);
		GameObject coal = Instantiate (Coal) as GameObject;
		coal.transform.position = new Vector3 (Randx, Randy, 0);
	}

}
