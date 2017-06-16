using UnityEngine;
using System.Collections;
public class Obstacle : MonoBehaviour {

	public SpacePod game;
	public static float speed = .1f;
	public Vector3 velocity = new Vector3(-1, 0, 0);
	Vector3 MovementVector = new Vector3();
	// Use this for initialization
	void Start () {
		game = FindObjectOfType (typeof(SpacePod)) as SpacePod;
		float randomY = Random.Range (-5f, 5f);
		transform.position = new Vector3 (15, randomY, 0);
		GameObject MyObject = Instantiate (game.ObstacleObject) as GameObject;
		MyObject.transform.position = transform.position;
		transform.parent = MyObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (game.score > 250) {
			rotate ();
		}
	}

	void Move(){
		transform.Translate (MovementVector);
	}

	void rotate(){
		transform.Rotate(new Vector3(0f, 0f, 1f));
			}

	public void SetSpeed(float a){
		speed = a;
	}
}
