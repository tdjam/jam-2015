using UnityEngine;
using System.Collections;

namespace Combat {
	[System.Serializable]
	public class StatCharge {
		[SerializeField] int charge = 20;
		int chargeMax;
		
		public int Charge {
			get { return charge; }
			set { charge = Mathf.Max(Mathf.Min(value, chargeMax), 0); }
		}
		
		public int ChargeMax {
			get { return chargeMax; }
			set { 
				chargeMax = value;
				if (charge > chargeMax) charge = chargeMax;
			}
		}
		
		[Tooltip("How much charge is generated per minute")]
		public int chargeRegen;
		float chargeRegenPool;
		
		public void ReceiveCharge (int val) {
			charge = Mathf.Min(val + charge, chargeMax);
		}
		
		public void RemoveCharge (int charge) {
			Charge -= charge;
		}
		
		public bool IsFull () {
			return charge == chargeMax;
		}
		
		// Force set the charge and max charge. Should be called with caution as it circumvents charge caps
		public void SetCharge (int charge) {
			this.charge = charge;
			this.chargeMax = charge;
		}
		
		// Calculates, finalizes, and sets stat curves
		public void Setup () {
			chargeMax = charge;
			charge = 0;
		}
		
		// Makes health regen stat generate additional hitpoints over time
		// @NOTE Designed to be called in the update loop
		public void ChargeRegen () {
			chargeRegenPool += chargeRegen / 60f * Time.deltaTime;
			
			if (chargeRegenPool > 1f) {
				int addCharge = (int)Mathf.Floor(chargeRegenPool);
				ReceiveCharge(addCharge);
				chargeRegenPool -= addCharge;
			}
		}
	}
}

