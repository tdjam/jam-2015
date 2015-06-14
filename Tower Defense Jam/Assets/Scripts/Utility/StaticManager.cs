using UnityEngine;
using System.Collections.Generic;

// Singleton manager, Sm for quick and simple reference
static public class Sm {
	static public Level.LevelManager level;
	
	static void ResetAll () {
		level = null;
	}
}
