using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToggleTower : MonoBehaviour {

    public GameObject dormantTower;
    public GameObject[] towers;

    int currentActive;

    // Use this for initialization
    void Start() {
        currentActive = towers.Length - 1;
    }

    // Update is called once per frame
    void Update() {
    }

    void OnMouseDown() {
        if (dormantTower.activeSelf) {
            dormantTower.SetActive(false);
        }
        towers[currentActive].SetActive(false);
        currentActive = (currentActive + 1) % towers.Length;
        towers[currentActive].SetActive(true);
    }
}
