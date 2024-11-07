using UnityEditor;
using UnityEngine;
using System.IO;

public class URPAutoAssignTextures : Editor
{
    [MenuItem("Tools/URP Auto Assign Textures for Each Subfolder")]
    static void AssignTexturesInSubfolders()
    {
        string mainFolderPath = "Assets/Models/"; // Replace with your main folder path

        // Get all subfolders in the main folder
        var subfolders = Directory.GetDirectories(mainFolderPath, "*", SearchOption.AllDirectories);

        foreach (var subfolder in subfolders)
        {
            // Create a material in each subfolder based on the subfolder name
            string folderName = Path.GetFileName(subfolder);
            string materialPath = Path.Combine(subfolder, folderName + "_Material.mat");

            Material material = AssetDatabase.LoadAssetAtPath<Material>(materialPath);
            if (material == null)
            {
                material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                AssetDatabase.CreateAsset(material, materialPath);
                Debug.Log($"Created new URP material in folder: {subfolder}");
            }

            // Find all textures in the current subfolder
            var textureGUIDs = AssetDatabase.FindAssets("t:Texture2D", new[] { subfolder });
            foreach (var textureGUID in textureGUIDs)
            {
                string texturePath = AssetDatabase.GUIDToAssetPath(textureGUID);
                Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(texturePath);
                
                // Assign texture based on its name
                AssignTextureToMaterial(material, texture);
            }
        }

        AssetDatabase.SaveAssets();
        Debug.Log("Textures assigned to materials for each subfolder!");
    }

    static void AssignTextureToMaterial(Material material, Texture2D texture)
    {
        if (texture.name.Contains("_Albedo") || texture.name.Contains("_BaseColor"))
        {
            material.SetTexture("_BaseMap", texture);
        }
        else if (texture.name.Contains("_AO"))
        {
            material.SetTexture("_OcclusionMap", texture);
        }
        else if (texture.name.Contains("_Bump") || texture.name.Contains("_Height"))
        {
            material.SetTexture("_ParallaxMap", texture); // For height/bump map
            material.EnableKeyword("_PARALLAXMAP");
        }
        else if (texture.name.Contains("_Normal"))
        {
            material.SetTexture("_BumpMap", texture);
            material.EnableKeyword("_NORMALMAP");
        }
    }
}
