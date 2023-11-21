using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GachaSystem : MonoBehaviour
{
    [System.Serializable]
    public class GachaItem
    {
        public string itemName;
        public int count; // counts of items
    }

    public List<GachaItem> gachaPool = new List<GachaItem>();
    private Dictionary<string, int> drawnItems = new Dictionary<string, int>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            DrawItem();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            DisplayDrawnItems();
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

        // Fishing
        if (Fishing())
        {
            UnityEngine.Debug.Log("Get: " + selectedItem.itemName);
            AddToDrawnItems(selectedItem.itemName);
            selectedItem.count--;
            if (selectedItem.count <= 0)
            {
                gachaPool.RemoveAt(randomIndex);
            }
        }
        else
        {
            UnityEngine.Debug.Log("Oops!");
        }
    }

    private bool Fishing()
    {
        // random for now
        return UnityEngine.Random.value > 0.5f;
    }

    private void AddToDrawnItems(string itemName)
    {
        if (drawnItems.ContainsKey(itemName))
        {
            drawnItems[itemName]++;
        }
        else
        {
            drawnItems.Add(itemName, 1);
        }
    }

    private void DisplayDrawnItems()
    {
        var sortedItems = drawnItems.OrderBy(item => item.Key);

        foreach (var item in drawnItems)
        {
            UnityEngine.Debug.Log(item.Key + " * " + item.Value);
        }
    }
}
