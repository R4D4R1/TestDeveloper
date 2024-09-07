using UnityEngine;

using System.Collections.Generic;

using JetBrains.Annotations;

namespace CookingPrototype.Kitchen {
	public sealed class FoodPlacer : MonoBehaviour {
		public string                  FoodName = string.Empty;
		public List<AbstractFoodPlace> Places   = new List<AbstractFoodPlace>();

		[UsedImplicitly]
		public void TryPlaceFood() {
			foreach ( var place in Places ) {
				if ( place.TryPlaceFood(new Food(FoodName)) ) {
					Debug.Log(FoodName);
					return;
				}
			}
		}
	}
}
