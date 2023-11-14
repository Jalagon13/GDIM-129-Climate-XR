using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagnetFishing
{
    public class ReelMovementTracker : MonoBehaviour
    {
        [SerializeField] private FishingRod _thisRod;
        [SerializeField] private float _reelDetectionDistance;
        [SerializeField] private GameObject _leftControllerTracker;
        [SerializeField] private GameObject _rightControllerTracker;

        private Vector3 _lastLeftPosition;
        private Vector3 _lastRightPosition;
        private bool _isReeling;

        private void Awake()
        {
            _lastLeftPosition = _leftControllerTracker.transform.position;
            _lastRightPosition = _rightControllerTracker.transform.position;
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.1f);

            //if(_isReeling)
            //    GameSignals.ROD_ACTIVATED.Dispatch();
            //else
            //    GameSignals.ROD_DEACTIVATED.Dispatch();

            StartCoroutine(Start());
        }

        private void FixedUpdate()
        {
            if (Vector3.Distance(_leftControllerTracker.transform.position, _lastLeftPosition) > _reelDetectionDistance || Vector3.Distance(_rightControllerTracker.transform.position, _lastRightPosition) > _reelDetectionDistance)
            {
                _isReeling = true;
                _thisRod.ReelInHook();
            }

            _isReeling = false;
            _lastLeftPosition = _leftControllerTracker.transform.position;
            _lastRightPosition = _rightControllerTracker.transform.position;
        }
    }
}
