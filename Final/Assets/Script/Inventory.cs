using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
	GameObject inventoryPanel;
	GameObject slotPanel;
	ItemDatabase database;
	public GameObject inventorySlot;
	public GameObject inventoryItem;

	public List<Item> items = new List<Item> ();
	public List<GameObject> slots = new List<GameObject>();
	int slotAmount;
    /// <summary>
    /// Set up the inventory
    /// </summary>
	void Start() {
		database = GetComponent<ItemDatabase> ();
		slotAmount = 5;
		inventoryPanel = GameObject.Find ("Inventory Panel");
		slotPanel = inventoryPanel.transform.FindChild ("Slot Panel").gameObject;
		for (int i = 0; i < slotAmount; i++) {
			items.Add (new Item ());
			slots.Add (Instantiate (inventorySlot));
			slots [i].transform.SetParent (slotPanel.transform);
		}
		AddItem (0);
		AddItem (1);
		AddItem (2);
		AddItem (3);
		AddItem (4);
	}
    /// <summary>
    /// Adding item to inventory
    /// </summary>
    /// <param name="id"></param>
	public void AddItem(int id)
	{
		Item itemToAdd = database.GetItemByID (id);
		if (itemToAdd.GetStackable () && CheckIfItemInInventory (itemToAdd)) {
			for (int i = 0; i < items.Count; i++) {
				if (items [i].GetId () == id) {
					ItemData data = slots [i].transform.GetChild (0).GetComponent<ItemData> ();
					data.amount++;
					data.transform.GetChild (0).GetComponent<Text> ().text = data.GetAmount ().ToString ();
					break;
				}
			}
		} else {
			for (int i = 0; i < items.Count; i++) {
				if (items [i].GetId () == -1) {
					items [i] = itemToAdd;
					GameObject itemObj = Instantiate (inventoryItem);
					itemObj.transform.SetParent (slots [i].transform);
					itemObj.transform.position = Vector2.zero;
					itemObj.GetComponent<Image> ().sprite = itemToAdd.GetSprite ();
					itemObj.name = itemToAdd.GetName ();
					break;
				}
			}
		}
	}
    /// <summary>
    /// override method
    /// </summary>
    /// <param name="id"></param>
    /// <param name="amount"></param>
	public void AddItem(int id, int amount)
	{
		Item itemToAdd = database.GetItemByID (id);
		if (itemToAdd.GetStackable () && CheckIfItemInInventory (itemToAdd)) {
			for (int i = 0; i < items.Count; i++) {
				if (items [i].GetId () == id) {
					ItemData data = slots [i].transform.GetChild (0).GetComponent<ItemData> ();
					for(int u = 0; u < amount; u++) {
						data.amount++;
					}
					data.transform.GetChild (0).GetComponent<Text> ().text = data.GetAmount ().ToString ();
					break;
				}
			}
		}else {
			for (int i = 0; i < items.Count; i++) {
				if (items [i].GetId () == -1) {
					items [i] = itemToAdd;
					GameObject itemObj = Instantiate (inventoryItem);
					itemObj.transform.SetParent (slots [i].transform);
					itemObj.transform.position = Vector2.zero;
					itemObj.GetComponent<Image> ().sprite = itemToAdd.GetSprite ();
					itemObj.name = itemToAdd.GetName ();
					break;
				}
			}
		}
	}
    /// <summary>
    /// Decrease item to inventory
    /// </summary>
    /// <param name="id"></param>
	public void DecreaseItem(int id)
	{
		Item itemToAdd = database.GetItemByID (id);
		if (itemToAdd.GetStackable () && CheckIfItemInInventory (itemToAdd)) {
			for (int i = 0; i < items.Count; i++) {
				if (items [i].GetId () == id) {
					ItemData data = slots [i].transform.GetChild (0).GetComponent<ItemData> ();
					if (data.amount > 0) {
						data.amount--;
						data.transform.GetChild (0).GetComponent<Text> ().text = data.GetAmount ().ToString ();
						break;
					}
				}
			}
		}else {
			for (int i = 0; i < items.Count; i++) {
				if (items [i].GetId () == -1) {
					items [i] = itemToAdd;
					GameObject itemObj = Instantiate (inventoryItem);
					itemObj.transform.SetParent (slots [i].transform);
					itemObj.transform.position = Vector2.zero;
					itemObj.GetComponent<Image> ().sprite = itemToAdd.GetSprite ();
					itemObj.name = itemToAdd.GetName ();
					break;
				}
			}
		}
	}
    /// <summary>
    /// Override method to decrease item
    /// </summary>
    /// <param name="id"></param>
    /// <param name="amount"></param>
	public void DecreaseItem(int id, int amount)
	{
		Item itemToAdd = database.GetItemByID (id);
		if (itemToAdd.GetStackable () && CheckIfItemInInventory (itemToAdd)) {
			for (int i = 0; i < items.Count; i++) {
				if (items [i].GetId () == id) {
					ItemData data = slots [i].transform.GetChild (0).GetComponent<ItemData> ();
					if (data.amount > amount) {
						data.amount = data.amount - amount;
						data.transform.GetChild (0).GetComponent<Text> ().text = data.GetAmount ().ToString ();
					}
					break;
				}
			}
		}else {
			for (int i = 0; i < items.Count; i++) {
				if (items [i].GetId () == -1) {
					items [i] = itemToAdd;
					GameObject itemObj = Instantiate (inventoryItem);
					itemObj.transform.SetParent (slots [i].transform);
					itemObj.transform.position = Vector2.zero;
					itemObj.GetComponent<Image> ().sprite = itemToAdd.GetSprite ();
					itemObj.name = itemToAdd.GetName ();
					break;
				}
			}
		}
	}
    /// <summary>
    /// CheckIfItemInInventory
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
	public bool CheckIfItemInInventory(Item item) {
		for(int i = 0; i < items.Count; i++) {
			if(items[i].GetId() == item.GetId())
				return true;
		}
		return false;
	}
    /// <summary>
    /// Check If Enough Item
    /// </summary>
    /// <param name="id"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
	public bool CheckIfEnoughItem(int id, int amount) {
		Item itemToAdd = database.GetItemByID (id);
		if (itemToAdd.GetStackable () && CheckIfItemInInventory (itemToAdd)) {
			for (int i = 0; i < items.Count; i++) {
				if (items [i].GetId () == id) {
					ItemData data = slots [i].transform.GetChild (0).GetComponent<ItemData> ();
					if (data.amount >= amount) {
						return true;
					}
				}
			}
		}
		return false;
	}
    /// <summary>
    /// Get Amount Of Item
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
	public int GetAmountOfItem(int id) {
		Item itemToCheck = database.GetItemByID (id);
		if (itemToCheck.GetStackable () && CheckIfItemInInventory (itemToCheck)) {
			for (int i = 0; i < items.Count; i++) {
				if (items [i].GetId () == id) {
					ItemData data = slots [i].transform.GetChild (0).GetComponent<ItemData> ();
					return data.amount;
				}
			}
		}
		return 0;
	}
}

