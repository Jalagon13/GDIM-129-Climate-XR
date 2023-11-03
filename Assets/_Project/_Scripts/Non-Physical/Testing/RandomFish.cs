using System.Collections.Generic;
using UnityEngine;

public class GachaSystem : MonoBehaviour
{
    [System.Serializable]
    public class GachaItem
    {
        public string itemName;
        public int count; // counts of items
    }

    public List<GachaItem> gachaPool = new List<GachaItem>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            DrawItem();
        }
    }

    private void DrawItem()
    {
        if (gachaPool.Count == 0)
        {
            UnityEngine.Debug.Log("no more fish");
            return;
        }

        // random get one
        int randomIndex = UnityEngine.Random.Range(0, gachaPool.Count);
        GachaItem selectedItem = gachaPool[randomIndex];

        UnityEngine.Debug.Log("Get:  " + selectedItem.itemName);

        // -1
        selectedItem.count--;

        // remove the item if count=0
        if (selectedItem.count <= 0)
        {
            gachaPool.RemoveAt(randomIndex);
        }
    }
}
