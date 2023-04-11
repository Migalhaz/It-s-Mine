using UnityEngine;
using UnityEditor;

namespace Game.ScriptableObjects.Editor
{
    public class ScriptableObjectMenu
    {
        public static void NewScriptable<T>(string _mainFolder, string _currentFolder, string _assetName = "New Scriptable") where T : ScriptableObject
        {
            T newScriptable = ScriptableObject.CreateInstance<T>();

            if (!AssetDatabase.IsValidFolder($"Assets/{_mainFolder}"))
            {
                AssetDatabase.CreateFolder("Assets", _mainFolder);
            }

            if (!AssetDatabase.IsValidFolder($"Assets/{_mainFolder}/{_currentFolder}"))
            {
                AssetDatabase.CreateFolder($"Assets/{_mainFolder}", _currentFolder);
            }

            int tries = 0;

            string triedName = _assetName;
            while (AssetDatabase.LoadAllAssetsAtPath($"Assets/{_mainFolder}/{_currentFolder}/{triedName}.asset").Length > 0)
            {
                tries++;
                triedName = $"{_assetName} {tries}";
            }
            _assetName = triedName;

            AssetDatabase.CreateAsset(newScriptable, $"Assets/{_mainFolder}/{_currentFolder}/{_assetName}.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = newScriptable;
        }
    }
}