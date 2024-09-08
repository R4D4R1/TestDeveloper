using UnityEngine;

using System.Collections.Generic;
using System;
using CookingPrototype.Controllers;

namespace CookingPrototype.Kitchen {
	public sealed class AutoFoodFiller : MonoBehaviour {
		public string                  FoodName = null;
		public List<AbstractFoodPlace> Places   = new List<AbstractFoodPlace>();
		bool						   _isGameStarted = false;

		private void Start() {
			UIController.Instance.GameStarted += OnGameStarted;
		}

		void OnDestroy() {
			if ( UIController.Instance ) {
				UIController.Instance.GameStarted -= OnGameStarted;

			}

			if ( UIController.Instance ) {
				UIController.Instance.GameStarted -= OnGameStarted;
			}
		}

		private void OnGameStarted() {
			_isGameStarted = true;
		}

		void Update() {
			if ( _isGameStarted ) {
				foreach ( var place in Places ) {
					place.TryPlaceFood(new Food(FoodName));
				}
			}
		}
	}
}
