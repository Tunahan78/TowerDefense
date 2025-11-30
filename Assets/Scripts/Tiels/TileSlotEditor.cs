using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileSlot)), CanEditMultipleObjects]
public class TileSlotEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        base.OnInspectorGUI();

       float buttonWidth = (EditorGUIUtility.currentViewWidth - 25) / 2; // Buton genişliği 

       EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Yol", GUILayout.Width(buttonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().yol;
            foreach(var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTİle(newTile);
            }
        }

        if (GUILayout.Button("Kaldırım", GUILayout.Width(buttonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().kaldırım;
            foreach(var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTİle(newTile);
            }
        }

       EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
         if (GUILayout.Button("YarımYol", GUILayout.Width(buttonWidth * 2)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().yarımYol;
            foreach(var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTİle(newTile);
            }
        }

        EditorGUILayout.EndHorizontal();

    }
}
