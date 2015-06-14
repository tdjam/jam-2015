using UnityEngine;
using System.Collections;

namespace Combat {
	[System.Serializable]
	public class StatHealth {
		[SerializeField] int health = 20;
		int healthMax;
		
		public int Health {
			get { return health; }
			set { health = Mathf.Max(Mathf.Min(value, healthMax), 0); }
		}

		public int HealthMax {
			get { return healthMax; }
			set { 
				healthMax = value;
				if (health > healthMax) health = healthMax;
			}
		}
		
		[Tooltip("How much health is healed per minute")]
		public int healthRegen;
		float healthRegenPool;

		[Tooltip("Reduces the total damage taken by this number (min of 1)")]
		public int defense;

		[Tooltip("Doesn't die when reaching 0 health")]
		public bool immortal;

		[Tooltip("Never takes damage, still receives attacks")]
		public bool invulnerable;

		public void ReceiveHealth (int val) {
			health = Mathf.Min(val + health, healthMax);
		}

		public void RemoveHealth (int amount) {
			if (invulnerable) return;

			int damage = Mathf.Max(1, amount - defense);
			Health -= damage;
		}

		public bool IsDead () {
			if (immortal) {
				return false;
			}

			return health <= 0;
		}

		// Force set the health and max health. Should be called with caution as it circumvents health caps
		public void SetHealthForce (int health) {
			this.health = health;
			this.healthMax = health;
		}

		// Calculates, finalizes, and sets stat curves
		public void Setup () {
			health = Mathf.Max(1, health); // Health can never be less than 1
			healthMax = health;
		}

		// Makes health regen stat generate additional hitpoints over time
		// @NOTE Designed to be called in the update loop
		public void HealthRegen () {
			healthRegenPool += healthRegen / 60f * Time.deltaTime;
			
			if (healthRegenPool > 1f) {
				int addHealth = (int)Mathf.Floor(healthRegenPool);
				ReceiveHealth(addHealth);
				healthRegenPool -= addHealth;
			}
		}
	}
}

