using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Level {
	[System.Serializable]
	public class Spawner {
		[SerializeField] List<Wave> waveQueue;
		public bool complete;

		public void Begin () {

		}

//		IEnumerator Loop () {
//			while (waveQueue.Count == 0) {
//
//			}

			// Pop off a wave
			// Loop through and spawn units
			// If repeat re-loop spawn unit
			// When complete pop off the next wave and repeat if waves are still available
//		}

		public void SpawnWave () {
			// Pop off
		}

		public bool IsComplete () {
			return waveQueue.Count == 0;
		}
	}
}
