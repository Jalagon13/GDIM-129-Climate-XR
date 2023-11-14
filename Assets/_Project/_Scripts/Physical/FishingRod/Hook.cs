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
        [SerializeField] private VectorVariable _rodTipPos;
        [SerializeField] private LineRenderer _hookLine;

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

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(_secTillFishCaught);

            StartMiniGame();
        }

        private void OnDestroy()
        {
            Destroy(_lr.gameObject);
            StopMiniGame();
        }

        private void LateUpdate()
        {
            _lr.SetPosition(0, _rodTipPos.Value);
            _lr.SetPosition(1, transform.position);
        }

        public void StartMiniGame()
        {
            _mg = Instantiate(_miniGamePrefab, transform.position += Vector3.up, Quaternion.identity);
            _mg.SetupMiniGame(this);
        }

        public void StopMiniGame()
        {
            Destroy(_mg.gameObject);
        }

        public void InitializeHook(Transform rodTipTransform)
        {
            _rb.AddForce(rodTipTransform.forward * 7f, ForceMode.Impulse);
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
                alreadyAnimating = true;
                StartCoroutine(BobberBounceCoroutine());
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
