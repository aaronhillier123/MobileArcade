using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {

	public SpriteRenderer rend;
	public Sprite Cat;
	public Sprite Dog;
	public Sprite Pig;
	public Sprite Bear;
	private int animal;
	public AnimalSlingshot game;
	private GameObject Current;

	// Use this for initialization
	void Start () {
		game = FindObjectOfType (typeof(AnimalSlingshot)) as AnimalSlingshot;
	}
	
	// Update is called once per frame
	void Update () {
		if(game.GetLoaded()!=false){
			GameObject loaded = game.GetLoaded ();
			float x = loaded.transform.position.x;
			float y = loaded.transform.position.y;
			float z = loaded.transform.position.z + 1f;
			transform.position = new Vector3 (x, y, z);;
	//		rend.sprite = loaded.
		//float y = right.transform.localPosition.y;
		//transform.position = new Vector2 (mid, y);
		}
	}

	/*public void SetSprite(int animal){
		if(animal==0){
			rend.sprite=Cat;
		}else if(animal==1){
			rend.sprite=Dog;
		}else if(animal==2){
			rend.sprite=Pig;
		}else if(animal==3){
			rend.sprite=Bear;
		}
	}
	*/

	void OnMouseDown(){

	}

}
