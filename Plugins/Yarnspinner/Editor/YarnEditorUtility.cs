using UnityEngine;
using UnityEditor;
using System.IO;

/// <summary>
/// Contains utility methods for working with Yarn Spinner content in the
/// Unity Editor.
/// </summary>
public static class YarnEditorUtility {

    // GUID for editor assets. (Doing it like this means that we don't have
    // to worry about where the assets are on disk, if the user has moved
    // Yarn Spinner around.)
    const string IconTextureGUID = "528a6dd601766934abb8b1053bd798ef";
    const string TemplateFileGUID = "4f4ca4a46020a454f80e2ac78eda5aa1";

    /// <summary>
    /// Returns a <see cref="Texture2D"/> that can be used to represent
    /// Yarn files.
    /// </summary>
    /// <returns>A texture to use in the Unity editor for Yarn
    /// files.</returns>
    public static Texture2D GetYarnDocumentIconTexture()
    {
        string textureAssetPath = AssetDatabase.GUIDToAssetPath(IconTextureGUID);

        return AssetDatabase.LoadAssetAtPath<Texture2D>(textureAssetPath);
    }

    /// <summary>
    /// Returns the path to a text file that can be used as the basis for
    /// newly created Yarn scripts.
    /// </summary>
    /// <returns>A path to a file to use in the Unity editor for creating
    /// new Yarn scripts.</returns>
    /// <throws cref="FileNotFoundException">Thrown if the template text
    /// file cannot be found.</throws>
    public static string GetTemplateYarnScriptPath()
    {
        var path = AssetDatabase.GUIDToAssetPath(TemplateFileGUID);
        if (string.IsNullOrEmpty(path))
        {
            throw new System.IO.FileNotFoundException($"Template file for new Yarn scripts couldn't be found. Have the .meta files for Yarn Spinner been modified or deleted? Try re-importing the Yarn Spinner package to fix this error.");
        }
        return path;
    }

    /// <summary>
    /// Begins the interactive process of creating a new Yarn file in the
    /// Editor.
    /// </summary>    
    [MenuItem("Assets/Create/Yarn Script", false, 101)]
    public static void CreateYarnAsset() {

        // This method call is undocumented, but public. It's defined in
        // ProjectWindowUtil, and used by other parts of the editor to
        // create other kinds of assets (scripts, textures, etc).
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
            0,
            ScriptableObject.CreateInstance<DoCreateYarnScriptAsset>(),
            "NewYarnScript.yarn",
            GetYarnDocumentIconTexture(),
            GetTemplateYarnScriptPath());
    }

    [MenuItem("Assets/Create/Yarn Spinner/Yarn Program", false, 101)]
    public static void CreateYarnProgram() {
        // This method call is undocumented, but public. It's defined in
        // ProjectWindowUtil, and used by other parts of the editor to
        // create other kinds of assets (scripts, textures, etc).
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
            0,
            ScriptableObject.CreateInstance<DoCreateYarnScriptAsset>(),
            "NewProgram.yarnprogram",
            GetYarnDocumentIconTexture(),
            GetTemplateYarnScriptPath());
    }

    /// <summary>
    /// Creates a new Yarn script at the given path, using the default
    /// template.
    /// </summary>
    /// <param name="path">The path at which to create the script.</param>
    public static void CreateYarnAsset(string path) {
        CreateYarnScriptAssetFromTemplate(path, GetTemplateYarnScriptPath());
    }

    private static Object CreateYarnScriptAssetFromTemplate(string pathName, string resourceFile)
    {
        // Read the contents of the template file
        string templateContent;
        try {
            templateContent = File.ReadAllText(resourceFile);
        } catch { 
            Debug.LogError("Failed to find the Yarn script template file. Creating an empty file instead.");
            // the minimal valid Yarn script - no headers, no body
            templateContent = "---\n===\n"; 
        } 
        
        // Figure out the 'file name' that the user entered
        string scriptName;
        if (Path.GetExtension(pathName).Equals("yarnprogram", System.StringComparison.InvariantCultureIgnoreCase)) {
            // This is a .yarnprogram file; the script "name" is always
            // "Program".
            scriptName = "Program";
        } else {
            // The script name is the name of the file, sans extension.
            scriptName = Path.GetFileNameWithoutExtension(pathName);
        } 

        // Replace any spaces with underscores - these aren't allowed in
        // node names
        scriptName = scriptName.Replace(" ", "_");

        // Replace the placeholder with the script name
        templateContent = templateContent.Replace("#SCRIPTNAME#", scriptName);

        // Respect the user's line endings preferences for this new text
        // asset
        string unixLineEndings = "\n";
        string windowsLineEndings = "\r\n";
        string lineEndings;
        switch (EditorSettings.lineEndingsForNewScripts)
        {
            case LineEndingsMode.OSNative:
                // OS native = use Windows if we're on Windows, else Unix
                var isWindows = Application.platform == RuntimePlatform.WindowsEditor;
                lineEndings = isWindows ? windowsLineEndings : unixLineEndings;
                break;
            case LineEndingsMode.Windows:
                // Windows = use Windows endings
                lineEndings = windowsLineEndings;
                break;
            case LineEndingsMode.Unix:
            default:
                // Unix or a anything else = use Unix endings
                lineEndings = unixLineEndings;
                break;
        }

        // Replace every line ending in the template (this way we don't
        // need to keep track of which line ending the asset was last saved
        // in)
        templateContent = System.Text.RegularExpressions.Regex.Replace(templateContent, @"\r\n?|\n", lineEndings);

        // Write it all out to disk as UTF-8
        var fullPath = Path.GetFullPath(pathName);
        File.WriteAllText(fullPath, templateContent, System.Text.Encoding.UTF8);

        // Force Unity to notice the new asset (this will also compile the
        // new, empty Yarn script)
        AssetDatabase.ImportAsset(pathName);

        // We don't hugely care about the details of the object anyway (we
        // just wanted to ensure that it's imported as at least an asset),
        // so we'll return it as an Object here.
        return AssetDatabase.LoadAssetAtPath<Object>(pathName);
    }

    // A handler that receives a callback after the user finishes naming a new file.
    private class DoCreateYarnScriptAsset : UnityEditor.ProjectWindowCallback.EndNameEditAction
    {
        // The user just finished typing (and didn't cancel it by pressing
        // escape or anything.) Commit the action by generating the file on
        // disk.
        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            // Produce the asset.
            Object o = CreateYarnScriptAssetFromTemplate(pathName, resourceFile);

            // Reveal it on disk.
            ProjectWindowUtil.ShowCreatedAsset(o);        
        }
    }
}
