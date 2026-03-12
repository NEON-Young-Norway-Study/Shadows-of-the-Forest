using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

public class SceneDialogueExporter
{
    [MenuItem("Tools/Exportar Diálogos (Incluir Desactivados y Prefabs)", false, 20)]
    public static void ExportSceneDialogues()
    {
        string savePath = EditorUtility.SaveFilePanel("Guardar CSV de diálogos", "Assets", "SceneDialogues_Full.csv", "csv");
        if (string.IsNullOrEmpty(savePath)) return;

        StringBuilder csvContent = new StringBuilder();
        csvContent.AppendLine("Scene,ObjectName,LineIndex,Text");

        string activeScenePath = EditorSceneManager.GetActiveScene().path;

        // Filtramos solo escenas dentro de la carpeta Assets
        string[] scenePaths = AssetDatabase.FindAssets("t:Scene")
            .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
            .Where(path => path.StartsWith("Assets/"))
            .ToArray();

        int totalFound = 0;

        foreach (string path in scenePaths)
        {
            // Abrimos la escena de forma aditiva o normal
            EditorSceneManager.OpenScene(path, OpenSceneMode.Single);

            // Buscamos todos los DialogueSystem, incluidos desactivados e instancias de Prefabs
            DialogueSystem[] allSystems = Resources.FindObjectsOfTypeAll<DialogueSystem>();

            foreach (var system in allSystems)
            {
                // Verificamos que el objeto pertenezca realmente a la escena que acabamos de abrir
                // Esto excluye diálogos que pudieran estar definidos dentro de Prefabs en la carpeta Project
                if (system.gameObject.scene.path == path)
                {
                    SerializedObject so = new SerializedObject(system);
                    SerializedProperty linesProp = so.FindProperty("dialogueLines");

                    if (linesProp != null && linesProp.isArray)
                    {
                        for (int i = 0; i < linesProp.arraySize; i++)
                        {
                            string line = linesProp.GetArrayElementAtIndex(i).stringValue;
                            string sceneName = Path.GetFileNameWithoutExtension(path);
                            string escapedLine = line.Replace("\"", "\"\"");

                            csvContent.AppendLine($"\"{sceneName}\",\"{system.gameObject.name}\",{i},\"{escapedLine}\"");
                            totalFound++;
                        }
                    }
                }
            }
        }

        // Regresar a la escena original
        if (!string.IsNullOrEmpty(activeScenePath))
            EditorSceneManager.OpenScene(activeScenePath);

        File.WriteAllText(savePath, csvContent.ToString(), new UTF8Encoding(true));

        EditorUtility.DisplayDialog("Proceso Completado", $"Se han extraído {totalFound} líneas de diálogo.", "OK");
    }
}