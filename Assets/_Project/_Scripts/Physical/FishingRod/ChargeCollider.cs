using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagnetFishing
{
    public class ChargeCollider : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("RodTip")) return;

            Debug.Log("Power Charging...");
            GameSignals.POWER_CHARGING.Dispatch();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("RodTip")) return;

            Debug.Log("Power Released!");
            GameSignals.POWER_RELEASED.Dispatch();
        }
    }
}
