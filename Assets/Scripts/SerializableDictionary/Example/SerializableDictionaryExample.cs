using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*[Serializable] public class MyDictionary1 : SerializableDictionary<ActionEnum.Action, float> { }

[CustomPropertyDrawer(typeof(MyDictionary1))]
public class MyDictionaryDrawer1 : DictionaryDrawer<ActionEnum.Action, float> { }*/

public class SerializableDictionaryExample : MonoBehaviour {
	// The dictionaries can be accessed throught a property
	[SerializeField]
	StringStringDictionary m_stringStringDictionary;
	public IDictionary<string, string> StringStringDictionary
	{
		get { return m_stringStringDictionary; }
		set { m_stringStringDictionary.CopyFrom (value); }
	}

	public ObjectColorDictionary m_objectColorDictionary;
	public StringColorArrayDictionary m_objectColorArrayDictionary;

	void Reset ()
	{
		// access by property
		StringStringDictionary = new Dictionary<string, string>() { {"first key", "value A"}, {"second key", "value B"}, {"third key", "value C"} };
		m_objectColorDictionary = new ObjectColorDictionary() { {gameObject, Color.blue}, {this, Color.red} };
	}
}
