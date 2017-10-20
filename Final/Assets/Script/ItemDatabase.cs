using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using System.IO;

public class ItemDatabase : MonoBehaviour {
//	private string conn;
	private List<Item> database;
	private JsonData itemData;
    /// <summary>
    /// Initialization
    /// </summary>
	public ItemDatabase() {
		database = new List<Item> ();
	}
    /// <summary>
    /// Building item database
    /// </summary>
	void Start()
	{
		itemData = JsonMapper.ToObject (File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
		ConstructItemDatabase ();
	}
    /// <summary>
    /// How to get an item
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
	public Item GetItemByID(int id) {
		for (int i = 0; i < database.Count; i++) {
			if (database [i].GetId () == id)
				return database [i];
		}
		return null;
	}
    /// <summary>
    /// Building item database
    /// </summary>
	public void ConstructItemDatabase() {
		for (int i = 0; i < itemData.Count; i++) {
			database.Add (new Item ((int)itemData [i] ["id"],(string)itemData [i] ["name"], (int)itemData [i] ["quantity"], 
				(bool)itemData[i]["stackable"],(string)itemData[i]["slug"]));
		}
	}
}
/// <summary>
/// Class for item
/// </summary>
public class Item {
	private int id;
	private string name;
	private int quantity;
	private String slug;
	private Sprite sprite;
	private bool stackable;
	public Item(int newId, string newName, int newQuantity, bool newStackable,String newSlug) {
		this.id = newId;
		this.name = newName;
		this.quantity = newQuantity;
		this.slug = newSlug;
		this.stackable = newStackable;
		this.sprite = Resources.Load<Sprite>("Item/" + this.slug);
	}
	public Item() {
		this.id = -1;
	}
	public Sprite GetSprite() {
		return this.sprite;
	}
	public void SetSprite(Sprite newSprite) {
		this.sprite = newSprite;
	}
	public int GetId() {
		return this.id;
	}
	public void SetId(int newId) {
		this.id = newId;
	}
	public string GetName() {
		return this.name;
	}
	public void SetName(string newName) {
		this.name = newName;
	}
	public bool GetStackable() {
		return this.stackable;
	}
	public void SetStackable(Boolean newStackable) {
		this.stackable = newStackable;
	}
	public int GetQuantity() {
		return this.quantity;
	}
	public void SetQuantity(int newQuantity) {
		this.quantity = newQuantity;
	}
	public void increaseQuantity(int amount) {
		this.quantity += amount;
	}
}
	
