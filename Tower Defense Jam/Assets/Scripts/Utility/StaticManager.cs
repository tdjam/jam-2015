using UnityEngine;
using System.Collections.Generic;

// Singleton manager, Sm for quick and simple reference
static public class Sm {
	static public Level.LevelManager level;
	static public Cannon.CannonManager cannon;
	static public CurrencyManager currency;
	
	static void ResetAll () {
		currency = null;
		level = null;
		cannon = null;
	}
}
