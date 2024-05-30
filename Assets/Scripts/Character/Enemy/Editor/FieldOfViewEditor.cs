using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360f, fov.fovRadius);
        Handles.color = Color.gray;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360f, fov.fovPerRadius);

        Vector3 fovAngle01 = DirectionFromAngles(fov.transform.eulerAngles.y, -fov.fovAngle / 2f);
        Vector3 fovAngle02 = DirectionFromAngles(fov.transform.eulerAngles.y, fov.fovAngle / 2f);
        Vector3 fovPerAngle01 = DirectionFromAngles(fov.transform.eulerAngles.y, -fov.fovPerAngle / 2f);
        Vector3 fovPerAngle02 = DirectionFromAngles(fov.transform.eulerAngles.y, fov.fovPerAngle / 2f);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + fovAngle01 * fov.fovRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + fovAngle02 * fov.fovRadius);
        Handles.color = Color.cyan;
        Handles.DrawLine(fov.transform.position, fov.transform.position + fovPerAngle01 * fov.fovPerRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + fovPerAngle02 * fov.fovPerRadius);

        if (fov.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.playerRef.transform.position);
        }
    }

    private Vector3 DirectionFromAngles(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0f, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
