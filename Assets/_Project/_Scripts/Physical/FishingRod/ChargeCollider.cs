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
            GameSignals.START_NEXT_MAIN_DIALOGUE.AddListener(DiableAbilityToCharge);
            GameSignals.HOOK_RELEASED.AddListener(DiableAbilityToCharge);
            GameSignals.MAIN_DIALOGUE_FINISHED.AddListener(EnableAbilityToCharge);
            GameSignals.FISH_CAUGHT.AddListener(EnableAbilityToCharge);
            GameSignals.FISH_GOT_AWAY.AddListener(EnableAbilityToCharge);
            GameSignals.ROD_DESELECTED.AddListener(DiableAbilityToCharge);
            GameSignals.ROD_SELECTED.AddListener(EnableAbilityToCharge);
        }

        private void OnDestroy()
        {
            GameSignals.START_NEXT_MAIN_DIALOGUE.RemoveListener(DiableAbilityToCharge);
            GameSignals.HOOK_RELEASED.RemoveListener(DiableAbilityToCharge);
            GameSignals.MAIN_DIALOGUE_FINISHED.RemoveListener(EnableAbilityToCharge);
            GameSignals.FISH_CAUGHT.RemoveListener(EnableAbilityToCharge);
            GameSignals.FISH_GOT_AWAY.RemoveListener(EnableAbilityToCharge);
            GameSignals.ROD_DESELECTED.RemoveListener(DiableAbilityToCharge);
            GameSignals.ROD_SELECTED.RemoveListener(EnableAbilityToCharge);
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
