using UnityEngine;
using System.Collections;

public class ObstacleObject : MonoBehaviour {

	public static float speed = .05f;
	public SpacePod game;
	public Vector3 velocity = new Vector3(-1, 0, 0);
	Vector3 MovementVector = new Vector3();
	private bool weaving = false;
	private bool up = false;
	private float WeaveRand;
	// Use this for initialization
	void Start () {
		game = FindObjectOfType (typeof(SpacePod)) as SpacePod;
		transform.SetParent (game.transform);
		WeaveRand =  UnityEngine.Random.Range (.5f, 1.5f);

	}
	
	// Update is called once per frame
	void Update () {
		MovementVector = (speed * velocity);
		Move ();
		if (transform.position.x < -12) {
			Destroy (this.gameObject);
		}
			
		if (game.score > 500 && weaving==false) {
			weaving = true;
			InvokeRepeating ("weave", WeaveRand, WeaveRand);
		}
			
	}

	void Move(){
		transform.Translate (MovementVector);
	}

	public void SetSpeed(float a){
		speed = a;
	}

	public void weave(){
		if(up == false){
			this.velocity.y = -.5f; 
		}
		else{
			this.velocity.y = +.5f;
		}
		up = !up;
	}
		
}
