using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Level {
	[System.Serializable]
	public class Spawner {
		[SerializeField] List<Wave> waveQueue;
		[HideInInspector] public int waveCount;

		public void Begin () {
			waveCount = 0;
			Sm.level.StartCoroutine(Sm.level.RunSpawner(waveQueue, this));
		}

		public bool IsComplete () {
			return waveQueue.Count == waveCount;
		}
	}
}
