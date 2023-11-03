using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MagnetFishing
{
    public class ChargeBar : MonoBehaviour
    {
        private RectTransform _chargeBar;
        private TextMeshProUGUI _chargeText;

        private void Awake()
        {
            GameSignals.POWER_CHARGING.AddListener(PowerCharging);
            GameSignals.POWER_RELEASED.AddListener(PowerReleased);

            _chargeBar = transform.GetChild(0).GetComponent<RectTransform>();
            _chargeText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }

        private void OnDestroy()
        {
            GameSignals.POWER_CHARGING.RemoveListener(PowerCharging);
            GameSignals.POWER_RELEASED.RemoveListener(PowerReleased);
        }

        private void Start()
        {
            EnableChargeUI(false);
        }

        private void PowerCharging(ISignalParameters parameters)
        {
            EnableChargeUI(true);

        }

        private void PowerReleased(ISignalParameters parameters)
        {
            EnableChargeUI(false);
        }

        private void EnableChargeUI(bool _)
        {
            _chargeBar.gameObject.SetActive(_);
            _chargeText.gameObject.SetActive(_);
        }
    }
}
