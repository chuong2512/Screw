using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//电子邮件puhalskijsemen@gmail.com
//网站 开vpn打开 http://web3incubators.com/
//电报https://t.me/gamecode999

namespace NultBolts
{
    public class BoardScrewHole : MonoBehaviour, IHole
    {
        [SerializeField] private GameObject adIcon;
        [SerializeField] private GameObject lockObj;
        [SerializeField] private bool hasCrew;

        public const bool lockScrewOnly = false;

        public bool isLockAll { get; set; }
        public bool isLocked;
        public bool adsLocked;

        public bool CanPin => !hasCrew;

        private void OnEnable()
        {
            adIcon.SetActive(adsLocked);
            lockObj.SetActive(isLocked);
        }

        public void Pin(Screw screw)
        {
            hasCrew = true;
        }

        public void UnPin()
        {
            hasCrew = false;
        }

        public void UnlockAds()
        {
            adsLocked = false;
            adIcon.SetActive(false);
        }

        public void Unlock()
        {
            isLocked = false;
            lockObj.SetActive(false);
        }
    }
}
