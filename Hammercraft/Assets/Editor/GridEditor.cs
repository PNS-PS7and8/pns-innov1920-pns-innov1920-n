using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoardManager))]
public class BoardEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if (GUILayout.Button("Initialize")) {
            ((BoardManager)target).ResetBoard();
        }
    }
}