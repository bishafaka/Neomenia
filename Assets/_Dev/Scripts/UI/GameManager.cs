using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	[Serializable]
	public class StringVariable
	{
		public string key;
		public string value;
	}
	[Serializable]
	public class IntVariable
	{
		public string key;
		public int value;
	}
	[Serializable]
	public class FloatVariable
	{
		public string key;
		public float value;
	}
	[Serializable]
	public class BoolVariable
	{
		public string key;
		public bool value;
	}
	[Header("String Variables")]
	public List<StringVariable> stringVariables=new();
	[Header("Int Variables")]
	public List<IntVariable> intVariables=new();
	[Header("Float Variables")]
	public List<FloatVariable> floatVariables=new();
	[Header("Bool Variables")]
	public List<BoolVariable> boolVariables=new();
	[Header("JSON")]
	[SerializeField] string fileName="gameManager.json";
	string SavePath=>System.IO.Path.Combine(Application.persistentDataPath, fileName);
	[Serializable]
	class VariablesData
	{
		public List<StringVariable> strings=new List<StringVariable>();
		public List<IntVariable> ints=new List<IntVariable>();
		public List<FloatVariable> floats=new List<FloatVariable>();
		public List<BoolVariable> bools=new List<BoolVariable>();
	}
	void Awake()
	{
		if(Instance!=null && Instance!=this)
		{
			Destroy(gameObject);
			return;
		}
		Instance=this;
		DontDestroyOnLoad(gameObject);
		Load();
		Debug.Log(Application.persistentDataPath);
	}
	public void SetPlayerName(string value)
	{
		SetString("player-name", value);
	}
	public void SetPronouns(int value)
	{
		string pronounSubject;
		string pronounObject;
		string pronounPossessive;
		string pronounReflexive;
		switch(value)
		{
			case 1:
				pronounSubject="he";
				pronounObject="him";
				pronounPossessive="his";
				pronounReflexive="himself";
				break;
			case 2:
				pronounSubject="she";
				pronounObject="her";
				pronounPossessive="her";
				pronounReflexive="herself";
				break;
			default:
				pronounSubject="they";
				pronounObject="them";
				pronounPossessive="their";
				pronounReflexive="themselves";
				break;
		}
		SetString("pronoun-subject", pronounSubject);
		SetString("pronoun-object", pronounObject);
		SetString("pronoun-possessive", pronounPossessive);
		SetString("pronoun-reflexive", pronounReflexive);
	}
	//SET VARIABLES
	public void SetString(string key, string value)
	{
		StringVariable variable=stringVariables.Find(v => v.key==key);
		if(variable!=null)
			variable.value=value;
		else
			stringVariables.Add(new StringVariable{key=key, value=value});
		Save();
	}
	public void SetInt(string key, int value)
	{
		IntVariable variable=intVariables.Find(v => v.key==key);
		if(variable!=null)
			variable.value=value;
		else
			intVariables.Add(new IntVariable{key=key, value=value});
		Save();
	}
	public void SetFloat(string key, float value)
	{
		FloatVariable variable=floatVariables.Find(v => v.key==key);
		if(variable!=null)
			variable.value=value;
		else
			floatVariables.Add(new FloatVariable{key=key, value=value});
		Save();
	}
	public void SetBool(string key, bool value)
	{
		BoolVariable variable=boolVariables.Find(v => v.key==key);
		if(variable!=null)
			variable.value=value;
		else
			boolVariables.Add(new BoolVariable{key=key, value=value});
		Save();
	}
	//GET VARIABLES
	public string GetString(string key)
	{
		StringVariable variable=stringVariables.Find(v => v.key==key);
		return variable!=null ? variable.value : "";
	}
	public int GetInt(string key)
	{
		IntVariable variable=intVariables.Find(v => v.key==key);
		return variable!=null ? variable.value : 0;
	}
	public float GetFloat(string key)
	{
		FloatVariable variable=floatVariables.Find(v => v.key==key);
		return variable!=null ? variable.value : 0f;
	}
	public bool GetBool(string key)
	{
		BoolVariable variable=boolVariables.Find(v => v.key==key);
		return variable!=null && variable.value;
	}
	//ADD VARIABLES
	public void AddInt(string key, int amount)
	{
		SetInt(key, GetInt(key)+amount);
	}
	public void AddFloat(string key, float amount)
	{
		SetFloat(key, GetFloat(key)+amount);
	}
	//VARIABLES
	public string ReplaceVariables(string text)
	{
		if(string.IsNullOrEmpty(text))
			return text;
		foreach(StringVariable variable in stringVariables)
			text=text.Replace("{"+variable.key+"}", variable.value);
		foreach(IntVariable variable in intVariables)
			text=text.Replace("{"+variable.key+"}", variable.value.ToString());
		foreach(FloatVariable variable in floatVariables)
			text=text.Replace("{"+variable.key+"}", variable.value.ToString());
		foreach(BoolVariable variable in boolVariables)
			text=text.Replace("{"+variable.key+"}", variable.value.ToString());
		return text;
	}
	//SAVE/LOAD JSON
	public void Save()
	{
		VariablesData data=new VariablesData{strings=stringVariables, ints=intVariables, floats=floatVariables,bools=boolVariables};
		string json=JsonUtility.ToJson(data, true);
		System.IO.File.WriteAllText(SavePath, json);
	}
	public void Load()
	{
		if(!System.IO.File.Exists(SavePath))
		{
			Save();
			return;
		}
		string json=System.IO.File.ReadAllText(SavePath);
		VariablesData data=JsonUtility.FromJson<VariablesData>(json);
		if(data==null)
			return;
		stringVariables=data.strings ?? new List<StringVariable>();
		intVariables=data.ints ?? new List<IntVariable>();
		floatVariables=data.floats ?? new List<FloatVariable>();
		boolVariables=data.bools ?? new List<BoolVariable>();
	}
	public void ResetVariables()
	{
		stringVariables.Clear();
		intVariables.Clear();
		floatVariables.Clear();
		boolVariables.Clear();
		Save();
	}
}
