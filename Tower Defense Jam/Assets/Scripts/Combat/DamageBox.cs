using UnityEngine;
using System.Collections;

namespace Combat {
	public class DamageBox : MonoBehaviour {
		[SerializeField] Stats stats;
		
		[Tooltip("Decides whether or not to hit a target based on the passed tag")]
		[SerializeField] string filterTag;
		
		void OnTriggerStay (Collider other) {
			if (other.tag != filterTag) return;

			Stats targetStats = other.gameObject.GetComponent<Stats>();
			if (targetStats) {
				targetStats.ReceiveDamage(stats.combat.attackDamage);
			}
		}
	}

}
