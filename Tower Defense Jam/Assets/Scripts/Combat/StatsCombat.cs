using UnityEngine;
using System.Collections.Generic;

namespace Combat {
	[System.Serializable]
	public class StatsCombat {
		[Tooltip("How much damage is dealt per attack?")]
		public int attackDamage = 10;

		[Tooltip("Does this unit take damage from attacks?")]
		public bool attackable = true;

		[Tooltip("Is this unit exempt from targeting for attacks?")]
		public bool targetable = true;
	}
}

