using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DictionaryScenario : MonoBehaviour {
	public List<DictionaryScenarioEntry> scenarios = new List<DictionaryScenarioEntry>();
}

[System.Serializable]
public class DictionaryScenarioEntry {
	public string displayName = "UnknownScenario";
	public string displayDescription = "Unknown scenario";
	public int inputScenarioID = -1;
	public int karmaMin = 0;
	public int karmaMax = 0;
}
