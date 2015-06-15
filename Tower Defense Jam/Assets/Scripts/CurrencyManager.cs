using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurrencyManager : MonoBehaviour {
	[SerializeField] Text textOutput;
    [SerializeField] uint money = 0;

    void Awake () {
		Sm.currency = this;
		UpdateText();
    }

	void UpdateText () {
		if (textOutput != null) {
			textOutput.text = string.Format("BITS: {0}", money.ToString());
		}
	}

	public bool IsMoney (uint money) {
		return money >= this.money;
	}

	public void Spend (uint money) {
		this.money -= money;
		UpdateText();
	}

	public void Receive (uint money) {
		this.money += money;
		UpdateText();
	}
}
