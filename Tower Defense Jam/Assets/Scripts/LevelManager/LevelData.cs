using UnityEngine;
using System.Collections.Generic;

namespace Level {
	[System.Serializable]
	public class LevelData : MonoBehaviour {
		[Tooltip("All of these spawners will be made active when the game starts up")]
		public List<Spawner> spawners;
	}
}

