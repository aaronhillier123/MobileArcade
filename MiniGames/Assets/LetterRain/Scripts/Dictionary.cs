using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Dictionary : MonoBehaviour {

	// Use this for initialization
	protected string[] mylist = new string[172820];
	public TextAsset dictionary;


	void Start () {
		mylist = dictionary.text.Split ("\n" [0]);
	}
	// Update is called once per frame
	void Update () {
		
	}

	public bool RealWord(string a){

		string b = a.ToLower ();
		int low = 0;
		int high = 172820;
		int count = 0;
		while (count < 20) {
			int currentindex = (high + low) / 2;
			string currentword = mylist [currentindex].ToString();
			if (string.Equals (b.Trim(), currentword.Trim(), System.StringComparison.OrdinalIgnoreCase)) {
				return true;
			} else if (string.Compare (b.Trim(), currentword.Trim(), true) == 1) {
				low = currentindex - 1;
			} else if (string.Compare (b.Trim(), currentword.Trim(), true) == -1) {
				high = currentindex + 1;
			} else {
			}
			++count;
		}
		return false;
	}
}
