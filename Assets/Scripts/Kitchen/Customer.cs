using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;

using  CookingPrototype.Controllers;

using JetBrains.Annotations;

namespace CookingPrototype.Kitchen {
	public sealed class Customer : MonoBehaviour {
		public Image                    CustomerImage   = null;
		public List<Sprite>             CustomerSprites = null;
		public Image                    TimerBar        = null;
		public List<CustomerOrderPlace> OrderPlaces     = null;

		const string ORDERS_PREFABS_PATH = "Prefabs/Orders/{0}";

		public List<Order> Orders { get; private set; } = null;
		float       _timer    = 0f;
		bool        _isActive = false;

		/// <summary>
		/// Custom Func to get customers order
		/// </summary>
		public List<Order> GetCustomerOrders() {
			return Orders;
		}

		public float WaitTime {
			get { return CustomersController.Instance.CustomerWaitTime - _timer; }
		}

		/// <summary>
		/// Есть ли необслуженные заказы у указанного посетителя.
		/// </summary>
		public bool IsComplete { get { return Orders.Count == 0; } }

		void Update() {
			if ( !_isActive ) {
				return;
			}
			_timer += Time.deltaTime;
			TimerBar.fillAmount = WaitTime / CustomersController.Instance.CustomerWaitTime;

			if ( WaitTime <= 0f ) {
				CustomersController.Instance.FreeCustomer(this);
			}
		}

		[ContextMenu("Set random sprite")]
		void SetRandomSprite() {
			CustomerImage.sprite = CustomerSprites[Random.Range(0, CustomerSprites.Count)];
			CustomerImage.SetNativeSize();
		}

		public void Init(List<Order> orders) {
			Orders = orders;

			if ( Orders.Count > OrderPlaces.Count ) {
				Debug.LogError("There's too many orders for one customer");
				return;
			}

			OrderPlaces.ForEach(x => x.Complete());

			var i = 0;
			for ( ; i < Orders.Count; i++ ) {
				var order   = Orders[i];
				var place   = OrderPlaces[i];
				Instantiate(Resources.Load<GameObject>(string.Format(ORDERS_PREFABS_PATH, order.Name)), place.transform, false);
				place.Init(order);
			}

			SetRandomSprite();

			_isActive = true;
			_timer = 0f;
		}

		[UsedImplicitly]
		public bool ServeOrder(Order order) {
			var place = OrderPlaces.Find(x => x.CurOrder == order);
			if ( !place ) {
				return false;
			}
			Orders.Remove(order);
			place.Complete();
			_timer = Mathf.Max(0f, _timer - 6f);
			return true;
		}
	}
}
