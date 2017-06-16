using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PowerBar : MonoBehaviour {

	private float OGX = 0;
	public Lead lead;
	// Use this for initialization
	void Start () {
		
		int width = Screen.width;
		int height = Screen.height;
		//transform.localPosition = (new Vector3 (-1*width/9f, 0, 0f));
		OGX = transform.localPosition.x;
		GameObject LeadObj = GameObject.Find ("Lead");
		lead = LeadObj.GetComponent<Lead> ();
	}
	
	// Update is called once per frame
	void Update () {
		Image mine = this.gameObject.GetComponent<Image> ();
		if(mine!=null){
		float diff = ((200f - lead.power))/2f;
		transform.localPosition = (new Vector3(OGX - diff, transform.localPosition.y, 0));
		mine.rectTransform.sizeDelta = new Vector2(lead.power, mine.rectTransform.rect.height);
		}
	}

	//public void Decrease(){
		
	//	float width = mine.rectTransform.rect.width;
	//	mine.rectTransform.sizeDelta = new Vector2 (width - .1f, mine.rectTransform.rect.height);
	//	mine.rectTransform.Translate (-.03f, 0f, 0f);
	//}
}
