using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;



namespace MagnetFishing
{

    public class InventorySystem : MonoBehaviour
    {
        public GachaSystem gachaSystem; 
        public TMP_Text descriptionText; 
        public List<Button> itemSlots; 
        public GameObject inventoryPanel; 
        private Dictionary<string, string> itemDescriptions = new Dictionary<string, string>(); 

        void Start()
        {
            inventoryPanel.SetActive(false);
            SetTextTransparency(descriptionText, 0); 

            // initialize
            InitializeItemDescriptions();
            InitializeItemSlots();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                // toggle activity
                inventoryPanel.SetActive(!inventoryPanel.activeSelf);

                // toggle transparent
                if (inventoryPanel.activeSelf)
                {
                    UpdateInventoryDisplay();
                    SetTextTransparency(descriptionText, 1); 
                }
                else
                {
                    SetTextTransparency(descriptionText, 0); 
                }
            }
        }

        private void SetTextTransparency(TMP_Text text, float alpha)
        {
            Color color = text.color;
            color.a = alpha;
            text.color = color;
        }

        private void InitializeItemDescriptions()
        {
            // ADD DESCRIPTION HERE!
            itemDescriptions.Add("A", "This is A");
            itemDescriptions.Add("B", "This is B");
            itemDescriptions.Add("C", "This is C");
        }

        private void InitializeItemSlots()
        {
            foreach (var slot in itemSlots)
            {
                SetSlotTransparency(slot, 0); 
            }
        }


        void UpdateInventoryDisplay()
        {
            var drawnItems = gachaSystem.GetDrawnItems(); 
            var sortedItems = drawnItems.ToList(); 
            sortedItems.Sort((pair1, pair2) => pair1.Key.CompareTo(pair2.Key)); // ordering

            foreach (var slot in itemSlots)
            {
                slot.onClick.RemoveAllListeners();
            }

            for (int i = 0; i < itemSlots.Count; i++)
            {
                var slot = itemSlots[i];
                var textComponent = slot.GetComponentInChildren<TMP_Text>();

                if (i < sortedItems.Count)
                {
                    var item = sortedItems[i];
                    textComponent.text = item.Key;
                    SetSlotTransparency(slot, 1);

                    // add click event
                    string itemName = item.Key;
                    slot.onClick.AddListener(() => {
                        UnityEngine.Debug.Log("Button clicked for item: " + itemName);
                        ShowDescription(itemName);
                    });

                    UnityEngine.Debug.Log("Added listener for item: " + itemName);
                }
                else
                {
                    textComponent.text = "";
                    SetSlotTransparency(slot, 0); 
                }
            }
        }



        private void SetSlotTransparency(Button slot, float alpha)
        {
            var colors = slot.colors;
            colors.normalColor = new Color(1, 1, 1, alpha);
            slot.colors = colors;

            var textComponent = slot.GetComponentInChildren<TMP_Text>();
            if (textComponent != null)
            {
                SetTextTransparency(textComponent, alpha);
            }
        }

        public void ShowDescription(string itemName)
        {
            UnityEngine.Debug.Log("Description");
            if (itemDescriptions.TryGetValue(itemName, out string description))
            {
                descriptionText.text = description;
            }
            else
            {
                descriptionText.text = "Null";
            }
        }
    }


}
