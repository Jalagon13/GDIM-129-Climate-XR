using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MagnetFishing
{
    public class ChargeBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private RectTransform _chargeBar;
        private TextMeshProUGUI _chargeText;
        private float _sliderValue = 0;
        private bool _charging;

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

        private void FixedUpdate()
        {
            if (_charging)
            {
                _sliderValue += 0.01f;

                if (_sliderValue >= 1)
                    _sliderValue = 1;
            }
        }

        private void Start()
        {
            EnableChargeUI(false);
        }

        private void PowerCharging(ISignalParameters parameters)
        {
            _sliderValue = 0f;
            _charging = true;

            EnableChargeUI(true);
        }

        private void PowerReleased(ISignalParameters parameters)
        {
            _charging = false;

            EnableChargeUI(false);
        }

        private void EnableChargeUI(bool _)
        {
            _chargeBar.gameObject.SetActive(_);
            _chargeText.gameObject.SetActive(_);
        }
    }
}
