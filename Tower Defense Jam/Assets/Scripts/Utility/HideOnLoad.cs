﻿using UnityEngine;
using System.Collections;

public class HideOnLoad : MonoBehaviour {
	void Awake () {
		MonoBehaviour[] comps = GetComponents<MonoBehaviour>();
		
		foreach (MonoBehaviour c in comps) {
			c.enabled = false;
		}
	}
}
