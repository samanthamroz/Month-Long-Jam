using UnityEditor.SearchService;
using UnityEngine;
using UnityEditor;

public class LoadingZonePicker : EditorWindow
{
    public string[] options = new string[] {"Cube", "Sphere", "Plane"};
    public int index = 0;
    [MenuItem("Examples/Editor GUILayout Popup usage")]
    static void Init()
    {
        EditorWindow window = GetWindow(typeof(LoadingZonePicker));
        window.Show();
    }

    void OnGUI()
    {
        index = EditorGUILayout.Popup(index, options);
    }

}