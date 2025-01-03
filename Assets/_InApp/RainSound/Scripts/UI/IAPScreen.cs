using BabySound.Scripts;
using SingleApp;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BabySound
{
	public class IAPScreen : Singleton<IAPScreen>
	{
		[SerializeField] private BuyCoinButton[] _buttons;

		void Start()
		{
			for(int i=0;i<_buttons.Length;i++)
			{
				_buttons[i].SetDataWithIndex();
			}

			gameObject.SetActive(false);
		}
	}
}
