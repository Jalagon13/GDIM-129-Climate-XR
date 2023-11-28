using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagnetFishing
{
    public class ChargeCollider : MonoBehaviour
    {
        private bool _canCharge;

        private void Awake()
        {
            GameSignals.MAIN_DIALOGUE_STARTED.AddListener(DiableAbilityToCharge);
            GameSignals.MAIN_DIALOGUE_FINISHED.AddListener(EnableAbilityToCharge);
        }

        private void OnDestroy()
        {
            GameSignals.MAIN_DIALOGUE_STARTED.RemoveListener(DiableAbilityToCharge);
            GameSignals.MAIN_DIALOGUE_FINISHED.RemoveListener(EnableAbilityToCharge);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("RodTip") || !_canCharge) return;

            GameSignals.POWER_CHARGING.Dispatch();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("RodTip") || !_canCharge) return;

            GameSignals.POWER_RELEASED.Dispatch();
        }

        private void DiableAbilityToCharge(ISignalParameters parameters)
        {
            _canCharge = false;
        }

        private void EnableAbilityToCharge(ISignalParameters parameters)
        {
            _canCharge = true;
        }
    }
}
