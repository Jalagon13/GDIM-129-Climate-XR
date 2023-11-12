using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagnetFishing
{
    public class ReelingDetection : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("ReelBar"))
            {
                GameSignals.REELING_IN.Dispatch();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("ReelBar"))
            {
                GameSignals.NOT_REELING_IN.Dispatch();
            }
        }
    }
}
