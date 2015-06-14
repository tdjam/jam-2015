using UnityEngine;
using System.Collections;

public class LevelManagerDebug : MonoBehaviour {
	[Tooltip("The target wave")]
	[SerializeField] int wave;

	public void SetWave () {
		Sm.level.currentLevel = wave;
		Sm.level.NextLevel();
	}
	
	public void NextWave () {
		Sm.level.NextLevel();
	}
}
