using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTest : MonoBehaviour {
	public GameObject PreFabObject;
	GameObject PreFabClone;

	// Use this for initialization
	void Start () {
		PreFabClone = Instantiate (PreFabObject, transform.position, Quaternion.identity) as GameObject;
	}

	// Update is called once per frame
	void Update () {

	}
}
