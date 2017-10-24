using UnityEngine;
using System.Collections;
using System;
using System.Collections;
using UnityEngine.UI;

public class InvokeScript : MonoBehaviour 
{
	public GameObject orc;

	public GameObject tree;


	public Text serverText;


	void Start()
	{
		Debug.Log ("In start!");
		Invoke ("SpawnObject", 2);

		StartCoroutine (LoadWWW ());

	}

	void SpawnObject()
	{
		Debug.Log ("Spawning Object");
//		Instantiate(target, new Vector3(0, 5, 0), Quaternion.identity);
		CancelInvoke ("SpawnObject");
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

		for (int i = 0, xCoord = -6; i < arr.Length - 1; i++, xCoord += 6) {
			for (int j = 0, zCoord = -6; j < 3; j++, zCoord += 6) {
				if (final [i, j].Equals("Orc")) {
					Instantiate(orc, new Vector3(xCoord, 0, zCoord), Quaternion.identity);
				}
				if (final [i, j].Equals("Tree")) {
					Instantiate(tree, new Vector3(xCoord, 0, zCoord), Quaternion.identity);
				}
				Debug.Log (final [i, j]);
			}

		}
		for (int i = 0; i < single.Length; i++) {
			Debug.Log (single [i]);
		}
	}
}
