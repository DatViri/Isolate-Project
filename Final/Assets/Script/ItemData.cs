using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour {
	public Item item;
	public int amount;
    /// <summary>
    /// Show the amount in inventory
    /// </summary>
    /// <returns></returns>
	public int GetAmount() {
		return this.amount;
	}
	public void GetAmount(int newAmount) {
		this.amount = newAmount;
	}
}
