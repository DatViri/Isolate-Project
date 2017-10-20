using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.UI;

public class BuildingWaterDatabase : MonoBehaviour {
	public Inventory inventory;
	private JsonData itemData;
	private WaterBuilding building;
	public Text waterStatus;
	private string full = "Full";
	private string empty = "Empty";
	private double respawnTime = 10.0;
	private bool waterEmpty = false;
	private List<WaterBuilding> buildingDatabase = new List<WaterBuilding> ();
	// Use this for initialization
	void Start () {
		waterStatus = GetComponent<BuildingWaterDatabase>().waterStatus;
		itemData = JsonMapper.ToObject (File.ReadAllText (Application.dataPath + "/StreamingAssets/WaterBuilding.json"));
		ConstructBuildingDatabase ();
		building = buildingDatabase [0];
		waterStatus.text = "Well: " + full;
	}
    // Check whether well is collected or not
	void Update() {
		if (waterEmpty == true) {
			waterStatus.text = "Well: " + empty;
			Respawn ();
		}
	}
    // Create a list of building levels and their attributes
	void ConstructBuildingDatabase() {
		for (int i = 0; i < itemData.Count; i++) {
			buildingDatabase.Add (new WaterBuilding ((int)itemData [i] ["level"], (int)itemData [i] ["water"],
				(int)itemData [i] ["upgrade need"] ["wood"], (int)itemData [i] ["upgrade need"] ["stone"],
				(int)itemData [i] ["upgrade need"] ["metal"]));
		}
	}
    // collecting well
	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("WaterBuilding") && waterEmpty == false) {
			int amount = other.GetComponent<BuildingWaterDatabase>().building.GetWaterCollect();
			for (int i = 0; i < amount; i++) {
				inventory.AddItem (1);
			}
			waterEmpty = true;
		}
	}
    // Cooldown for well
	void Respawn() {
		respawnTime -= Time.deltaTime;
		if (respawnTime <= 0) {
			waterEmpty = false;
			waterStatus.text = "Well: " + full;
			respawnTime = 5;
		}
	}
    //Upgrading the well
	void OnTriggerStay2D(Collider2D other) {
		if(other.gameObject.CompareTag("WaterBuilding") && Input.GetKeyDown("u") && other.GetComponent<BuildingWaterDatabase> ().building.GetLevel () < 4) {
			int wood = other.GetComponent<BuildingWaterDatabase>().building.GetWoodNeed();
			int stone = other.GetComponent<BuildingWaterDatabase> ().building.GetStoneNeed ();
			int metal = other.GetComponent<BuildingWaterDatabase> ().building.GetMetalNeed ();
			if (inventory.CheckIfEnoughItem (4, wood) && inventory.CheckIfEnoughItem (3, stone) && inventory.CheckIfEnoughItem (2,metal)) {
				inventory.DecreaseItem (4, wood);
				inventory.DecreaseItem (3, stone);
				inventory.DecreaseItem (2, metal);
				other.GetComponent<BuildingWaterDatabase> ().building = buildingDatabase [other.GetComponent<BuildingWaterDatabase> ().building.GetLevel () + 1];
				Debug.Log (other.GetComponent<BuildingWaterDatabase> ().building.GetLevel ());
			}
		}
	}
}
// Class for well
public class WaterBuilding {
	private int level;
	private int waterCollect;
	private int woodNeed;
	private int stoneNeed;
	private int metalNeed;
	public WaterBuilding(int level, int waterCollect, int woodNeed, int stoneNeed, int metalNeed) {
		this.level = level;
		this.waterCollect = waterCollect;
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
	public int GetWaterCollect() {
		return this.waterCollect;
	}
	public void SetWaterCollect(int newWaterCollect) {
		this.waterCollect = newWaterCollect;
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

