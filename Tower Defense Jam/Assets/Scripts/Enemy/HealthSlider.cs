using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using Combat;

public class HealthSlider : MonoBehaviour {

    const string HEALTH_BAR_PREFAB_LOC = "Prefabs/HealthBar";

    public Stats stats;

    GameObject healthBarPrefab;
    GameObject healthBar;
    Slider slider;

    void Awake() {
        healthBarPrefab = Resources.Load(HEALTH_BAR_PREFAB_LOC)
            as GameObject;
        healthBar = Instantiate(healthBarPrefab);
        healthBar.transform.SetParent(transform);
        healthBar.transform.localPosition = Vector3.zero;
        slider = healthBar.GetComponentInChildren<Slider>();
    }

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        healthBar.transform.rotation = Camera.main.transform.rotation;
        slider.value = ((float) stats.health.Health) / stats.health.HealthMax;
    }
}
