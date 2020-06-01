using UnityEngine;
using UnityEditor;

public class UpdateCardResourcePath {

    [MenuItem("Tools/Update cards")]
    private static void UpdateCards() {
        foreach(var card in Resources.LoadAll<CardBase>("")) {
            string path = AssetDatabase.GetAssetPath(card);
            path = path.Replace("Assets/Resources/", "");
            path = path.Replace(".asset", "");
            card.resourcePath = path;
            EditorUtility.SetDirty(card);
        }
    }

}