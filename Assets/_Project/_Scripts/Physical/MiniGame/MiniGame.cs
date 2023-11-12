using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagnetFishing
{
    public class MiniGame : MonoBehaviour
    {
        [SerializeField] private RectTransform _reelBarPivot;
        [SerializeField] private float _miniGameDuration;
        [Range(0, 0.1f)]
        [SerializeField] private float _rotChangeStrength;

        private bool _reeling;
        private float _rotation = 1;
        private Hook _hook;

        private void Awake()
        {
            GameSignals.ROD_ACTIVATED.AddListener(ReelingIn);
            GameSignals.ROD_DEACTIVATED.AddListener(NotReelingIn);
        }

        private void OnDestroy()
        {
            GameSignals.ROD_ACTIVATED.RemoveListener(ReelingIn);
            GameSignals.ROD_DEACTIVATED.RemoveListener(NotReelingIn);
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(_miniGameDuration);

            GameSignals.FISH_GOT_AWAY.Dispatch();
        }

        private void FixedUpdate()
        {
            _reelBarPivot.Rotate(new(0, 0, CalculateRotation()));

            SetPosition();
        }

        private void SetPosition()
        {
            if (_hook == null) return;

            transform.SetPositionAndRotation(_hook.transform.position, Quaternion.identity);
        }

        public void SetupMiniGame(Hook hook)
        {
            _hook = hook;
        }

        private void ReelingIn(ISignalParameters parameters)
        {
            _reeling = true;
        }

        private void NotReelingIn(ISignalParameters parameters)
        {
            _reeling = false;
        }

        private float CalculateRotation()
        {
            if (_reeling)
            {
                _rotation -= _rotChangeStrength;

                if (_rotation < -1)
                    _rotation = -1;
            }
            else
            {
                _rotation += _rotChangeStrength;

                if(_rotation > 1)
                    _rotation = 1;
            }

            return _rotation;
        }
    }
}
