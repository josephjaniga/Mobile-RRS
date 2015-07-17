using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum ModalLayouts{
	OneColumnModal = 1,
	TwoColumnModal = 2
}

public class GUIManager : MonoBehaviour {

	// level Loop
	public GameObject restartButton;
	public GameObject victoryButton;
	public GameObject objectivesPanel;
	public GameObject oneColumnModal;
	public GameObject twoColumnModal;

	public void displayLevelModal(){
		string descriptiontext = "Survive 1 Trigger Pull with " +_.stateMachine.liveBulletsInCylinder_objective+ " Live Bullet" + (_.stateMachine.liveBulletsInCylinder_objective > 1 ? "s" : "");
		string buttonText = "I  GOT  THIS!";
		Sprite img = null;
		// img = Resources.LoadAll<Sprite>("Art/Sprites/deer-hunter")[0];
		customModal(descriptiontext, buttonText, img, ModalLayouts.OneColumnModal);
	}
	
	public void customModal(string DescriptionText, string ButtonText, Sprite Image = null, ModalLayouts ModalLayout = ModalLayouts.OneColumnModal){
		_.stateMachine.locked = true;
		
		if ( ModalLayout == ModalLayouts.OneColumnModal ){
			
			oneColumnModal.SetActive(true);
			
			GameObject image = oneColumnModal.transform.FindChild("Panel").FindChild("Image").gameObject;
			GameObject descriptionText = oneColumnModal.transform.FindChild("Panel").FindChild("Text").gameObject;
			GameObject buttonText = oneColumnModal.transform.FindChild("Panel").FindChild("Button").FindChild("ButtonText").gameObject;
			
			if ( Image != null ){
				image.SetActive(true);
				image.GetComponent<Image>().sprite = Image;
			} else {
				image.SetActive(false);
			}
			
			descriptionText.GetComponent<Text>().text = DescriptionText;
			buttonText.GetComponent<Text>().text = ButtonText;
		}
		
		if ( ModalLayout == ModalLayouts.TwoColumnModal ){
			
			twoColumnModal.SetActive(true);
			
			GameObject image = twoColumnModal.transform.FindChild("Panel").FindChild("Image").gameObject;
			GameObject descriptionText = twoColumnModal.transform.FindChild("Panel").FindChild("Text").gameObject;
			GameObject buttonText = twoColumnModal.transform.FindChild("Panel").FindChild("Button").FindChild("ButtonText").gameObject;
			
			if ( Image != null ){
				image.SetActive(true);
				image.GetComponent<Image>().sprite = Image;
			} else {
				image.SetActive(false);
			}
			
			descriptionText.GetComponent<Text>().text = DescriptionText;
			buttonText.GetComponent<Text>().text = ButtonText;
		}
	}

	public void deactivateAllModals(){
		oneColumnModal.SetActive(false);
		twoColumnModal.SetActive(false);
		_.stateMachine.locked = false;
	}

}
