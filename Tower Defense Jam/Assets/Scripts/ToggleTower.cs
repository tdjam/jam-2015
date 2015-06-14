using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToggleTower : MonoBehaviour {

    public GameObject[] towers;

    int currentActive = 0;

    // Use this for initialization
    void Start() {
        towers[currentActive].SetActive(true);
    }

    // Update is called once per frame
    void Update() {
    }

    void OnMouseDown() {
        towers[currentActive].SetActive(false);
        currentActive = (currentActive + 1) % towers.Length;
        towers[currentActive].SetActive(true);
    }
}
