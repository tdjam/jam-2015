using UnityEngine;
using Combat;


namespace Enemy {
	public class EnemyDeath : MonoBehaviour {
		Stats stats;

		void Awake () {
			stats = GetComponent<Stats>();
		}

		void Update () {
			if (stats.health.IsDead()) {
				Destroy(gameObject);
			}
		}
	}
}
