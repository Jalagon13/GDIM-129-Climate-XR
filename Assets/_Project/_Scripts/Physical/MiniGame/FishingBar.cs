using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagnetFishing
{
    public class FishingBar : MonoBehaviour
    {
        [SerializeField] private Material _activatedMaterial;
        [SerializeField] private Material _deactivatedMaterial;


        private MeshRenderer _mr;
        private Rigidbody _rb;
        private bool _reeling;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _mr = GetComponent<MeshRenderer>();

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
                _rb.AddForce(Vector3.up * 0.3f, ForceMode.Impulse);

            FollowParentTransform();
        }

        private void FollowParentTransform()
        {
            Vector3 temp = transform.position;
            temp.x = transform.root.transform.position.x;
            temp.z = transform.root.transform.position.z;
            transform.position = temp;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("TrashIcon")) return;

            _mr.material = _activatedMaterial;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("TrashIcon")) return;
            
            _mr.material = _deactivatedMaterial;
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
