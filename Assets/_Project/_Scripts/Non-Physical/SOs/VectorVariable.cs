using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagnetFishing
{
    [CreateAssetMenu(fileName = "New Vector Variable", menuName = "Variables/Vector Variable")]
    public class VectorVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public Vector3 Value;

        public void SetVector(Vector3 vector)
        {
            Value = vector;
        }
    }
}
