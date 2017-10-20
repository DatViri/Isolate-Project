using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.UI;

public class BuildingFoodDatabase : MonoBehaviour {
	public Inventory inventory;
	private JsonData itemData;
	private FoodBuilding building;
	public Text foodStatus;
	private string full = "Full";
	private string empty = "Empty";
	private double respawnTime = 10.0;
	private bool foodEmpty = false;
	private List<FoodBuilding> buildingDatabase = new List<FoodBuilding> ();
	// Use this for initialization
	void Start () {
		foodStatus = GetComponent<BuildingFoodDatabase>().foodStatus;
		itemData = JsonMapper.ToObject (File.ReadAllText (Application.dataPath + "/StreamingAssets/FoodBuilding.json"));
		ConstructBuildingDatabase ();
		building = buildingDatabase [0];
		foodStatus.text = "Farm: " + full;
	}
    // Check whether farm is collected or not. If yes, count down time to respawn
	void Update() {
		if (foodEmpty == true) {
			foodStatus.text = "Farm: " + empty;
			Respawn ();
		}
	}
    // Creating the list of building levels and their attributes
	void ConstructBuildingDatabase() {
		for (int i = 0; i < itemData.Count; i++) {
			buildingDatabase.Add (new FoodBuilding ((int)itemData [i] ["level"], (int)itemData [i] ["food"],
				(int)itemData [i] ["upgrade need"] ["wood"], (int)itemData [i] ["upgrade need"] ["stone"],
				(int)itemData [i] ["upgrade need"] ["metal"]));
		}
	}
    // Collecting the farm
	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("FoodBuilding") && foodEmpty == false) {
			int amount = other.GetComponent<BuildingFoodDatabase>().building.GetFoodCollect();
			for (int i = 0; i < amount; i++) {
				inventory.AddItem (0);
			}
			foodEmpty = true;
		}
	}
    // Cooldown for farm
	void Respawn() {
		respawnTime -= Time.deltaTime;
		if (respawnTime <= 0) {
			foodEmpty = false;
			foodStatus.text = "Farm: " + full;
			respawnTime = 5;
		}
	}
    // Upgrading the farm
	void OnTriggerStay2D(Collider2D other) {
		if(other.gameObject.CompareTag("FoodBuilding") && Input.GetKeyDown("u") && other.GetComponent<BuildingFoodDatabase> ().building.GetLevel () < 4) {
			int wood = other.GetComponent<BuildingFoodDatabase>().building.GetWoodNeed();
			int stone = other.GetComponent<BuildingFoodDatabase> ().building.GetStoneNeed ();
			int metal = other.GetComponent<BuildingFoodDatabase> ().building.GetMetalNeed ();
			if (inventory.CheckIfEnoughItem (4, wood) && inventory.CheckIfEnoughItem (3, stone) && inventory.CheckIfEnoughItem (2,metal)) {
				inventory.DecreaseItem (4, wood);
				inventory.DecreaseItem (3, stone);
				inventory.DecreaseItem (2, metal);
				other.GetComponent<BuildingFoodDatabase> ().building = buildingDatabase [other.GetComponent<BuildingFoodDatabase> ().building.GetLevel () + 1];
				Debug.Log (other.GetComponent<BuildingFoodDatabase> ().building.GetLevel ());
			}
		}
	}
}
// Farm class
public class FoodBuilding {
	private int level;
	private int foodCollect;
	private int woodNeed;
	private int stoneNeed;
	private int metalNeed;
	public FoodBuilding(int level, int foodCollect, int woodNeed, int stoneNeed, int metalNeed) {
		this.level = level;
		this.foodCollect = foodCollect;
		this.woodNeed = woodNeed;
		this.stoneNeed = stoneNeed;
		this.metalNeed = metalNeed;
	}
	public int GetLevel() {
		return this.level;
	}
	public void SetLevel(int newLevel) {
		this.level = newLevel;
	}
	public int GetFoodCollect() {
		return this.foodCollect;
	}
	public void SetFoodCollect(int newFoodCollect) {
		this.foodCollect = newFoodCollect;
	}
	public int GetWoodNeed() {
		return this.woodNeed;
	}
	public void SetWoodNeed(int newWoodNeed) {
		this.level = newWoodNeed;
	}
	public int GetStoneNeed() {
		return this.stoneNeed;
	}
	public void SetStoneNeed(int newStoneNeed) {
		this.level = newStoneNeed;
	}
	public int GetMetalNeed() {
		return this.metalNeed;
	}
	public void SetMetalNeed(int newMetalNeed) {
		this.level = newMetalNeed;
	}
}