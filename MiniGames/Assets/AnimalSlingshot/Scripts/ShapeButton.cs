using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShapeButton : MonoBehaviour {

	//GameObjects used for instantiation on the slingshot
	public GameObject Bullet;

	public Canvas can;

	public Sprite CatSprite;
	public Sprite DogSprite;
	public Sprite PigSprite;
	public Sprite BearSprite;

	public int animal = 0;
	public AnimalSlingshot game;
	private SpriteRenderer rend;
	private float width;
	private float height;

	public Text RemText;
	// Use this for initialization
	void Start () {
		can = FindObjectOfType (typeof(Canvas)) as Canvas;
		game = GameObject.FindObjectOfType (typeof(AnimalSlingshot)) as AnimalSlingshot;
		ShowText ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		LoadShapeSling ();
	}

	public void LoadShapeSling(){
		AnimalSlingshot game = FindObjectOfType (typeof(AnimalSlingshot)) as AnimalSlingshot;
		animal = GetComponent<ShapeButton> ().animal;
		try {
			Bullet CurrentBullet = game.GetLoaded ().GetComponent<Bullet> ();
			if (CurrentBullet != null && CurrentBullet.motion == false) {
				Destroy (game.GetLoaded ());
				game.SetCurrent (null);
			}
		} catch {
		}
		SetCurrent ();
		GameObject CurrentObj = game.GetCurrent ();
		GameObject shot = GameObject.Find ("Shot");
		if (shot != null) {

			//shot.transform.localScale = new Vector2 (1f, 1f);
			shot.transform.localPosition = new Vector3 (0f, -.3f, 0f);
			GameObject current = Instantiate (CurrentObj) as GameObject;
			current.transform.localPosition = shot.transform.position;
			current.transform.localScale = new Vector2 (.6f, .6f);
			current.GetComponent<Bullet> ().animal = animal;
			game.SetLoaded (current);
		}
	}

	void SetCurrent(){
		//Game game = FindObjectOfType (typeof(Game)) as Game;
		game.SetCurrent (game.Bullet);
		game.CShape = animal;
	}

	public void ChangeText(string a){
		RemText.text = a;
	}

	public void ShowText(){
	}
		
}
