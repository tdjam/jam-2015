using UnityEngine;
using System.Collections;
using Combat;

namespace Cannon {
	public class CannonManager : MonoBehaviour {
		Stats stats;
		CannonAimer aimer;
		CannonLaser laser;

		[SerializeField] float laserDuration;

		[Tooltip("Allow the cannon to be fired as often as you want?")]
		[SerializeField] bool debug;
		
		[Tooltip("Cannon GUI that should be generated and attached at run-time")]
		[SerializeField] GameObject cannonGuiPrefab;
		CannonGuiManager cannonGui;
		
		void Start () {
			Sm.cannon = this;

			stats = GetComponent<Stats>();
			aimer = GetComponent<CannonAimer>();
			laser = GetComponent<CannonLaser>();

			// Setup the stats
			cannonGui = Instantiate(cannonGuiPrefab).GetComponent<CannonGuiManager>();
			cannonGui.charge.maxValue = stats.charge.ChargeMax;
			cannonGui.health.maxValue = stats.health.HealthMax;
			cannonGui.manager = this;

			if (debug) {
				stats.charge.Charge = stats.charge.ChargeMax;
			}
		}

		public void FireCannon () {
			if (stats.charge.IsFull()) {
				stats.charge.Charge = 0;

				if (debug) {
					stats.charge.Charge = stats.charge.ChargeMax;
				} 

				StartCoroutine(CannonFireLoop());
			}
		}
		
		IEnumerator CannonFireLoop () {
			laser.FireLaser();
			float timer = laserDuration;
			Stats targetStats = null;

			while (timer > 0f) {
				if (targetStats == null || targetStats.health.IsDead()) {
					GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
					if (enemy != null) {
						targetStats = enemy.GetComponent<Stats>();
						aimer.aimTarget = targetStats.transform;
					}
				}

				timer -= Time.deltaTime;
				yield return null;
			}

			aimer.aimTarget = null;
			laser.StopLaser();
		}

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
