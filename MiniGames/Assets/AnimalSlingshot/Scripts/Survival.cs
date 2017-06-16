using UnityEngine;
using System.Collections;

public class Survival : MonoBehaviour {


	private AnimalSlingshot game;

	void Start () {
		game = FindObjectOfType (typeof(AnimalSlingshot)) as AnimalSlingshot;


	}
	
	public void AStart(){
		game.GoodScore = 0;
		game.GoodScoreText.text = game.GoodScore.ToString ();
		game.BadScore = 60;
		InvokeRepeating ("Times", 1f, 1f);
	}

	void Update () {
		
	}

	void Times(){
			game.BadScore--;
			game.BadScoreText.text = game.BadScore.ToString ();
		if (game.BadScore <= 0) {
			game.end();
		}
	}
}
