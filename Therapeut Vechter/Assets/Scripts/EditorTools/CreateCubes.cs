using UnityEditor;
using UnityEditor.Overlays;
using UnityEditor.Toolbars;
using UnityEngine;

// [EditorToolbarElement(Identifier, EditorWindowType)] is used to register toolbar elements for use in ToolbarOverlay
// implementations.
[EditorToolbarElement(id, typeof(SceneView))]
class CreateCubes : EditorToolbarButton, IAccessContainerWindow
{
    // This ID is used to populate toolbar elements.
    public const string id = "ExampleToolbar/Button";

    // IAccessContainerWindow provides a way for toolbar elements to access the `EditorWindow` in which they exist.
    // Here we use `containerWindow` to focus the camera on our newly instantiated objects after creation.
    public EditorWindow containerWindow { get; set; }

    // As this is ultimately just a VisualElement, it is appropriate to place initialization logic in the constructor.
    // In this method you can also register to any additional events as required. Here we will just set up the basics:
    // a tooltip, icon, and action.
    public CreateCubes()
    {
        // A toolbar element can be either text, icon, or a combination of the two. Keep in mind that if a toolbar is
        // docked horizontally the text will be clipped, so usually it's a good idea to specify an icon.
        text = "Create Cubes";
        icon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/CreateCubesIcon.png");

        tooltip = "Instantiate some cubes in the scene.";
        clicked += OnClick;
    }

    // This method will be invoked when the `CreateCubes` button is clicked.
    void OnClick()
    {
        var parent = new GameObject("Cubes").transform;

        // When writing editor tools don't forget to be a good citizen and implement Undo!
        Undo.RegisterCreatedObjectUndo(parent.gameObject, "Create Cubes in Sphere");

        for (int i = 0; i < 50; i++)
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;

            Undo.RegisterCreatedObjectUndo(cube.gameObject, "Create Cubes in Sphere");
            cube.position = Random.insideUnitSphere * 25;
            cube.rotation = Quaternion.LookRotation(Random.onUnitSphere);

            Undo.SetTransformParent(cube, parent, "Create Cubes in Sphere");
            cube.SetParent(parent);
        }

        Selection.activeTransform = parent;

        if (containerWindow is SceneView view)
            view.FrameSelected();
    }
}

// Same as above, except this time we'll create a toggle + dropdown toolbar item.
[EditorToolbarElement(id, typeof(SceneView))]
class DropdownToggleExample : EditorToolbarDropdownToggle, IAccessContainerWindow
{
    public const string id = "ExampleToolbar/DropdownToggle";

    // This property is specified by IAccessContainerWindow and is used to access the Overlay's EditorWindow.
    public EditorWindow containerWindow { get; set; }
    static int colorIndex = 0;
    static readonly Color[] colors = new Color[] { Color.red, Color.green, Color.cyan };

    DropdownToggleExample()
    {
        text = "Color Bar";
        tooltip = "Display a color swatch in the top left of the scene view. Toggle on or off, and open the dropdown" +
            "to change the color.";

        // When the dropdown is opened, ShowColorMenu is invoked and we can create a popup menu.
        dropdownClicked += ShowColorMenu;

        // Subscribe to the Scene View OnGUI callback so that we can draw our color swatch.
        SceneView.duringSceneGui += DrawColorSwatch;
    }

    void DrawColorSwatch(SceneView view)
    {
        // Test that this callback is for the Scene View that we're interested in, and also check if the toggle is on
        // or off (value).
        if (view != containerWindow || !value)
            return;

        Handles.BeginGUI();
        GUI.color = colors[colorIndex];
        GUI.DrawTexture(new Rect(8, 8, 120, 24), Texture2D.whiteTexture);
        GUI.color = Color.white;
        Handles.EndGUI();
    }

    // When the dropdown button is clicked, this method will create a popup menu at the mouse cursor position.
    void ShowColorMenu()
    {
        var menu = new GenericMenu();
        menu.AddItem(new GUIContent("Red"), colorIndex == 0, () => colorIndex = 0);
        menu.AddItem(new GUIContent("Green"), colorIndex == 1, () => colorIndex = 1);
        menu.AddItem(new GUIContent("Blue"), colorIndex == 2, () => colorIndex = 2);
        menu.ShowAsContext();
    }
}

// All Overlays must be tagged with the OverlayAttribute
[Overlay(typeof(SceneView), "Placement Tools")]
// IconAttribute provides a way to define an icon for when an Overlay is in collapsed form. If not provided, the first
// two letters of the Overlay name will be used.
[Icon("Assets/PlacementToolsIcon.png")]
// Toolbar overlays must inherit `ToolbarOverlay` and implement a parameter-less constructor. The contents of a toolbar
// are populated with string IDs, which are passed to the base constructor. IDs are defined by
// EditorToolbarElementAttribute.
public class EditorToolbarExample : ToolbarOverlay
{
    // ToolbarOverlay implements a parameterless constructor, passing 2 EditorToolbarElementAttribute IDs. This will
    // create a toolbar overlay with buttons for the CreateCubes and DropdownToggleExample examples.
    // This is the only code required to implement a toolbar overlay. Unlike panel overlays, the contents are defined
    // as standalone pieces that will be collected to form a strip of elements.
    EditorToolbarExample() : base(
        CreateCubes.id,
        DropdownToggleExample.id)
    {}
}