using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

	public Player player;
	public DictionaryScenario dictionaryScenario;
	public List<Scenario> timeline = new List<Scenario>();

	public int currentFrame = 0;

	void Start(){
		//On lance le jeu, on charge le premier element scénaristique
		LoadScenario ( 0 );

		//Histoire dans le cas où le player a comme karma :
		Debug.Log ("Le karma du player = " + player.karma);

		//Le joueur avance vers la maison et ....
		//Décide d'ouvrir la fenetre
		if (LoadScenario (1, player.karma)) {
			//Il ouvre la fenetre
			//Et tombe nez à nez avec zombie et meurs
			LoadScenario (3, player.karma);
		} else {
			//Il n'ouvre pas la fenetre mais décider d'ouvre la porte
			if (LoadScenario (2, player.karma)) {
				//Il ouvre la porte
				//Condition : Si son karma est < 75, le player va déceder
				if (!LoadScenario (4, player.karma)) {
					//Si son karma >= 75
					if (LoadScenario (5, player.karma)) {
						//Rien ne se passe, mission terminée
						Debug.Log ("Félicitations, vous etes dans la maison !");
					}
				}
			}
		}

	}

	public bool LoadScenario(int scenarioID, int qtyKarma = 0){
		//Controler l'id du scénario actuel
		//Regarder les scénarios disponibles
		//On charge le scénario en fonction de la situation

		//Si scenarioID > -1 alors je vais chercher si ID existe dans le dictionaire
		if (scenarioID > -1) {
			if(dictionaryScenario.scenarios[scenarioID] != null) {
				
				//Debug.Log("Oui, le scénario " + scenarioID + " existe");

				//Initialiser une nouvelle instance de Scenario avec les données du dictionnaire
				GameObject gameObjectScenario = new GameObject();
				Scenario scenario = gameObjectScenario.AddComponent<Scenario>();
				scenario.dictionaryEntryID = scenarioID;
				scenario.name = dictionaryScenario.scenarios[scenarioID].displayName;
				scenario.displayDescription = dictionaryScenario.scenarios[scenarioID].displayDescription;
				scenario.inputScenarioID = dictionaryScenario.scenarios[scenarioID].inputScenarioID;
				scenario.karmaMin = dictionaryScenario.scenarios[scenarioID].karmaMin;
				scenario.karmaMax = dictionaryScenario.scenarios[scenarioID].karmaMax;

				//Si le scénario n'existe pas dans ma timeline
				if(!timeline.Contains(scenario)){
					//Controler si le inputScenarioID est bien l'ID du scenario actualement exécuté dans la timeline
					if(timeline.Count == 0 || scenario.inputScenarioID == timeline[timeline.Count-1].dictionaryEntryID){
						//On va controler la quantité de karma necessaire, pour le scénario
						if(scenario.karmaMax == 0 || (
							scenario.karmaMax  != 0 && qtyKarma >= scenario.karmaMin && qtyKarma < scenario.karmaMax
						)){
							timeline.Add(scenario);
							Debug.Log("Le scénario " + scenarioID + " a été rajouté à la timeline : " + scenario.displayDescription);
							return true;
						} else {
							Debug.Log ("Vous n'avez le karma nécessaire pour executer le scenario : " + scenarioID + " > " +scenario.displayDescription);
						}
					} else {
						Debug.Log("Non désolé, le " + scenarioID + " n'est pas la suite du scénario actuel : " + scenario.displayDescription);
					}
				} else {
					Debug.Log("Han han... Le scénario " + scenarioID + " a déjà été rajouté à la timeline : " + scenario.displayDescription);
				}

			} else {
				Debug.Log("Tu te fous de ma gueule, le scénario " + scenarioID + " n'existe pas");
			}
		}
		
		return false;
	}

	void Update(){
		currentFrame++;
	}

}
