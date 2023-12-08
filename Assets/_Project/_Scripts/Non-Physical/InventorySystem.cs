using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace MagnetFishing
{
    public class InventorySystem : MonoBehaviour
    {
        public FishingRod fishingRod; // Reference to the FishingRod script
        public TMP_Text descriptionText;
        public List<Button> itemSlots;
        public GameObject inventoryPanel;
        public Button openInventory;
        private Dictionary<string, string> itemDescriptions = new Dictionary<string, string>();

        public Transform player;
        public Transform itemToCheck;
        public float checkDistance = 5.0f;
        public bool enableDistanceCheck = true;

        void Start()
        {
            openInventory.gameObject.SetActive(true);
            openInventory.onClick.AddListener(() => {
                ToggleInventory(); // toggles the inventory
            });
            inventoryPanel.SetActive(false);
            SetTextTransparency(descriptionText, 0);

            InitializeItemDescriptions();
            InitializeItemSlots();

            fishingRod = FindObjectOfType<FishingRod>();
            if (fishingRod == null)
            {
                UnityEngine.Debug.LogError("FishingRod not found in the scene.");
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Y) && IsPlayerInRange())
            {
                ToggleInventory();
            }

            if (inventoryPanel.activeSelf && !IsPlayerInRange())
            {
                inventoryPanel.SetActive(false);
            }
        }

        private bool IsPlayerInRange()
        {
            if (!enableDistanceCheck || itemToCheck == null || player == null)
                return true;

            return Vector3.Distance(player.position, itemToCheck.position) <= checkDistance;
        }

        private void ToggleInventory()
        {
            bool isActive = !inventoryPanel.activeSelf;
            inventoryPanel.SetActive(isActive);

            if (isActive)
            {
                UpdateInventoryDisplay();
                SetTextTransparency(descriptionText, 1);
            }
            else
            {
                SetTextTransparency(descriptionText, 0);
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
            // Initialize with predefined item descriptions
            itemDescriptions.Add("Rubber Boot", "Reuse and upcycle. If only you had a pair!");
            itemDescriptions.Add("Plastic Loofah", "Microplastics, yuck! If only there were a more natural alternative...");
            itemDescriptions.Add("Fish", "A REAL LIVE FISH! WOW!");
        }

        private void InitializeItemSlots()
        {
            // Initialize the item slots with transparency
            foreach (var slot in itemSlots)
            {
                SetSlotTransparency(slot, 0);
            }
        }

        void UpdateInventoryDisplay()
        {
            var caughtFishes = fishingRod.GetCaughtFishes(); // Using caught fish list from FishingRod
            var sortedItems = caughtFishes.ToList();
            sortedItems.Sort((pair1, pair2) => pair1.Key.CompareTo(pair2.Key));

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
                    textComponent.text = item.Key + " x" + item.Value; // Display item name and count
                    SetSlotTransparency(slot, 1);

                    string itemName = item.Key;
                    slot.onClick.AddListener(() => {
                        UnityEngine.Debug.Log("Button clicked for item: " + itemName);
                        ShowDescription(itemName); // Show description based on the item name
                    });
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
            if (itemDescriptions.TryGetValue(itemName, out string description))
            {
                descriptionText.text = description;
            }
            else
            {
                descriptionText.text = "No description available.";
            }
        }
    }
}
