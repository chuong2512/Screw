using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BuyCoinButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private TextMeshProUGUI _priceText;

    [SerializeField] private int _index;

    public int Index
    {
        get => _index;
        set
        {
            _index = value;
            SetDataWithIndex();
        }
    }

    private int _coin;

    private void OnValidate()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        _button?.onClick.AddListener(OnClickButton);
    }

    public void SetDataWithIndex()
    {
        switch (_index)
        {
            case 0:
                _priceText.SetText("0.99$");
                _coin = 100;
                break;
            case 1:
                _priceText.SetText("2.99$");
                _coin = 300;
                break;
            case 2:
                _priceText.SetText("4.99$");
                _coin = 500;
                break;
            case 3:
                _priceText.SetText("9.99$");
                _coin = 1000;
                break;
            case 5:
                _priceText.SetText("0.49$");
                _coin = 10;
                break;
            case 4:

                if (GameDataManager.Instance.playerData.removeAds)
                {
                    _priceText.SetText("BOUGHT");
                    break;
                }

                _priceText.SetText("0.49$");
                break;
        }

        _text.SetText($"{_coin}");
    }

    private void OnClickButton()
    {
        IAPManager.OnPurchaseSuccess = AddCoin;

        switch (_index)
        {
            case 0:
                IAPManager.Instance.BuyProductID(IAPKey.PACK1);
                break;
            case 1:
                IAPManager.Instance.BuyProductID(IAPKey.PACK2);
                break;
            case 2:
                IAPManager.Instance.BuyProductID(IAPKey.PACK3);
                break;
            case 3:
                IAPManager.Instance.BuyProductID(IAPKey.PACK4);
                break;
            
            case 5:
                IAPManager.Instance.BuyProductID(IAPKey.PACK05);
                break;
            case 4:
                if (GameDataManager.Instance.playerData.removeAds)
                {
                    return;
                }

                IAPManager.OnPurchaseSuccess = RemoveAds;
                IAPManager.Instance.BuyProductID(IAPKey.PACK5);
                break;
        }
    }

    private void RemoveAds()
    {
        _priceText.SetText("BOUGHT");
        GameDataManager.Instance.playerData.RemoveAds();
    }

    private void AddCoin()
    {
        GameDataManager.Instance.playerData.AddDiamond(_coin);
    }
}