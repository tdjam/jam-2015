using UnityEngine;
using System.Collections;
using VolumetricLines;

namespace Cannon {
	public class CannonLaser : MonoBehaviour {
		VolumetricLineBehavior lineBehaviour;

		[Tooltip("The target our laser will stick to (from the base)")]
		[SerializeField] Transform snapTarget;

		[Tooltip("How far should the laser beam shoot before stopping? Making it too distant will get quite expensive")]
		public float fireDistanceMax = 10;
		float fireDistance = 0;
		
		[Tooltip("Damping speed applied to laser extension and retraction")]
		[SerializeField] float fireSmoothing;

		[Tooltip("Speed cap applied to the fire")]
		[SerializeField] float maxSpeed;

		float fireDampVelocity;

		void Awake () {
			lineBehaviour = GetComponent<VolumetricLineBehavior>();
		}

		void Update () {
			transform.position = snapTarget.position;
			transform.rotation = snapTarget.rotation;
		}

		public void FireLaser () {
			StopAllCoroutines();
			StartCoroutine(MoveLaserForward());
		}

		IEnumerator MoveLaserForward () {
			while (lineBehaviour.m_endPos.y < fireDistanceMax) {
				fireDistance = Mathf.SmoothDamp(fireDistance, fireDistanceMax, ref fireDampVelocity, fireSmoothing, maxSpeed, Time.deltaTime);
				lineBehaviour.m_endPos.y = fireDistance;
				yield return null;
			}
		}

		public void StopLaser () {
			StopAllCoroutines();
			StartCoroutine(MoveLaserBack());
		}


		IEnumerator MoveLaserBack () {
			while (lineBehaviour.m_endPos.y > 0) {
				fireDistance = Mathf.SmoothDamp(fireDistance, 0, ref fireDampVelocity, fireSmoothing, maxSpeed, Time.deltaTime);
				lineBehaviour.m_endPos.y = fireDistance;
				yield return null;
			}
		}
	}
}
