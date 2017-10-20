using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;

	public float velocity;

	public Text countText;

	public Text winText;

	public Text serverText;


	private Rigidbody rb;

	private int count;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		count = 0;
		setCountText ();
		winText.text = "";
		StartCoroutine (LoadWWW ());
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("PickUp")) {
			other.gameObject.SetActive (false);
			count = count + 1;
			setCountText ();
		}
	}

	void setCountText(){
		countText.text = "Count: " + count.ToString ();
		if (count >= 5) {
			winText.text = "You win!!";
		}


	}

	IEnumerator LoadWWW () {
		string url = "https://powerful-badlands-35153.herokuapp.com/maps/1/jsondata";
		WWW www = new WWW (url);
		Debug.Log ("Getting from " + url);
		yield return www;
		string urlJSON = www.text;
		serverText.text = urlJSON;
		Debug.Log (urlJSON);
		int end = urlJSON.Length;
		string parse = urlJSON.Substring (1, end - 3);
		parse = parse + ",";
		string[] arr = parse.Split(new string[] { "]," }, StringSplitOptions.None);
		string[,] final = new string[3, 3];
		string[] single = new string[9];
		int count = 0;
		for (int i = 0; i < arr.Length - 1; i++) {
			arr[i] = arr[i].Substring (1);
			string[] temp = arr [i].Split (',');
			for (int j = 0; j < 3; j++) {
				if (temp [j].IndexOf ("\"") != -1) {
					temp [j] = temp [j].Substring (1, temp [j].Length - 2);
				}
				final [i, j] = temp [j];
				single [count] = temp [j];
				count++;
			}
		}

		for (int i = 0; i < arr.Length - 1; i++) {
			for (int j = 0; j < 3; j++) {
				Debug.Log (final [i, j]);
			}

		}
		for (int i = 0; i < single.Length; i++) {
			Debug.Log (single [i]);
		}
	}
}

public class MyClass
{
	public Array level;
}