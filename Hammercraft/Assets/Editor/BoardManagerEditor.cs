using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoardManager))]
public class BoardManagerEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if (GUILayout.Button("onReset.Invoke()")) ((BoardManager)target).onReset.Invoke();
        if (GUILayout.Button("SubmitManager()")) ((BoardManager)target).SubmitManager();
    }
}