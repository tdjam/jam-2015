using UnityEngine;
using System.Collections;
using Combat;

namespace Cannon {
	public class CannonManager : MonoBehaviour {
		Stats stats;

		[Tooltip("How fast will the charge decrease when firing")]
		[SerializeField] int chargeDecreaseRate;

		[Tooltip("Allow the cannon to be fired as often as you want?")]
		[SerializeField] bool debug;
		
		[Tooltip("Cannon GUI that should be generated and attached at run-time")]
		[SerializeField] GameObject cannonGuiPrefab;
		CannonGuiManager cannonGui;
		
		void Start () {
			Sm.cannon = this;

			stats = GetComponent<Stats>();

			// Setup the stats
			cannonGui = Instantiate(cannonGuiPrefab).GetComponent<CannonGuiManager>();
			cannonGui.charge.maxValue = stats.charge.ChargeMax;
			cannonGui.health.maxValue = stats.health.HealthMax;
		}

		public void FireCannon () {
			// If fully charged and not firing
		}
		
//		IEnumerator CannonFireLoop () {
			// turn on the laser
			// Target the nearest enemy with a relative targeting point

			// Decrase charge at the appropriate rate
			// When enemy is killed find the next nearest enemy to that targeting point
			// Repeat until the charge runs out
//		}

		void Update () {
			// @TODO Smooth damp these values
			cannonGui.charge.value = stats.charge.Charge;
			cannonGui.health.value = stats.health.Health;

			// Enable the fire button if the cannon is fully charged
		}

		void OnDestroy () {
			if (cannonGui != null)
				Destroy(cannonGui.gameObject);
		}
	}
}
