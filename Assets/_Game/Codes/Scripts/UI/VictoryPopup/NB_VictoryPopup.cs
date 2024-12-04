using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NultBolts
{
    public class NB_VictoryPopup : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] TextMeshProUGUI txt_ValueReward;
        [SerializeField] Button btn_Next;

        private void Start()
        {
            btn_Next.onClick.AddListener(Next);
        }

        void Next()
        {
            NultBoltsManager.Instance.ReloadLevel();
            gameObject.SetActive(false);

        }
    }
}
