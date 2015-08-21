using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Control : MonoBehaviour {
	public GameManager gameManager;
	private Player player;
	public void SetPlayer(Player player_){
		player = player_;
	}
	public Player GetPlayer(){
		return player;
	}

	private GameObject slot_drink;
	private GameObject slot_item;
	private GameObject slot_skill;
	private GameObject slot_status;

	private RectTransform rtf_panel_menu;
	private static UI_Log ui_log;
	private Text ui_level;
	private Text ui_gold;

	private float bottomSlotLength;

	private bool itemDragLock;
	private bool isAtTop;

	void Start(){
		bottomSlotLength = GameObject.Find ("ParamY_Slot").transform.position.y;
		ui_gold = GameObject.Find ("GoldText").GetComponent<Text> ();

		SlotButtonInit ();
		InGameMenuInit ();

		ui_log = GameObject.Find ("Panel_Log").GetComponent<UI_Log> ();
		ui_level = GameObject.Find ("Icon_Level").transform.FindChild ("Text").GetComponent<Text>();

		itemDragLock = true;
		isAtTop = false;
	}

	public static void AddLog(string str_){
		ui_log.AddLogText (str_);
	}

	public void AddItem(Item item, int index){
		slot_item.GetComponent<UI_SlotHoldingItem> ().AddToSlot (item.GetItemCode(), item.GetSprite(), index);
	}
	public void AddDrink(Drink drink, int index){
		slot_drink.GetComponent<UI_SlotHoldingDrink> ().AddToSlot (drink.GetDrinkCode(), drink.GetSprite(), index);
	}

	public void SetGold (int amount){
		ui_gold.text = amount.ToString ();
	}

	public void LevelUp(){
		int tempL = System.Convert.ToInt32(ui_level.text); 
		tempL++;
		ui_level.text = tempL.ToString ();
	}

	#region Side Btns
	public void OnInvestigateBtnClicked(){
		ui_log.AddLogText ("조사하기");
	}

	public void OnHoldBtnClicked(){
		gameManager.HoldTurn ();
		ui_log.AddLogText (gameManager.playerScript.name + "은(는) 한턴 쉬었다.");
	}
	#endregion

	#region In Game Menu Buttons
	private void InGameMenuInit (){
		rtf_panel_menu = GameObject.Find ("Panel_Menu").GetComponent<RectTransform> ();
	}

	public void onButtonMenuClicked(){
		rtf_panel_menu.anchoredPosition = new Vector2 (0, 0);
	}

	public void onButtonContinueClicked(){
		rtf_panel_menu.anchoredPosition = new Vector2 (5000, 0);
	}

	public void onButtonOptionClicked(){
		//todo
	}

	public void onButtonToMainClicked(){
		//todo
	}

	public void onButtonSaveExitClicked(){
		//saving method
		Application.Quit ();
	}

	#endregion

	#region Input Buttons
	public void onButtonWClicked(){
		if (gameManager.player.GetComponent<ATTACK> ().attackable (Direction.LEFT)) {

			gameManager.PlayerAttack (Direction.LEFT);
		} else {
			gameManager.PlayerMove (Direction.LEFT);
		}
	}

	public void onButtonEClicked(){
		if (gameManager.player.GetComponent<ATTACK> ().attackable (Direction.RIGHT)) {
			//Debug.Log ("attack!");
			gameManager.PlayerAttack (Direction.RIGHT);
		} else {
			gameManager.PlayerMove (Direction.RIGHT);
		}
	}

	public void onButtonNClicked(){
		if (gameManager.player.GetComponent<ATTACK> ().attackable (Direction.UP)) {
			//Debug.Log ("attack!");
			gameManager.PlayerAttack (Direction.UP);
		} else {
			gameManager.PlayerMove (Direction.UP);
		}
	}

	public void onButtonSClicked(){
		if (gameManager.player.GetComponent<ATTACK> ().attackable (Direction.DOWN)) {
			//Debug.Log ("attack!");
			gameManager.PlayerAttack (Direction.DOWN);
		} else {
			gameManager.PlayerMove (Direction.DOWN);
		}
	}

	public void onButtonSEClicked(){
		if (gameManager.player.GetComponent<ATTACK> ().attackable (Direction.RIGHTDOWN)) {
			//Debug.Log ("attack!");
			gameManager.PlayerAttack (Direction.RIGHTDOWN);
		} else {
			//Debug.Log ("move!");
			gameManager.PlayerMove (Direction.RIGHTDOWN);
		}
	}

	public void onButtonSWClicked(){
		if (gameManager.player.GetComponent<ATTACK> ().attackable (Direction.LEFTDOWN)) {
			//Debug.Log ("attack!");
			gameManager.PlayerAttack (Direction.LEFTDOWN);
		} else {
			//Debug.Log ("move!");
			gameManager.PlayerMove (Direction.LEFTDOWN);
		}
	}

	public void onButtonNWClicked(){
		if (gameManager.player.GetComponent<ATTACK> ().attackable (Direction.LEFTUP)) {
			//Debug.Log ("attack!");
			gameManager.PlayerAttack (Direction.LEFTUP);
		} else {
			//Debug.Log ("move!");
			gameManager.PlayerMove (Direction.LEFTUP);
		}
	}

	public void onButtonNEClicked(){
		if (gameManager.player.GetComponent<ATTACK> ().attackable (Direction.RIGHTUP)) {
			//Debug.Log ("attack!");
			gameManager.PlayerAttack (Direction.RIGHTUP);
		} else {
			//Debug.Log ("move!");
			gameManager.PlayerMove (Direction.RIGHTUP);
		}
	}

	#endregion


	#region Slot Buttons
	private void SlotButtonInit(){
		slot_drink = GameObject.Find ("Slot_Drink");
		slot_drink.GetComponent<UI_SlotHoldingDrink> ().Init_Drink ();
		slot_item = GameObject.Find ("Slot_Item");
		slot_item.GetComponent<UI_SlotHoldingItem> ().Init_Item ();
		slot_skill = GameObject.Find ("Slot_Skill");
		slot_skill.GetComponent<UI_SkillSlot> ().Init_Skill ();
		slot_status = GameObject.Find ("Slot_Status");
		
		slot_item.SetActive (false);
		slot_skill.SetActive (false);
		slot_status.SetActive (false); 
	}

	public void onDrinkButtonDown(){
		slot_drink.SetActive (true);
		slot_item.SetActive (false);
		slot_skill.SetActive (false);
		slot_status.SetActive (false);

		GameObject sel = GameObject.Find ("Selected_Drink");
		if (sel != null)
			slot_drink.GetComponent<UI_Slot> ().SelectOff ();
	}

	public void onItemButtonDown(){
		slot_drink.SetActive (false);
		slot_item.SetActive (true);
		slot_skill.SetActive (false);
		slot_status.SetActive (false);

		GameObject sel = GameObject.Find ("Selected_Item");
		if (sel != null)
			slot_item.GetComponent<UI_Slot> ().SelectOff ();
	}

	public void onSkillButtonDown(){
		slot_drink.SetActive (false);
		slot_item.SetActive (false);
		slot_skill.SetActive (true);
		slot_status.SetActive (false);

		GameObject sel = GameObject.Find ("Selected_Skill");

		if (sel != null) {
			slot_skill.GetComponent<UI_SkillSlot> ().Off ();
		}
	}

	public void onStatusButtonDown(){
		slot_drink.SetActive (false);
		slot_item.SetActive (false);
		slot_skill.SetActive (false);
		slot_status.SetActive (true);
	}

	#endregion

	public bool ItemDragLock {
		get {
			return itemDragLock;
		}
		set {
			itemDragLock = value;
		}
	}

	public bool IsAtTop {
		get {
			return isAtTop;
		}
		set {
			isAtTop = value;
		}
	}

	public GameObject Slot_drink {
		get {
			return slot_drink;
		}
	}

	public GameObject Slot_item {
		get {
			return slot_item;
		}
	}

	public float BottomSlotLength {
		get {
			return bottomSlotLength;
		}
	}
}
