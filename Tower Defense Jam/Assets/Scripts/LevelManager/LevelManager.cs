using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Level {
	[System.Serializable]
	public class LevelManager : MonoBehaviour {
		[SerializeField] bool debug;
		[SerializeField] GameObject debugPrefab;

		[Tooltip("Each entry will be treated as a full level")]
		[SerializeField] List<LevelData> levels;

		// If the wave has been complete
		[HideInInspector] public bool waveComplete;

		public void NextLevel () {
			if (levels.Count == 0 || waveComplete == false) return;

			waveComplete = false;
			LevelData lv = levels[0];
			levels.RemoveAt(0);

			StartCoroutine(RunLevel(lv));
		}

		IEnumerator RunLevel (LevelData lv) {
			// Activate every spawners
			foreach (Spawner spawner in lv.spawners) {
				spawner.Begin();
			}

			// When all enemy spawners are complete continue
			int spawnerCompleteCount = 0;
			while (spawnerCompleteCount != lv.spawners.Count) {
				yield return new WaitForSeconds(3f);

				spawnerCompleteCount = 0;
				foreach (Spawner spawner in lv.spawners) {
					if (spawner.IsComplete()) spawnerCompleteCount += 1;
				}
			}

			// Verify all enemies on the map have been destroyed
			while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0) {
				yield return new WaitForSeconds(1f);
			}

			waveComplete = true;
		}
	}
}
