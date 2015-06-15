using UnityEngine;
using Combat;

namespace Enemy {
	public class EnemyDeath : MonoBehaviour {
		Stats stats;
		NavDestination dest;
		[SerializeField] GameObject deathParticles;

		void Awake () {
			dest = GetComponent<NavDestination>();
			stats = GetComponent<Stats>();
		}

		void Death () {
			deathParticles.transform.SetParent(transform.parent);
			deathParticles.SetActive(true);

			Destroy(gameObject);
		}

		void Update () {
			if (dest.IsDestination()) {
				Sm.cannon.GetComponent<Stats>().ReceiveDamage(stats.combat.attackDamage);
				Death();
			}

			if (stats.health.IsDead()) {
				Death();
			}
		}
	}
}
