using UnityEngine;
using System.Collections.Generic;

namespace Level {
	[System.Serializable]
	public class Wave {
		[Tooltip("Where this wave will create new units")]
		public Pathway spawnPoint;

		[Tooltip("How many times should this wave repeat")]
		public int repeatTotal;
		[HideInInspector] public int repeatCount; // Internal repeat counter

		[Range(0f, 10f)] public float repeatDelay = 3f;

		public List<Unit> squad;
	}
}