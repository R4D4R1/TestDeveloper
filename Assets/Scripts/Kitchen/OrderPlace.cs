using UnityEngine;

using System;
using System.Collections.Generic;
using System.Linq;

using CookingPrototype.Controllers;

namespace CookingPrototype.Kitchen {
	public sealed class OrderPlace : AbstractFoodPlace {

		public List<FoodPlace> Places          = new List<FoodPlace>();
		[HideInInspector]
		public List<string>    CurOrder        = new List<string>();
		public event Action    CurOrderUpdated;

		List<Order>  _possibleOrders = new List<Order>();

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
			_possibleOrders.AddRange(OrdersController.Instance.Orders);

		}

		//void Start() {
		//	_possibleOrders.AddRange(OrdersController.Instance.Orders);
		//}

		bool CanAddFood(Food food) {
			if ( CurOrder.Contains(food.Name) ) {
				return false;
			}

			foreach ( var order in _possibleOrders ) {
				foreach ( var orderFood in order.Foods.Where(x => x.Name == food.Name) ) {
					if ( string.IsNullOrEmpty(orderFood.Needs) || CurOrder.Contains(orderFood.Needs) ) {
						return true;
					}
				}
			}
			return false;
		}

		void UpdatePossibleOrders() {
			var ordersToRemove = new List<Order>();
			foreach ( var order in _possibleOrders ) {
				if ( order.Foods.Where(x => x.Name == CurOrder[CurOrder.Count - 1]).Count() == 0 ) {
					ordersToRemove.Add(order);
				}
			}
			_possibleOrders.RemoveAll(x => ordersToRemove.Contains(x));
		}

		public override bool TryPlaceFood(Food food) {
			if ( !CanAddFood(food) ) {
				return false;
			}

			foreach ( var place in Places ) {
				if ( !place.TryPlaceFood(food) ) {
					continue;
				}

				CurOrder.Add(food.Name);
				UpdatePossibleOrders();
				CurOrderUpdated?.Invoke();
				return true;
			}
			return false;
		}

		public override void FreePlace() {
			_possibleOrders.Clear();
			_possibleOrders.AddRange(OrdersController.Instance.Orders);

			CurOrder.Clear();

			foreach ( var place in Places ) {
				place.FreePlace();
			}

			CurOrderUpdated?.Invoke();
		}
	}
}
