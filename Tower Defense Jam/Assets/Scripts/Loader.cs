using UnityEngine;
using System.Collections.Generic;

public class Loader : MonoBehaviour {
	List<GameObject> preload;

	// Use this for initialization
	void Awake () {
		GameObject shared = new GameObject();
		shared.name = "_Shared";

		foreach (GameObject go in preload) {
//			GameObject g = (GameObject)Instantiate(preload);
//			g.
		}
	}
}
