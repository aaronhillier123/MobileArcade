using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	//public AudioClip pop;
	public float speed = .1f;
	public Vector3 velocity = new Vector3(0,-1,0);
	Vector3 MovementVector;
	SpacePod game;
	private SpriteRenderer spriterender;
	public Sprite Pod1;
	public Sprite Pod2;
	public Sprite Pod3;
	public Sprite Pod4;
	public Sprite Pod5;
	public Sprite Pod6;
//	int a = Screen.width;
//	int b = Screen.height;
	private bool crashing = false;

	//Game gamescript = game.GetComponent<Game>();
	// Use this for initialization
	void Start () {
		
		game = GameObject.FindObjectOfType(typeof(SpacePod)) as SpacePod;
		spriterender = GetComponent<SpriteRenderer>();

		int togglesound = PlayerPrefs.GetInt ("SoundToggle");
		if (togglesound == 0) {
			GetComponent<AudioSource> ().mute = true;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Move ();

	}

	void Move(){
		MovementVector = velocity * speed;
		transform.Translate (MovementVector);
		if (transform.position.x > 8.6 || transform.position.x < -8.6) {
			if (crashing == false) {
				crashing = true;
				crash ();
			}
		}
		if (transform.position.y > 4.7 || transform.position.y < -4.7) {
			crashing = true;
				crash ();
			} 
		}

	void OnCollisionEnter2D(Collision2D collider){
		Paddle paddle = collider.gameObject.GetComponent<Paddle> ();
		Obstacle obs = collider.gameObject.GetComponent<Obstacle> ();
		if (paddle != null) {
			Vector3 normal = collider.contacts [0].normal;
			velocity = Vector3.Reflect (velocity, normal);
		}
		if (obs != null) {
			crash ();
		} 
	}

	//find absolute value of a float
	float absolute(float a){
		if (a<0){
			a = a * -1;
		}
		return a;
	}

	public void crash(Obstacle ob){
		//Debug.Log ("collided with obstacle");
		crashing=true;
		velocity =  new Vector3(0,-1,0);
		game.CancelInvoke ();
		//GetComponent<SpriteRenderer>().sprite = Egg1;
		StartCoroutine("SpriteChange");
	}
		
	//when the ball collides with an obstacle
	public void crash(){
		AudioSource Player = GetComponent<AudioSource> ();
		Player.Play ();
		velocity =  new Vector3(0,-1,0);
		Destroy (GetComponent<Rigidbody2D>());
		Destroy (GetComponent<CircleCollider2D>());
		game.CancelInvoke ();
		//GetComponent<SpriteRenderer>().sprite = Egg1;
		StartCoroutine("SpriteChange");
	}

	//homemade animation
	IEnumerator SpriteChange(){
		spriterender.sprite = Pod1;
		yield return new WaitForSeconds (.05f);
		spriterender.sprite = Pod2;
		yield return new WaitForSeconds (.05f);
		spriterender.sprite = Pod3;
		yield return new WaitForSeconds (.05f);
		spriterender.sprite = Pod4;
		yield return new WaitForSeconds (.05f);
		spriterender.sprite = Pod5;
		yield return new WaitForSeconds (.05f);
		spriterender.sprite = Pod6;
		yield return new WaitForSeconds (.25f);
		Destroy (this.gameObject);
		game.end ();
	}
		
	//make sure velocity isnt too fast
	public void AdjustVelocity(){
		float divfact = 1f;
		float a = velocity.x;
		float b = velocity.y;
		float fp = (a * a) + (b * b);
		if (fp < 0f) {
			fp = fp * -1f;
		}
		float c = Mathf.Sqrt (fp);
		divfact = 1f / c;
		velocity.y = b * divfact;
		velocity.x = a * divfact;
	}
}

