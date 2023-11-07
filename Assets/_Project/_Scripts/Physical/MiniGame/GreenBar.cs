using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagnetFishing
{
    public class GreenBar : MonoBehaviour
    {
        private Rigidbody _rb;
        private bool _reeling;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();

            GameSignals.ROD_ACTIVATED.AddListener(Reeling);
            GameSignals.ROD_DEACTIVATED.AddListener(NotReeling);
        }

        private void OnDestroy()
        {
            GameSignals.ROD_ACTIVATED.RemoveListener(Reeling);
            GameSignals.ROD_DEACTIVATED.RemoveListener(NotReeling);
        }

        private void FixedUpdate()
        {
            if(_reeling)
                _rb.AddForce(Vector3.up * 0.5f, ForceMode.Impulse);
        }

        private void Reeling(ISignalParameters parameters)
        {
            _reeling = true;
        }

        private void NotReeling(ISignalParameters parameters)
        {
            _reeling = false;
        }
    }
}
