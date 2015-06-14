using UnityEngine;
using System.Collections;

namespace Cannon {
	public class CannonAimer : MonoBehaviour {
		[Tooltip("Object to rotate")]
		[SerializeField] Transform rotateTarget;
		
		[Tooltip("What we are aiming at")]
		public Transform aimTarget;
		
		[Tooltip("Offset the aim at rotation. Used to make the graphic line up correctly")]
		[SerializeField] Vector3 aimAngleOffset;
		
		[Tooltip("Forward posititon offset after rotation. Used to move the graphic relative to where it is rotating")]
		[SerializeField] float forwardAimOffset;
		
		[Tooltip("How long it takes to relatively point at a new target")]
		[SerializeField] float aimTime;
		
		[Tooltip("Attempts to cap the aim speed so damping isn't too fast")]
		[SerializeField] float maxAimSpeed;
		
		Vector3 originPos;
		Vector3 dampVelocity;
		Vector3 aimPos;
		
		void Start () {
			originPos = rotateTarget.position;
		}
		
		// @TODO Use coroutine to only aim when not pointing at the target maybe?
		void Update () {
			if (aimTarget == null) return;
			
			rotateTarget.position = originPos;
			aimPos = Vector3.SmoothDamp(aimPos, aimTarget.position, ref dampVelocity, aimTime, maxAimSpeed);
			
			rotateTarget.rotation = Quaternion.LookRotation(aimPos - rotateTarget.position) * Quaternion.Euler(aimAngleOffset.x, aimAngleOffset.y, aimAngleOffset.z);
			rotateTarget.position = Vector3.MoveTowards(rotateTarget.position, aimPos, forwardAimOffset);
		}
	}
}

