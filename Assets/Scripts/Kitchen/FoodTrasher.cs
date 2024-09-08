using System;

using UnityEngine;

using JetBrains.Annotations;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

namespace CookingPrototype.Kitchen {
	[RequireComponent(typeof(FoodPlace))]
	public sealed class FoodTrasher : MonoBehaviour {

		FoodPlace _place = null;
		float     _timer = 0f;
		int       _clickCount = 0;

		void Start() {
			_place = GetComponent<FoodPlace>();
			_timer = Time.realtimeSinceStartup;
		}

		/// <summary>
		/// Освобождает место по двойному тапу если еда на этом месте сгоревшая.
		/// </summary>
		[UsedImplicitly]
		public void TryTrashFood() {
			if ( _place.CurFood != null ) {
				if ( _place.CurFood.CurStatus == Food.FoodStatus.Overcooked ) {
					_clickCount++;
					RestartDoubleClickTime();

					if ( _clickCount == 2 )
						_place.FreePlace();
				}
			}
		}

		private async void RestartDoubleClickTime() {
			await Task.Delay(250);
			_clickCount = 0;
		}
	}
}
