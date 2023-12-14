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
            itemDescriptions.Add("Plastic Toothbrush", "This invention was critical for improving human’s oral hygiene. However, it has also led to an incredible amount of plastic waste to landfills. Every part of the toothbrush, including the thin bristles, are made of plastics.");
            itemDescriptions.Add("Bamboo Toothbrush", "There is a wide variety of bamboo and compostable toothbrushes. It is important to know what each component is made of, as sometimes the bristles may be made of plastic. Proper disposal, such as industrial composting, may be required to allow the toothbrush to biodegrade. But if done correctly, your old toothbrush will be turned into fuel for new plants to grow!");
            itemDescriptions.Add("Styrofoam To-Go Box", "Most areas do not offer recycling for extruded polystyrene foam, the generic name for Styrofoam, because of its highly difficult and expensive process. Furthermore, when Styrofoam is heated, it can leach toxic chemicals into food and waterways.");
            itemDescriptions.Add("Metal Food Tin", "Reusable food containers made of metal or glass are infinitely recyclable. But because these are reusable, you are reducing waste even more effectively. When purchasing a container, try to look for ones that are entirely made of glass or metal, and avoid ones that include plastic components.");
            itemDescriptions.Add("Fish", "A REAL LIVE FISH! WOW! Over time, if we help clean up nature, it will find ways to heal itself. This little guy might have microplastics in him from the long-term pollution of this lake, but with your help the lake will get better and better with time. You've made your grandpa proud!");
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
            var sequentiallyCaughtItems = caughtFishes.ToList();
            //sortedItems.Sort((pair1, pair2) => pair1.Key.CompareTo(pair2.Key));

            foreach (var slot in itemSlots)
            {
                slot.onClick.RemoveAllListeners();
            }

            for (int i = 0; i < itemSlots.Count; i++)
            {
                var slot = itemSlots[i];
                var textComponent = slot.GetComponentInChildren<TMP_Text>();

                if (i < sequentiallyCaughtItems.Count)
                {
                    var item = sequentiallyCaughtItems[i];
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
