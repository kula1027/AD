using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_SkillSlot : MonoBehaviour {
	GameManager gameManager;

	private GameObject selected;
	private int selected_index;
	private UI_Control ui_control;

	private Transform[] btn;
	private Text skillName;
	private Text skillEffect;
	private Text skillExplain;

	public void Init_Skill () {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		ui_control = GameObject.Find ("UI Manager").GetComponent<UI_Control> ();
		selected = transform.Find ("Selected_Skill").gameObject;

		skillName = GameObject.Find ("SkillName").GetComponent<Text> ();
		skillEffect = GameObject.Find ("SkillEffect").GetComponent<Text> ();
		skillExplain = GameObject.Find ("SkillExplain").GetComponent<Text> ();

		Off ();

		btn = new Transform[5];
		for (int loop = 0; loop < 5; loop++) {
			btn[loop] = transform.GetChild(loop + 1);
		}
	}
	
	public void OnSkillClicked(int btnNum){
		if (!ui_control.IsAtTop) {
			gameManager.UseSkill (btnNum);
		}
		else {
			ToggleUI(btnNum);
		}
	}
	
	public void ToggleUI(int index){
		if (selected.activeSelf && index == selected_index) {
			Off ();
		} else {
			selected.SetActive (true);
			selected.transform.position = btn[index].position;
			selected_index = index;
			skillName.text = gameManager.GetSkillName(index);
			skillEffect.text = gameManager.GetSkillEffect(index);
			skillExplain.text = gameManager.GetSkillExplain(index);
			selected_index = index;
		}
	}

	public void OnSkillUse(){
		if (selected_index != -1) {
			gameManager.UseSkill (selected_index);
		}
	}

	private void SelectOff(){
		selected.SetActive (false);
		selected_index = -1;
	}
	private void TextOff(){
		skillName.text = "";
		skillEffect.text = "";
		skillExplain.text = "";
	}
	public void Off(){
		SelectOff ();
		TextOff ();
	}
}
