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

	bool purchased;
    int currentActive;

    void Start() {
        currentActive = towers.Length - 1;
		purchaseSignText.text = string.Format("COST: ${0}", cost.ToString());
    }

	void OnMouseEnter () {
		if (!purchased) {
			purchaseSign.gameObject.SetActive(true);
		}
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

        towers[currentActive].SetActive(false);
        currentActive = (currentActive + 1) % towers.Length;
        towers[currentActive].SetActive(true);
    }
}
