using System.Collections.Generic;

using UnityEngine;

using JetBrains.Annotations;

namespace CookingPrototype.Kitchen {
	[RequireComponent(typeof(FoodPlace))]
	public sealed class FoodTransfer : MonoBehaviour {
		public bool                    OnlyTransferCooked = true;
		public List<AbstractFoodPlace> DestPlaces         = new List<AbstractFoodPlace>();

		FoodPlace _place = null;

		void Start() {
			_place = GetComponent<FoodPlace>();
		}

		[UsedImplicitly]
		public void TryTransferFood() {
			var food = _place.CurFood;

			if ( food == null ) {
				return;
			}

			if ( OnlyTransferCooked && (food.CurStatus != Food.FoodStatus.Cooked) ) {
				_place.TryPlaceFood(food);
				return;
			}
			foreach ( var place in DestPlaces ) {
				if ( !place.TryPlaceFood(food) ) {
					continue;
				}
				_place.FreePlace();
				return;
			}
		}
	}
}
