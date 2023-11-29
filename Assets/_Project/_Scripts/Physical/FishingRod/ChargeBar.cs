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
        private float _forcePercentage = 0;
        private bool _charging;

        private void Awake()
        {
            _chargeBar = transform.GetChild(0).GetComponent<RectTransform>();
            _chargeText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            GameSignals.POWER_CHARGING.AddListener(PowerCharging);
            GameSignals.POWER_RELEASED.AddListener(PowerReleased);
            GameSignals.ROD_DESELECTED.AddListener(CancelCharge);
        }

        private void OnDisable()
        {
            GameSignals.POWER_CHARGING.RemoveListener(PowerCharging);
            GameSignals.POWER_RELEASED.RemoveListener(PowerReleased);
            GameSignals.ROD_DESELECTED.RemoveListener(CancelCharge);
        }

        private void Start()
        {
            EnableChargeUI(false);
        }

        private void FixedUpdate()
        {
            if (_charging)
            {
                _forcePercentage += 0.01f;

                if (_forcePercentage >= 1)
                    _forcePercentage = 1;

                _slider.value = _forcePercentage;
            }
        }

        private void CancelCharge(ISignalParameters parameters)
        {
            if (_charging)
            {
                EnableChargeUI(false);
                _charging = false;
            }
        }

        private void PowerCharging(ISignalParameters parameters)
        {
            _forcePercentage = 0f;
            _charging = true;

            EnableChargeUI(true);
        }

        private void PowerReleased(ISignalParameters parameters)
        {
            Signal signal = GameSignals.HOOK_RELEASED;
            signal.ClearParameters();
            signal.AddParameter("ForcePercentage", _forcePercentage);
            signal.Dispatch();

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
