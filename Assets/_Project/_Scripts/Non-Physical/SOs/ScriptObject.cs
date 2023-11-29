using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagnetFishing
{
    [CreateAssetMenu(fileName = "New Script Object", menuName = "Script Object")]
    public class ScriptObject : ScriptableObject
    {
        public GameObject ItemToDisplay;
        [TextArea]
        public string[] DialogueScript;
    }
}
