using UnityEngine;
using System.Collections;

public class NavDestination : MonoBehaviour {
	NavMeshAgent agent;

	[Tooltip("Distance threshold to count destination as achieved")]
	[SerializeField] float endThreshold = 0.1f;

	public void Setup (GameObject destination) {
		agent = GetComponent<NavMeshAgent>();
		SetDestination(destination);
	}

	void SetDestination (GameObject destination) {
		agent.SetDestination(destination.transform.position);
	}

	public bool IsDestination () {
		return Vector3.Distance(transform.position, agent.destination) < endThreshold;
	}
}
