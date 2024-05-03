using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyManager))]
public class EnemyManagerEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyManager enemy = (EnemyManager)target;

        Color c = Color.green;
        if (enemy.alertStage == AlertStage.Intrigued) c = Color.Lerp(Color.green, Color.red, enemy.alertLevel / 100f);
        else if (enemy.alertStage == AlertStage.Alerted) c = Color.red;

        Handles.color = new Color(c.r, c.g, c.b, 0.3f);
        Handles.DrawSolidArc(
            enemy.transform.position,
            enemy.transform.up,
            Quaternion.AngleAxis(-enemy.fovAngle / 2f, enemy.transform.up) * enemy.transform.forward,
            enemy.fovAngle,
            enemy.fov
        );
        Handles.DrawSolidArc(
            enemy.transform.position,
            enemy.transform.up,
            Quaternion.AngleAxis(-enemy.peripheralFOVAngle / 2f, enemy.transform.up) * enemy.transform.forward,
            enemy.peripheralFOVAngle,
            enemy.peripheralFOV
        );

        Handles.color = c;
        enemy.fov = Handles.ScaleValueHandle(enemy.fov, enemy.transform.position + enemy.transform.forward * enemy.fov, enemy.transform.rotation, 3f, Handles.SphereHandleCap, 1f);
        enemy.peripheralFOV = Handles.ScaleValueHandle(enemy.peripheralFOV, enemy.transform.position + enemy.transform.forward * enemy.peripheralFOV, enemy.transform.rotation, 1.5f, Handles.SphereHandleCap, 1f);
    }
}
