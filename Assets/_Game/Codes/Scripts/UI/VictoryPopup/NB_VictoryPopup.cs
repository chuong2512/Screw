using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NultBolts
{
    public class NB_VictoryPopup : MonoBehaviour
    {
        public static int reward = 100;
        [Header("Setup")]
        [SerializeField] TextMeshProUGUI txt_ValueReward;
        [SerializeField] Button btn_Next;

        private void Start()
        {
            btn_Next.onClick.AddListener(Next);

            txt_ValueReward.text = reward.ToString();
        }

        private void OnEnable()
        {
            GameDataManager.Instance.playerData.AddDiamond(reward);
        }

        void Next()
        {
            NultBoltsManager.Instance.ReloadLevel();
            gameObject.SetActive(false);
        }
    }
}
