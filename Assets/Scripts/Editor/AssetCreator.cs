using Content;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace EditorExtension
{
    public abstract class AssetCreator<T> where T : ScriptableObject
    {
        public static void Create()
        {
            T asset = ScriptableObject.CreateInstance<T>();
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);

            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }

            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T).Name + ".asset");

            AssetDatabase.CreateAsset(asset, assetPathAndName);

            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }

    public class PhysicsBehaviourCreator : AssetCreator<PhysicsBehaviour>
    {
        [MenuItem("Assets/Create/PhysicsBehaviour")]
        public static void CreateAsset()
        {
            Create();
        }
    }
}