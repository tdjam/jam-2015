using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Combat;

public class DetectEnemiesInVolume : MonoBehaviour {

    public int damage = 10;
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
            Stats enemyStats = other.gameObject.GetComponent<Stats>();
            enemyStats.ReceiveDamage(damage);
        }
    }

    IEnumerator Die() {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
