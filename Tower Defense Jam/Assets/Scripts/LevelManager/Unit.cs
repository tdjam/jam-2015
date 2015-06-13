using UnityEngine;
using System.Collections;

namespace Level {
	[System.Serializable]
	public class Unit {
		[Tooltip("What unit type to spawn")]
		public UnitType type;

		[Tooltip("Where this unit will seek")]
		public Pathway destination;

		[Tooltip("After spawning this item wait this long before creating the next")]
		[Range(0f, 10f)] public float spawnDelay = 1f;
	}
}
