using UnityEngine;
using System.Collections;

namespace Combat {
	public class Stats : MonoBehaviour {
		public StatHealth health;
		public StatCharge charge;
		public StatsCombat combat;

		void Awake () {
			health.Setup();
			charge.Setup();
		}
		
		// @TODO Use source insted of nested hitbox
		public virtual void ReceiveDamage (int damage) {
			if (!combat.attackable || health.IsDead()) return; // Cannot take damage if already dead or not attackable
			
			health.RemoveHealth(damage);
		}
		
		void Update () {
			if (health.healthRegen > 0) {
				health.HealthRegen();
			}

			if (charge.chargeRegen > 0) {
				charge.ChargeRegen();
			}
		}
	}
}

