using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagnetFishing
{
    public class ItemDisplay : MonoBehaviour
    {
        public void Display(GameObject modelToShow)
        {
            var model = Instantiate(modelToShow, transform.position, Quaternion.identity);
            model.transform.SetParent(transform, true);
            model.transform.localScale = Vector3.one * 3;
            model.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        public void HideDisplay()
        {
            if(transform.childCount > 0)
            {
                foreach (Transform transform in transform)
                {
                    Destroy(transform.gameObject);
                }
            }
        }
    }
}
