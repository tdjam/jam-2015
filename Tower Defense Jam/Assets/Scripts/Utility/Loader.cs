using UnityEngine;
using System.Collections.Generic;

public class Loader : MonoBehaviour {
	[SerializeField] List<GameObject> preload;

	void Awake () {
		GameObject shared = new GameObject();
		shared.name = "_Shared";

		foreach (GameObject go in preload) {
			GameObject clone = Instantiate(go) as GameObject;
			clone.transform.SetParent(shared.transform);
		}
	}
}
