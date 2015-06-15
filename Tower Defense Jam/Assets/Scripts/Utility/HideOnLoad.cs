using UnityEngine;
using System.Collections;

public class HideOnLoad : MonoBehaviour {
	void Awake () {
		GetComponent<MeshRenderer>().enabled = false;
	}
}
