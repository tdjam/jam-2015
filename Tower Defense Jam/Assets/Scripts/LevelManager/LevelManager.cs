using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Level {
	[System.Serializable]
	public class LevelManager : MonoBehaviour {
		[Tooltip("Each entry will be treated as a full level")]
		[SerializeField] List<LevelData> levels;

		[SerializeField] bool debug;
		[SerializeField] GameObject debugPrefab;

		[SerializeField] UnityEngine.UI.Text waveTextOutput;

		// If the wave has been complete
		[HideInInspector] public bool waveComplete = true;

		[SerializeField] Transform enemyParent;

		[Header("Locations")]
		[SerializeField] GameObject redStart;
		[SerializeField] GameObject redEnd;

		[SerializeField] GameObject blueStart;
		[SerializeField] GameObject blueEnd;

		[SerializeField] GameObject greenStart;
		[SerializeField] GameObject greenEnd;

		[SerializeField] GameObject altStart;
		[SerializeField] GameObject altEnd;

		[Header("Units")]
		[SerializeField] GameObject unitBasic;

		[HideInInspector] public int currentLevel = 0;

		void Awake () {
			waveComplete = true;
			Sm.level = this;
			if (debug) Instantiate(debugPrefab);
		}

		// Begin the next level
		public void NextLevel () {
			if (levels.Count == currentLevel || waveComplete == false) return;

			waveComplete = false;
			LevelData lv = levels[currentLevel];

			waveTextOutput.text = string.Format("Wave {0}", currentLevel + 1);
			waveTextOutput.gameObject.SetActive(true);

			StartCoroutine(RunLevel(lv));
		}

		// Runs a level until completion
		IEnumerator RunLevel (LevelData lv) {
			// Activate every spawners
			foreach (Spawner spawner in lv.spawners) {
				spawner.Begin();
			}

			// When all enemy spawners are complete continue
			int spawnerCompleteCount = 0;
			while (spawnerCompleteCount < lv.spawners.Count) {
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
			currentLevel += 1;
		}

		public IEnumerator RunSpawner (List<Wave> waveQueue, Spawner spawner) {
			Wave wave;
			while (spawner.waveCount < waveQueue.Count) {
				// Get the next wave
				wave = waveQueue[spawner.waveCount];
				wave.repeatCount = 0;

				// Run the wave until it is done repeating
				while (!wave.IsComplete()) {

					// Spawn units
					foreach (Unit unit in wave.squad) {

						GameObject origin = GetStartPoint(wave.spawnPoint);
						GameObject destination = GetEndPoint(unit.destination);

						GameObject newUnit = Object.Instantiate(Sm.level.GetUnit(unit.type), origin.transform.position, Quaternion.identity) as GameObject;
						newUnit.transform.SetParent(enemyParent);

						// @TODO Sets the destination, but this is a horrible way of doing it
						HutongGames.PlayMaker.FsmGameObject fsmTarget = newUnit.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmGameObject("destination");
						if (fsmTarget != null) fsmTarget.Value = destination;

						yield return new WaitForSeconds(unit.spawnDelay);
					}
					
					wave.repeatCount += 1;
					yield return new WaitForSeconds(wave.repeatDelay);
					
				}
				
				spawner.waveCount += 1;
			}
		}
		
		// Get the start point of a path
		public GameObject GetStartPoint (Pathway type) {
			switch (type) {
			case Pathway.Red:
				return redStart;
			case Pathway.Green:
				return greenStart;
			case Pathway.Blue:
				return blueStart;
			case Pathway.Alt:
				return altStart;
			}
			
			return null;
		}
		
		// Get the end point of a path
		public GameObject GetEndPoint (Pathway type) {
			switch (type) {
			case Pathway.Red:
				return redEnd;
			case Pathway.Green:
				return greenEnd;
			case Pathway.Blue:
				return blueEnd;
			case Pathway.Alt:
				return altEnd;
			}
			
			return null;
		}
		
		// Get a unit type
		public GameObject GetUnit (UnitType type) {
			switch (type) {
			case UnitType.Basic:
				return unitBasic;
			}
			
			return null;
		}
	}
}
