﻿namespace FoxKit.Modules.RouteBuilder.Editor
{
    using FoxKit.Utils;
    using Rotorz.Games.Collections;
    using System.Collections.Generic;
    using UnityEditor;

    using UnityEngine;

    /// <summary>
    /// Custom editor for Routes.
    /// </summary>
    [CustomEditor(typeof(Route))]
    public class RouteEditor : Editor
    {
        private ReorderableListControl listControl;
        private IReorderableListAdaptor listAdaptor;
        
        private RouteNode CustomListItem(Rect position, RouteNode itemValue)
        {
            Rect iPos = new Rect(position.xMin, position.yMin, position.width - 33, position.height);
            itemValue = EditorGUI.ObjectField(iPos, itemValue, typeof(RouteNode), true) as RouteNode;

            GUIContent buttonContent = new GUIContent("+", "Insert new node here.");
            Rect bPos = new Rect(position.xMax - 30, position.yMin, 30, position.height);
            bool pressed = GUI.Button(bPos, buttonContent);
            if (pressed)
            {
                Route r = this.target as Route;
                int thisIndex = r.Nodes.IndexOf(itemValue);
                r.AddNewNode(thisIndex + 1);
            }
            pressed = false;

            return itemValue;
        }

        void OnEnable()
        {
            var route = this.target as Route;
            route.Rebuild();

            listControl = new ReorderableListControl();
            listControl.ItemRemoving += this.OnItemRemoving;
            listAdaptor = new GenericListAdaptor<RouteNode>(route.Nodes, CustomListItem, ReorderableListGUI.DefaultItemHeight);
        }

        private void OnDisable()
        {
            // Unsubscribe from events
            if (listControl != null)
            {
                listControl.ItemRemoving -= this.OnItemRemoving;
            }
        }

        private void OnItemRemoving(object sender, ItemRemovingEventArgs args)
        {
            var route = this.target as Route;
            RouteNode item = route.Nodes[args.ItemIndex];
            DestroyImmediate(item.gameObject);
        }

        public override void OnInspectorGUI()
        {
            var route = this.target as Route;

            DrawToolShelf(route);
            DrawSettings(route);
            DrawNodeList(route);            

            EditorUtility.SetDirty(target);
        }

        private void DrawToolShelf(Route route)
        {
            var iconAddNode = Resources.Load("UI/Route Builder/Buttons/routebuilder_button_new_node") as Texture;
            var iconParent = Resources.Load("UI/Route Builder/Buttons/routebuilder_button_parent") as Texture;

            Rotorz.Games.Collections.ReorderableListGUI.Title("Tools");
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            // Add node button
            if (FoxKitUiUtils.ToolButton(iconAddNode, "Add a new node to the end of the route."))
            {
                route.AddNewNode();
            }

            // Select parent button
            if (FoxKitUiUtils.ToolButton(iconParent, "Select parent RouteSet."))
            {
                UnitySceneUtils.Select(route.transform.parent.gameObject);
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        private void DrawSettings(Route route)
        {
            Rotorz.Games.Collections.ReorderableListGUI.Title("Settings");

            var closedContent = new GUIContent("Closed", "If checked, the first and last nodes will appear connected.");
            route.Closed = EditorGUILayout.Toggle(closedContent, route.Closed);

            var treatNameAsHash = new GUIContent("Treat name as hash", "When exporting, treat the route's name as a hash instead of a string literal. Use if its true name is unknown.");
            route.TreatNameAsHash = EditorGUILayout.Toggle(treatNameAsHash, route.TreatNameAsHash);
        }

        private void DrawNodeList(Route route)
        {
            Rotorz.Games.Collections.ReorderableListGUI.Title("Nodes");
            listControl.Draw(listAdaptor);
        }

        private static void DrawEmpty()
        {
            GUILayout.Label("Route has no nodes.", EditorStyles.miniLabel);
        }
    }
}