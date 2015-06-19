using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ToggleTower : MonoBehaviour {

    public uint cost = 10;
    public GameObject dormantTower;
    public GameObject[] towers;
	[SerializeField] GameObject purchaseSign;
	[SerializeField] Text purchaseSignText;

	public AudioClip[] spawnSounds;
	public AudioClip hoverSound; 
	AudioSource audio;

	bool purchased;
    int currentActive;

    void Start() {
        currentActive = towers.Length - 1;
		purchaseSignText.text = string.Format("COST: ${0}", cost.ToString());
		audio = transform.GetComponent<AudioSource> ();
    }

	void OnMouseEnter () {
		if (!purchased) {
			purchaseSign.gameObject.SetActive(true);
		}
		audio.PlayOneShot (hoverSound);
	}

	void OnMouseExit () {
		purchaseSign.gameObject.SetActive(false);
	}

    void OnMouseDown() {
        if (dormantTower.activeSelf) {
            if (Sm.currency.IsMoney(cost)) {
				Sm.currency.Spend(cost);
                dormantTower.SetActive(false);
				purchased = true;
            } else {
                Debug.Log("Insufficient funds!");
                return;
            }
        }

		PlaySpawnSound();

        towers[currentActive].SetActive(false);
        currentActive = (currentActive + 1) % towers.Length;
        towers[currentActive].SetActive(true);
    }

	void PlaySpawnSound() {

		int rand = Random.Range (0, spawnSounds.Length);
		audio.PlayOneShot (spawnSounds [rand]);

	}

}
