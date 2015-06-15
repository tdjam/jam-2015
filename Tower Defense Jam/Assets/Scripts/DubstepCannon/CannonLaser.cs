using UnityEngine;
using System.Collections;
using VolumetricLines;

namespace Cannon {
	public class CannonLaser : MonoBehaviour {
		[Tooltip("The target to execute this script on")]
		[SerializeField] GameObject target;
		VolumetricLineBehavior lineBehaviour;

		[Tooltip("The target our laser will stick to (from the base)")]
		[SerializeField] Transform snapTarget;

		[Tooltip("How far should the laser beam shoot before stopping? Making it too distant will get quite expensive")]
		public float fireDistanceMax = 10f;
		float fireDistance = 0f;
		
		[Tooltip("Damping speed applied to laser extension and retraction")]
		[SerializeField] float fireSmoothing;

		[Tooltip("Speed cap applied to the fire")]
		[SerializeField] float maxSpeed;

		float fireDampVelocity;

		void Awake () {
			lineBehaviour = target.GetComponent<VolumetricLineBehavior>();

			target.SetActive(false);
		}

		void Update () {
			target.transform.position = snapTarget.position;
			target.transform.rotation = snapTarget.rotation;
		}

		void Reset () {
			lineBehaviour.m_endPos = Vector3.zero;
			lineBehaviour.m_startPos = Vector3.zero;
		}

		public void FireLaser () {
			StopAllCoroutines();
			StartCoroutine(MoveLaserForward());
		}

		IEnumerator MoveLaserForward () {
			Reset();
			target.SetActive(true);
			fireDistance = 0f;

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
			while (lineBehaviour.m_startPos.y < lineBehaviour.m_endPos.y - 0.1f) {
				lineBehaviour.m_startPos.y = Mathf.SmoothDamp(lineBehaviour.m_startPos.y, lineBehaviour.m_endPos.y, ref fireDampVelocity, fireSmoothing, maxSpeed, Time.deltaTime);
				yield return null;
			}

			target.SetActive(false);
			Reset();
		}
	}
}
