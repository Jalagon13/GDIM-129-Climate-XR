using MoreMountains.Feedbacks;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MagnetFishing
{
    public class Hook : MonoBehaviour
    {
        [SerializeField] private MiniGame _miniGamePrefab;
        [SerializeField] private float _secTillFishCaught; // temp delete later prolly. for debug for now.
        [SerializeField] private float _maxThrowForce;
        [SerializeField] private float _minThrowForce;
        [SerializeField] private VectorVariable _rodTipPos;
        [SerializeField] private LineRenderer _hookLine;
        [SerializeField] private MMF_Player _castFeedback;
        [SerializeField] private MMF_Player _lakeEnterFeedback;

        private MiniGame _mg;
        private Rigidbody _rb;
        private LineRenderer _lr;
        public bool inWater { get; private set; }  = false;

        public static bool alreadyAnimating = false;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _lr = Instantiate(_hookLine);
        }

        private void OnEnable()
        {
            _lr.enabled = true;
            _castFeedback?.PlayFeedbacks();
            StartCoroutine(DelayBeforeMiniGame());
        }

        private void OnDisable()
        {
            _lr.enabled = false;
            StopMiniGame();
        }

        private void LateUpdate()
        {
            _lr.SetPosition(0, _rodTipPos.Value);
            _lr.SetPosition(1, transform.position);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.TryGetComponent<WaterCollisionBehavior>(out var behavior))
            {
                _lakeEnterFeedback?.PlayFeedbacks();
            }
        }

        private IEnumerator DelayBeforeMiniGame()
        {
            yield return new WaitForSeconds(_secTillFishCaught);

            StartMiniGame();
        }

        public void StartMiniGame()
        {
            _mg = Instantiate(_miniGamePrefab, transform.position + Vector3.up, Quaternion.identity);
            _mg.SetupMiniGame(this);
        }

        public void StopMiniGame()
        {
            if(_mg != null)
                Destroy(_mg.gameObject);
        }

        public void ThrowHook(Transform rodTipTransform, float forcePercentage, Vector3 throwPosition)
        {
            transform.position = throwPosition;
            _rb.AddForce(rodTipTransform.forward * ((forcePercentage * _maxThrowForce) + _minThrowForce), ForceMode.Impulse);
        }

        public void ReelInOneTick(Transform rodTip)
        {
            float originalZ = gameObject.transform.position.z;
            Vector3 XAndZMove = Vector3.MoveTowards(gameObject.transform.position, rodTip.position, 0.05f);
            gameObject.transform.position = new Vector3(XAndZMove.x, gameObject.transform.position.y, XAndZMove.z);
        }

        public void ToggleInWater(bool value)
        {
            inWater = value;
        }

        public void EnterWaterBehavior()
        {
            inWater = true;
            if (alreadyAnimating == false)
            {
                /*alreadyAnimating = true;
                StartCoroutine(BobberBounceCoroutine());*/
            }
            else
            {
                //Debug.Log("Bobber already bouncing!");
            }
        }

        private IEnumerator BobberBounceCoroutine()
        {
            //Debug.Log("Moving bobber now. Original position: " + gameObject.transform.position);
            Vector3 originalPosition = gameObject.transform.position;
            for (float x = -0.5f; Math.Abs(x) > 0.005f; x = x * -0.5f)
            {
                //Debug.Log("x is " + x.ToString());
                if (x < 0.0f)
                {
                    //Debug.Log("Moving bobber to " + (originalPosition.y + x).ToString());
                    while (gameObject != null && gameObject.transform.position.y > originalPosition.y + x)
                    {
                        gameObject.transform.position = gameObject.transform.position - new Vector3(0, 0.01f, 0);
                        yield return null;
                    }
                    //Debug.Log("Moved!");
                }
                else
                {
                    //Debug.Log("Moving bobber to " + (originalPosition.y + x).ToString());
                    while (gameObject != null && gameObject.transform.position.y < originalPosition.y + x)
                    {
                        gameObject.transform.position = gameObject.transform.position + new Vector3(0, 0.01f, 0);
                        yield return null;
                    }
                    //Debug.Log("Moved!");
                }
                //Debug.Log("x will be " + (x * 0.5f).ToString());
            }
            alreadyAnimating = false;
            //Debug.Log("Bobber done bouncing!");

        }
    }
}
