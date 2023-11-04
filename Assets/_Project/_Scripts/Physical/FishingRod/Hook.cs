using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagnetFishing
{
    public class Hook : MonoBehaviour
    {
        [SerializeField] private VectorVariable _rodTipPos;
        [SerializeField] private LineRenderer _hookLine;

        private Rigidbody _rb;
        private LineRenderer _lr;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _lr = Instantiate(_hookLine);
        }

        private void OnDestroy()
        {
            Destroy(_lr.gameObject);
        }

        private void LateUpdate()
        {
            _lr.SetPosition(0, _rodTipPos.Value);
            _lr.SetPosition(1, transform.position);
        }

        public void InitializeHook(Transform rodTipTransform)
        {
            _rb.AddForce(rodTipTransform.forward * 7f, ForceMode.Impulse);
        }
    }
}
