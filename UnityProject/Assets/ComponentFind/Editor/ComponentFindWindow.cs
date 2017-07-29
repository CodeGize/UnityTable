using UnityEditor;
using UnityEngine;
using UnityTable;

namespace ComponentFind
{
    public class ComponentFindWindow : EditorWindow
    {
        [MenuItem("Tools/Windows/ComponentFindWindow")]
        public static void Open()
        {
            GetWindow<ComponentFindWindow>();
        }

        private SerializedPropertyTable m_table;

        public void OnGUI()
        {
            using (new EditorGUILayout.VerticalScope())
            {
                if (m_table != null)
                {
                    m_table.OnGUI();
                }
            }
        }

        public void OnEnable()
        {
            m_table = new SerializedPropertyTable("Table", FindObjects, CreateCameraColumn);
        }

        private Camera[] FindObjects()
        {
            return FindObjectsOfType<Camera>();
        }


        private SerializedPropertyTreeView.Column[] CreateCameraColumn(out string[] propnames)
        {
            propnames = new string[3];
            var columns = new SerializedPropertyTreeView.Column[3];
            columns[0] = new SerializedPropertyTreeView.Column
            {
                headerContent = new GUIContent("Name"),
                headerTextAlignment = TextAlignment.Left,
                sortedAscending = true,
                sortingArrowAlignment = TextAlignment.Center,
                width = 200,
                minWidth = 25f,
                maxWidth = 400,
                autoResize = false,
                allowToggleVisibility = true,
                propertyName = null,
                dependencyIndices = null,
                compareDelegate = SerializedPropertyTreeView.DefaultDelegates.s_CompareName,
                drawDelegate = SerializedPropertyTreeView.DefaultDelegates.s_DrawName,
                filter = new SerializedPropertyFilters.Name()
            };
            columns[1] = new SerializedPropertyTreeView.Column
            {
                headerContent = new GUIContent("On"),
                headerTextAlignment = TextAlignment.Left,
                sortedAscending = true,
                sortingArrowAlignment = TextAlignment.Center,
                width = 25,
                autoResize = false,
                allowToggleVisibility = true,
                propertyName = "m_Enabled",
                dependencyIndices = null,
                compareDelegate = SerializedPropertyTreeView.DefaultDelegates.s_CompareCheckbox,
                drawDelegate = SerializedPropertyTreeView.DefaultDelegates.s_DrawCheckbox,
            };

            columns[2] = new SerializedPropertyTreeView.Column
            {
                headerContent = new GUIContent("Mask"),
                headerTextAlignment = TextAlignment.Left,
                sortedAscending = true,
                sortingArrowAlignment = TextAlignment.Center,
                width = 200,
                minWidth = 25f,
                maxWidth = 400,
                autoResize = false,
                allowToggleVisibility = true,
                propertyName = "m_CullingMask",
                dependencyIndices = null,
                compareDelegate = SerializedPropertyTreeView.DefaultDelegates.s_CompareInt,
                drawDelegate = SerializedPropertyTreeView.DefaultDelegates.s_DrawDefault,
                filter = new SerializedPropertyFilters.Name()
            };
            for (var i = 0; i < columns.Length; i++)
            {
                var column = columns[i];
                propnames[i] = column.propertyName;
            }

            return columns;
        }
    }
}