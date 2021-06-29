// Created 06/04/2021

using UnityEngine;
using UnityEditor;
using System;

public class SearchBarTool : EditorWindow
{
    private string _searchText;
    private string _oldSearchText;
    private Vector2 _scrollView = Vector2.zero;
    private string[] results;
    GUIStyle _assestLableStyle;


    public string SearchText
    {
        get
        {
            return _searchText;
        }

        set
        {
            _oldSearchText = _searchText;
            _searchText = value;
        }
    }

    [MenuItem("Window/SearchBar &#f")]
    public static void ShowWindow()
    {
        var wnd = GetWindow<SearchBarTool>();
        wnd.titleContent = new GUIContent("Search Bar");
    }

    private void OnGUI()
    {
        _assestLableStyle = new GUIStyle("Label");
        GUILayout.BeginHorizontal(GUI.skin.FindStyle("Toolbar"));
        SearchText = GUILayout.TextField(SearchText, GUI.skin.FindStyle("ToolbarSeachTextField"));


        if (GUILayout.Button("", GUI.skin.FindStyle("ToolbarSeachCancelButton")))
        {
            Array.Clear(results, 0, results.Length);
            SearchText = "";
            GUI.FocusControl(null);
        }

        GUILayout.EndHorizontal();

        if (SearchText == "" | SearchText == null)
        {
            if (results == null) return;

            Array.Clear(results, 0, results.Length);
        }
        else
        {
            if (_oldSearchText != SearchText)
            {
                Debug.Log(SearchText + " = searchText");
                results = AssetDatabase.FindAssets(SearchText);
            }

        }

        _scrollView = GUILayout.BeginScrollView(_scrollView);

        _assestLableStyle.normal.textColor = Color.white;
        _assestLableStyle.hover.textColor = Color.red;

        for (int i = 0; i < results.Length; i++)
        {
            var l = AssetDatabase.GUIDToAssetPath(results[i]);

            GUILayout.Space(5f);
            GUILayout.BeginHorizontal();

            GUILayout.Space(20f);

            if (GUILayout.Button(l, _assestLableStyle))
            {
                Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(l);
            }

            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();
    }
}
