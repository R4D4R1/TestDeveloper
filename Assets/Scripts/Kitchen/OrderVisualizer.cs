using System.Collections.Generic;

using UnityEngine;

namespace CookingPrototype.Kitchen {
	public sealed class OrderVisualizer : MonoBehaviour {
		public List<FoodVisualizer> Visualizers = new List<FoodVisualizer>();

		void Start() {
			Clear();
		}

		void Clear() {
			Visualizers.ForEach(x => x.SetEnabled(false));
			//Visualizers.ForEach(x => Debug.Log(x.name + " " + x.transform.parent.name));
		}

		public void Init(List<string> foods) {
			Clear();
			foreach ( var vis in Visualizers ) {
				if ( foods.Contains(vis.Name) ) {
					vis.SetEnabled(true);
				}
			}
		}
	}
}
