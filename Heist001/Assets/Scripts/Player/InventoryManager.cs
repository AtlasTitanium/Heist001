using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Image[] inventorySlots;
    public Image[] inventoryImages;

    public Color idleSlotColor;
    public Color chosenSlotColor;

    public Sprite basicSprite;

    public void SetInventoryImage(int imageNumber, Sprite image) {
        inventoryImages[imageNumber].sprite = image;
    }

    public void RemoveInventoryImage(int imageNumber) {
        inventoryImages[imageNumber].sprite = basicSprite;
    }

    public void SetCurrentItemSlot(int imageNumber) {
        for (int i = 0; i < inventorySlots.Length; i++) {
            if(imageNumber == i) {
                inventorySlots[i].color = chosenSlotColor;
            } else {
                inventorySlots[i].color = idleSlotColor;
            }
        }
    }
}
