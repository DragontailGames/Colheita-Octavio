 using UnityEngine;
 using UnityEngine.UI;
 
 #if UNITY_EDITOR
 using UnityEditor;
 #endif
 
[CreateAssetMenu (menuName = "Event/Event Status Teste")]
 public class TestStatus : ScriptableObject
 {
	public enum testEnum{teste1,test2};
	public testEnum test_sfae;

	[HideInInspector]
	public Vector2 vec2;

	// ... Update(), Awake(), etc
 }

#if UNITY_EDITOR
[CustomEditor(typeof(TestStatus))]
public class RandomScript_Editor : Editor
{

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector(); // for other non-HideInInspector fields

		TestStatus script = (TestStatus)target;

		if (script.test_sfae == TestStatus.testEnum.test2) // if bool is true, show other fields
		{
			script.vec2 = EditorGUILayout.Vector2Field("Vector 2", script.vec2);
		}
	}
}
#endif
