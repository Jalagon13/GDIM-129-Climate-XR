using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MagnetFishing
{
    public class ItemDisplay : MonoBehaviour
    {
        [SerializeField] private Canvas _caughtCanvas;
        [SerializeField] private TextMeshProUGUI _caughtText;

        private void Start()
        {
            EnableCanvas(false);
        }

        public void Display(GameObject modelToShow, string trashName, int scaleFactor = 1)
        {
            var model = Instantiate(modelToShow, transform.position, Quaternion.identity);
            model.transform.SetParent(transform, true);
            model.transform.localScale = Vector3.one * 3 * scaleFactor;
            model.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            EnableCanvas(true);
            UpdateText(trashName);
        }

        private void EnableCanvas(bool _)
        {
            _caughtCanvas.gameObject.SetActive(_);
        }

        private void UpdateText(string trashName)
        {
            _caughtText.text = $"Item Caught!<br>{trashName}";
        }

        public void HideDisplay()
        {
            EnableCanvas(false);

            if (transform.childCount > 0)
            {
                foreach (Transform transform in transform)
                {
                    Destroy(transform.gameObject);
                }
            }
        }
    }
}
