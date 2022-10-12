using Exercises;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEditor.Toolbars;
using UnityEngine;

namespace EditorTools
{
    #region SavePoseRotations

    // [EditorToolbarElement(Identifier, EditorWindowType)] is used to register toolbar elements for use in ToolbarOverlay
    // implementations.
    [EditorToolbarElement(id, typeof(SceneView))]
    class SavePoseRotations : EditorToolbarButton, IAccessContainerWindow
    {
        // This ID is used to populate toolbar elements.
        public const string id = "SavePoseRotations/Button";

        // IAccessContainerWindow provides a way for toolbar elements to access the `EditorWindow` in which they exist.
        // Here we use `containerWindow` to focus the camera on our newly instantiated objects after creation.
        public EditorWindow containerWindow { get; set; }

        private ModelBodyPoints selectedModelBodyPoints;

        // As this is ultimately just a VisualElement, it is appropriate to place initialization logic in the constructor.
        // In this method you can also register to any additional events as required. Here we will just set up the basics:
        // a tooltip, icon, and action.
        public SavePoseRotations()
        {
            // A toolbar element can be either text, icon, or a combination of the two. Keep in mind that if a toolbar is
            // docked horizontally the text will be clipped, so usually it's a good idea to specify an icon.
            text = "Save Pose Rotations";
            icon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Art/Textures/EditorIcons/SaveIcon.png");

            tooltip = "Saves the pose that the model is currently in. You can use the animation preview to set it to a specific point";

            clicked += OnClick;
        }

        // This method will be invoked when the `SavePoseRotations` button is clicked.
        private void OnClick()
        {
            if (selectedModelBodyPoints==null)
                return;
            
            Debug.Log("Saving: "+selectedModelBodyPoints.name);
        }
    }

    #endregion

    #region PoseRotationSaveTool

    // By passing `typeof(ModelBodyPoints)` as the second argument, we register PoseRotationSaveTool as a CustomEditor tool to be presented
    // when the current selection contains a ModelBodyPoints component.
    //[EditorTool("Pose Rotation Save Tool", typeof(ModelBodyPoints))]

    // [EditorToolbarElement(Identifier, EditorWindowType)] is used to register toolbar elements for use in ToolbarOverlay
    // implementations.

    [EditorToolbarElement(id, typeof(SceneView))]
    public class SelectModelBodyPoint : EditorToolbarDropdown, IAccessContainerWindow
    {
        private GUIContent poseToolbarIcon;
        private ModelBodyPoints[] modelBodyPoints;

        private int modelBodyPointsIndex;

        // This ID is used to populate toolbar elements.
        public const string id = "SelectModelBodyPoint/Dropdown";


        // IAccessContainerWindow provides a way for toolbar elements to access the `EditorWindow` in which they exist.
        // Here we use `containerWindow` to focus the camera on our newly instantiated objects after creation.
        public EditorWindow containerWindow { get; set; }

        // As this is ultimately just a VisualElement, it is appropriate to place initialization logic in the constructor.
        // In this method you can also register to any additional events as required. Here we will just set up the basics:
        // a tooltip, icon, and action.
        public SelectModelBodyPoint()
        {
            // A toolbar element can be either text, icon, or a combination of the two. Keep in mind that if a toolbar is
            // docked horizontally the text will be clipped, so usually it's a good idea to specify an icon.
            text = "Select Model";
            icon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Art/Textures/EditorIcons/PoseToolIcon.png");

            tooltip =
                "Select the model you wish to use.";

            // When the dropdown is opened, ShowColorMenu is invoked and we can create a popup menu.
            clicked += ShowDifferentModels;
        }

        // When the dropdown button is clicked, this method will create a popup menu at the mouse cursor position.
        private void ShowDifferentModels()
        {
            modelBodyPoints = Object.FindObjectsOfType<ModelBodyPoints>();
            if (modelBodyPoints == null)
                return;

            var menu = new GenericMenu();
            var index = 0;
            foreach (var bodyPoints in modelBodyPoints)
            {
                var index1 = index;
                menu.AddItem(new GUIContent(bodyPoints.name), modelBodyPointsIndex == index1,
                    () => modelBodyPointsIndex = index1);
                index++;
            }

            menu.ShowAsContext();
        }
    }

    #endregion

    #region ExerciseCreationToolbar

    // All Overlays must be tagged with the OverlayAttribute
    [Overlay(typeof(SceneView), "Exercise Creation Tools")]
    // IconAttribute provides a way to define an icon for when an Overlay is in collapsed form. If not provided, the first
    // two letters of the Overlay name will be used.
    [Icon("Assets/Art/Textures/EditorIcons/PoseToolIcon.png")]
    // Toolbar overlays must inherit `ToolbarOverlay` and implement a parameter-less constructor. The contents of a toolbar
    // are populated with string IDs, which are passed to the base constructor. IDs are defined by
    // EditorToolbarElementAttribute.
    public class ExerciseCreationToolbar : ToolbarOverlay
    {
        // ToolbarOverlay implements a parameterless constructor, passing 2 EditorToolbarElementAttribute IDs. This will
        // create a toolbar overlay with buttons for the CreateCubes and DropdownToggleExample examples.
        // This is the only code required to implement a toolbar overlay. Unlike panel overlays, the contents are defined
        // as standalone pieces that will be collected to form a strip of elements.
        private ExerciseCreationToolbar() : base(
            SelectModelBodyPoint.id,SavePoseRotations.id)
        {
        }
    }

    #endregion
}