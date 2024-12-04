using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Player player = (Player)target;
        player.health = EditorGUILayout.IntField("Ma santé", player.health);
        player.maxHealth = EditorGUILayout.IntField("Ma snté max", player.maxHealth);
        player.cube = (GameObject)EditorGUILayout.ObjectField("Bloc", player.cube, typeof(GameObject), true);
        if (GUILayout.Button("Heal"))
        {
            player.health = player.maxHealth;
        }
        if (GUILayout.Button("Instantiate cube"))
        {
            Instantiate(player.cube);
        }
    }
}
