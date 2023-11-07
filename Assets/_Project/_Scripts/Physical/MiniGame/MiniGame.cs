using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagnetFishing
{
    public class MiniGame : MonoBehaviour
    {
        private GameObject _hookObject;
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        public void Setup(Hook hook)
        {
            _hookObject = hook.gameObject;
        }

        private void Update()
        {
            if (_hookObject == null) return;

            transform.position = _hookObject.transform.position;
            transform.LookAt(_mainCamera.transform.position);
        }
    }
}
