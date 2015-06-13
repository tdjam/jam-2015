using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DetectEnemiesInVolume : MonoBehaviour {

    public float lifetime = 5f;

    // Use this for initialization
    void Start () {
        StartCoroutine("Die");
    }

    // Update is called once per frame
    void Update () {
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy") {
            Debug.LogFormat("Damaging {0} at {1}", other.tag, other.transform.position);
        }
    }

    IEnumerator Die() {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
