using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ButtonScale : MonoBehaviour {

	// Use this for initialization
	void Start () {
		RectTransform mine = gameObject.GetComponent<RectTransform> ();
		//gameObject.transform.position = new Vector3 (0, -75, 0);
		mine.position = new Vector3(0, -75, 0);
		mine.offsetMax = new Vector2 (0, -75);
		mine.offsetMin = new Vector2 (0, 0-75);
		mine.sizeDelta = new Vector2 (400, 60);
		//mine.rect.hei
		//mine.rect.width = 60;
		mine.localScale = new Vector2 (1, 1);
		//gameObject.transform.position = new Vector3 (17 * Screen.width / 50, Screen.height / 16);}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
