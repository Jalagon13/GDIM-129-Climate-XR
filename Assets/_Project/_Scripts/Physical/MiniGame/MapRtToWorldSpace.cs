using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagnetFishing
{
    public class MapRtToWorldSpace : MonoBehaviour
    {
        [SerializeField] private GameObject _targetGo;

        private RectTransform _localRect;

        private void Awake()
        {
            _localRect = GetComponent<RectTransform>();
        }

        private void Update()
        {
            Vector3 worldPos = GetWorldSpacePosition(_localRect);
            _targetGo.transform.SetPositionAndRotation(worldPos, Quaternion.identity);
        }

        private Vector3 GetWorldSpacePosition(RectTransform rectTransform)
        {
            // Use RectTransformUtility to convert local position to world position
            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);

            // The world position can be any of the corners. Here, we use the bottom-left corner.
            return corners[0];
        }
    }
}
