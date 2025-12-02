using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileSlot)), CanEditMultipleObjects]
public class TileSlotEditor : Editor
{
    private GUIStyle canterStyle;
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        base.OnInspectorGUI();

        canterStyle = new GUIStyle(GUI.skin.label);
        {
            canterStyle.alignment = TextAnchor.MiddleCenter;
            canterStyle.fontStyle = FontStyle.Bold;
            canterStyle.fontSize = 14;
        }
       float oneButtonWidth = (EditorGUIUtility.currentViewWidth - 25) ; // Buton genişliği 
       float twoButtonWidth = (EditorGUIUtility.currentViewWidth - 25) / 2; // Buton genişliği 
       float threeButtonWidth = (EditorGUIUtility.currentViewWidth - 25) / 3; // Buton genişliği 

       GUILayout.Label("Pozisyon ve Rotasyon Ayarları", canterStyle);
       EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Sola Rotasyon", GUILayout.Width(twoButtonWidth)))
        {
            foreach(var targetTile in targets)
            {
                ((TileSlot)targetTile).TileRotate(-1);
            }
        }

        if (GUILayout.Button("Sağa Rotasyon", GUILayout.Width(twoButtonWidth)))
        {
            foreach(var targetTile in targets)
            {
                ((TileSlot)targetTile).TileRotate(1);
            }
        }
       EditorGUILayout.EndHorizontal();

       EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("-.1 Y de ", GUILayout.Width(twoButtonWidth)))
        {
            foreach(var targetTile in targets)
            {
                ((TileSlot)targetTile).ADJuse(-1);
            }
        }

        if (GUILayout.Button("+.1 Y de", GUILayout.Width(twoButtonWidth)))
        {
            foreach(var targetTile in targets)
            {
                ((TileSlot)targetTile).ADJuse(1);
            }
        }
       EditorGUILayout.EndHorizontal();
       
       GUILayout.Label("Karo Ayarları", canterStyle);
       EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Yol", GUILayout.Width(twoButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().yol;
            foreach(var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTİle(newTile);
            }
        }

        if (GUILayout.Button("Kaldırım", GUILayout.Width(twoButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().kaldırım;
            foreach(var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTİle(newTile);
            }
        }

       EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
         if (GUILayout.Button("YarımYol", GUILayout.Width(oneButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().yarımYol;
            foreach(var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTİle(newTile);
            }
        }

        EditorGUILayout.EndHorizontal();

        GUILayout.Label("Viraj Ayarları", canterStyle);
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("DıştanViraj", GUILayout.Width(twoButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().dıştanViraj;
            foreach(var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTİle(newTile);
            }
        }

        if (GUILayout.Button("İçtenViraj", GUILayout.Width(twoButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().içtenViraj;
            foreach(var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTİle(newTile);
            }
        }

       EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Küçük DıştanViraj", GUILayout.Width(twoButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().küçük_dıştanViraj;
            foreach(var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTİle(newTile);
            }
        }

        if (GUILayout.Button("Küçük İçtenViraj", GUILayout.Width(twoButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().küçük_içtenViraj;
            foreach(var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTİle(newTile);
            }
        }

       EditorGUILayout.EndHorizontal();

       GUILayout.Label("Köprü Ayarları", canterStyle);
       EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Köprü ve Yol", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().yol_köprü;
            foreach(var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTİle(newTile);
            }
        }

        if (GUILayout.Button("Köprü ve Kaldırım", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().kaldırım_köprü;
            foreach(var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTİle(newTile);
            }
        }

        if (GUILayout.Button("Köprü ve Yarım Yol", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().yarımYol_köprü;
            foreach(var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTİle(newTile);
            }
        }

       EditorGUILayout.EndHorizontal();

        GUILayout.Label("Tepe Ayarları", canterStyle);
       EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Tepe1", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().tepe_1;
            foreach(var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTİle(newTile);
            }
        }

        if (GUILayout.Button("Tepe2", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().tepe_2;
            foreach(var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTİle(newTile);
            }
        }

        if (GUILayout.Button("Tepe3", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindFirstObjectByType<TileSetHolder>().tepe_3;
            foreach(var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTİle(newTile);
            }
        }

       EditorGUILayout.EndHorizontal();



    }
}
