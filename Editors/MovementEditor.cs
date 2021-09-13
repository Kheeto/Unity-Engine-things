using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Movement))]
[CanEditMultipleObjects]
public class MovementEditor : Editor
{
    bool showComponents;
    bool showMovement;
    bool showLook;
    bool showJump;

    public override void OnInspectorGUI()
    {
        Movement m = target as Movement;

        showComponents = EditorGUILayout.BeginFoldoutHeaderGroup(showComponents, "Components");
        if (showComponents)
        {
            m.playerCam = EditorGUILayout.ObjectField("Camera", m.playerCam, typeof(Transform), true) as Transform;
            m.groundCheck = EditorGUILayout.ObjectField("Ground Check", m.groundCheck, typeof(Transform), true) as Transform;
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUILayout.Space();

        showMovement = EditorGUILayout.BeginFoldoutHeaderGroup(showMovement, "Movement Settings");
        if (showMovement)
        {
            m.speed = EditorGUILayout.FloatField("Speed", m.speed);
            m.groundDinstance = EditorGUILayout.FloatField("Ground Dinstance", m.groundDinstance);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUILayout.Space();

        showLook = EditorGUILayout.BeginFoldoutHeaderGroup(showLook, "Look Settings");
        if (showLook)
        {
            GUILayout.Label("Sensivity: " + m.sensitivity);
            GUILayout.BeginHorizontal();
            m.sensitivity = GUILayout.HorizontalScrollbar(m.sensitivity, 10, m.minSensivity, m.maxSensivity);
            GUILayout.EndHorizontal();
            m.sensitivity = EditorGUILayout.FloatField("Or Edit Sensivity here: ", m.sensitivity);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUILayout.Space();

        showJump = EditorGUILayout.BeginFoldoutHeaderGroup(showJump, "Jump Settings");
        if (showJump)
        {
            m.jumpForce = EditorGUILayout.FloatField("Jump Force", m.jumpForce);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUILayout.Space();
    }
}
