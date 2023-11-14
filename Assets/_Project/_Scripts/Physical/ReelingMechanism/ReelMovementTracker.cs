using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagnetFishing
{
    public class ReelMovementTracker : MonoBehaviour
    {
        [SerializeField] private FishingRod _thisRod;
        [SerializeField] private GameObject _leftControllerTracker;
        [SerializeField] private GameObject _rightControllerTracker;
        private Vector3 _lastLeftPosition;
        private Vector3 _lastRightPosition;
        // Start is called before the first frame update
        void Start()
        {
            _lastLeftPosition = _leftControllerTracker.transform.position;
            _lastRightPosition = _rightControllerTracker.transform.position;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (Vector3.Distance(_leftControllerTracker.transform.position, _lastLeftPosition) > 0.05f || Vector3.Distance(_rightControllerTracker.transform.position, _lastRightPosition) > 0.05f)
            {
                _thisRod.ReelInHook();
            }
            _lastLeftPosition = _leftControllerTracker.transform.position;
            _lastRightPosition = _rightControllerTracker.transform.position;
        }
    }
}
