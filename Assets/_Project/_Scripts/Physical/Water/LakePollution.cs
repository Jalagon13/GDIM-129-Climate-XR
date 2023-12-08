using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagnetFishing
{
    public class LakePollution : MonoBehaviour
    {
        [SerializeField] private Material[] _lakeMaterialOrder;

        private MeshRenderer _mr;
        private int _counter;

        private void Awake()
        {
            _mr = GetComponent<MeshRenderer>();

            GameSignals.FISH_CAUGHT.AddListener(ChangeLakeMaterial);
        }

        private void OnDestroy()
        {
            GameSignals.FISH_CAUGHT.AddListener(ChangeLakeMaterial);
        }

        private void ChangeLakeMaterial(ISignalParameters parameters)
        {
            if (_counter > _lakeMaterialOrder.Length) return;

            _mr.material = _lakeMaterialOrder[_counter];
            _counter++;
        }
    }
}
