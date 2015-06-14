using UnityEngine;
using System.Collections;
using Combat;

namespace Cannon {
	public class CannonManager : MonoBehaviour {
		Stats stats;

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
		
		void OnDestroy () {
			if (cannonGui != null)
				Destroy(cannonGui.gameObject);
		}

		void Update () {
			cannonGui.charge.value = stats.charge.Charge;
			cannonGui.health.value = stats.health.Health;
		}
	}
}
