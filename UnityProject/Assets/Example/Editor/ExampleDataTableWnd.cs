using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace StardustEditorTool
{
    //a normal EditorWindow
    public class ExampleDataTableWnd : EditorWindow
    {
        #region
        
        //define the class of data
        public class ExampleData
        {
            public int ID;
            public string Name;
            public int Age;
        }

        //define a CommonTable class for ExampleData
        public class ExampleDataTable : CommonTable<ExampleData>
        {
            public ExampleDataTable(List<ExampleData> datas,
                CommonTableColumn<ExampleData>[] cs,
                FilterMethod<ExampleData> onfilter,
                SelectMethod<ExampleData> onselect = null)
                : base(datas, cs, onfilter, onselect)
            {
            }
        }
        #endregion

        [MenuItem("Tools/Windows/ExampleDataTableWnd")]
        public static void Open()
        {
            GetWindow<ExampleDataTableWnd>();
        }

        private ExampleDataTable m_table;

        protected void OnGUI()
        {
            if (m_table == null)
                InitDatas();
            if (m_table != null)
                m_table.OnGUI();
        }

        private void InitDatas(bool forceUpdate = false)
        {
            if (forceUpdate || m_table == null)
            {
                //init datas
                var datas = new List<ExampleData>();
                //for example,add some test datas
                datas.Add(new ExampleData() { ID = 101, Age = 10, Name = "Lili" });
                datas.Add(new ExampleData() { ID = 203, Age = 15, Name = "JoJo" });
                datas.Add(new ExampleData() { ID = 404, Age = 11, Name = "Kikki" });
                datas.Add(new ExampleData() { ID = 508, Age = 30, Name = "Baby" });

                //init columns
                var cols = new CommonTableColumn<ExampleData>[]
                {
                    new CommonTableColumn<ExampleData>
                    {
                        headerContent = new GUIContent("ID"),       //header display name
                        canSort = true,                             //
                        Compare = (a,b)=>-a.ID.CompareTo(b.ID),      //sort method
                        DrawCell = (rect,data)=>EditorGUI.LabelField(rect,data.ID.ToString()),
                    },
                    new CommonTableColumn<ExampleData>
                    {
                        headerContent = new GUIContent("Name"),//header display name
                        canSort = true,
                        Compare = (a,b)=>-a.Name.CompareTo(b.Name),//sort method
                        DrawCell = (rect,data)=>EditorGUI.LabelField(rect,data.Name),
                    },
                    new CommonTableColumn<ExampleData>
                    {
                        headerContent = new GUIContent("Age"),//header display name
                        DrawCell = (rect,data)=>EditorGUI.LabelField(rect,data.Age.ToString()),
                    }
                };


                m_table = new ExampleDataTable(datas, cols, OnFilter, OnRowSelect);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="datas"></param>
        private void OnRowSelect(List<ExampleData> datas)
        {
            throw new NotImplementedException();
        }

        private bool OnFilter(ExampleData data, string std)
        {
            int number;
            if (!int.TryParse(std, out number))
                return false;
            return data.ID == number;
        }
    }
}