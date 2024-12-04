using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NultBolts
{
    public class NB_GameplayMenu : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] private Button btnSetting;

        [SerializeField] private Button btnNext;
        [SerializeField] private Button btnReset;

        [SerializeField] private TextMeshProUGUI levelText;

        public void Start()
        {
            btnNext.onClick.AddListener(OnNext);
            btnReset.onClick.AddListener(OnReset);
            btnSetting.onClick.AddListener(OnSetting);
        }

        private void OnEnable()
        {
            ShowLevelTitle();
            NultBoltsManager.Instance.actionLoadLevel += ShowLevelTitle;
        }
        private void OnDisable()
        {
            NultBoltsManager.Instance.actionLoadLevel -= ShowLevelTitle;
        }

        void ShowLevelTitle()
        {
            levelText.text = $"LEVEL {DataManager.indexLevel_NB + 1}";
        }

        private void OnNext()
        {
            NultBoltsManager.Instance.ChangeGameState(TypeManager.GameState.Win);
        }
        private void OnReset()
        {
            NultBoltsManager.Instance.ReloadLevel();
        }
        private void OnSetting()
        {

        }
        private void OnClickShopTheme()
        {

        }
        private void OnClickRemoveAds()
        {

        }
        private void OnListLevel()
        {

        }
    }
}
