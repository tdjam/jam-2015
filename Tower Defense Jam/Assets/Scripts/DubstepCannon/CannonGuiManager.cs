using UnityEngine;
using UnityEngine.UI;

namespace Cannon {
	public class CannonGuiManager : MonoBehaviour {
		public Slider health;
		public Slider charge;
		public Button fireCannon;

		public CannonManager manager;

		public void FireCannon () {
			manager.FireCannon();
		}
	}
}
