using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor.Tools
{
    public class CircleFormationWindow : EditorWindow
    {
        #region Constants

        private const int ButtonWidth = 385;

        #endregion
        
        private Transform _pivot;
        private float _radius;

        private List<GameObject> _gameObjects = new List<GameObject>();

        [MenuItem("Let's Leap/ScoreComet Formation")]
        private static void Init()
        {
            var window = GetWindowWithRect<CircleFormationWindow>(new Rect(0, 0, 400, 500));
            window.titleContent = new GUIContent("ScoreComet Formation");
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical(string.Empty, EditorStyles.helpBox);
            GUILayout.BeginHorizontal();

            _pivot = (Transform) EditorGUILayout.ObjectField("Pivot", _pivot, typeof(Transform), true);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();

            _radius = EditorGUILayout.FloatField("Radius", _radius);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Formation", GUILayout.Width(ButtonWidth)))
            {
                var center = _pivot.position;
                var angle = 90f;
                var angleStep = 360f/ _pivot.childCount;
                foreach (Transform transform in _pivot)
                {
                    var position = new Vector3
                    {
                        x = center.x + _radius * Mathf.Sin(angle * Mathf.Deg2Rad),
                        y = center.y + _radius * Mathf.Cos(angle * Mathf.Deg2Rad),
                        z = center.z,
                    };
                    position = Quaternion.AngleAxis(90, Vector3.forward) * position;
                    transform.localPosition = position;
                    angle += angleStep;
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
    }
}