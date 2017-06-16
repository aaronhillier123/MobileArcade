using UnityEngine;
using System.Collections;

public class Comet : MonoBehaviour {

	public static float speed = .1f;
	public Vector3 velocity = new Vector3();
	Vector3 MovementVector = new Vector3();
	float randx;
	// Use this for initialization
	void Start () {
		float rand =  UnityEngine.Random.Range (-9f, 7f);
		if (rand >= -2) {
			randx = UnityEngine.Random.Range (-1f, 0f);
		} else {
			randx = UnityEngine.Random.Range (0f, 1f);
		}
		float randy = UnityEngine.Random.Range (-1f, 0f);
		float rands = UnityEngine.Random.Range (.2f, .3f);
		transform.position = new Vector3 (rand, 6f, 0f);
		speed = rands;
		velocity.x = randx;
		velocity.y = randy;
	}
	
	// Update is called once per frame
	void Update () {
		MovementVector = speed * velocity;
		Move ();
		if (transform.position.y < -8 || transform.position.x < -12 || transform.position.x > 12) {
			Destroy (this.gameObject);
		}
	}

	void Move(){
		transform.Translate (MovementVector);
	}

	void OnTriggerEnter2D(Collider2D coll){
		Ball pod = coll.gameObject.GetComponent<Ball> ();
		if (pod != null) {
			pod.crash ();
		}
	}
}
