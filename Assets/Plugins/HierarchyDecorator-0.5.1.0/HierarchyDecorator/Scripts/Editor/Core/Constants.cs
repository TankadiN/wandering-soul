﻿using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HierarchyDecorator
    {
    [InitializeOnLoad]
    internal static class Constants
        {
        //Prefs for references
        internal const string PREF_GUID = "HD_GUID";

        //Path
        internal const string SETTINGS_PATH = "Hierarchy Decorator";

        internal const string SETTINGS_ASSET_PATH = "Assets/HierarchyDecorator/Settings.asset";
        internal const string SETTINGS_ASSET_FOLDER = "Assets/HierarchyDecorator/";

        //Draw
        internal readonly static Color SelectionColor = new Color (58f / 255f, 178f / 255f, 178f / 255f, 1);
        internal readonly static Color HoverColor = new Color (150f / 255f, 150f / 255f, 150f / 255f, 1);

        internal readonly static Color UnactiveColor = new Color (0.9f, 0.9f, 0.9f, 0.4f);
        internal readonly static Color UnactivePrefabColor = new Color (0.48f, 0.67f, 0.95f, 0.6f);

        internal static Settings Settings => Settings.GetOrCreateSettings ();
        internal static List<PrefixSettings> prefixes => Settings.prefixes;

        //Layer Masks
        internal static string[] LayerMasks => UnityEditorInternal.InternalEditorUtility.layers;
        internal readonly static LayerMask AllLayers = ~0;

        //Editor Settings
        public readonly static string[] componentKeywords =
            {   
            "2D",

            "Anim",
            "Audio",

            "Collider",

            "Joint",

            "Nav",
            "Network",

            "Mesh",

            "Renderer",
            };

        }

    internal static class Textures
        {
        #region Icons
        internal readonly static Texture2D CheckboxEmpty = Resources.Load<Texture2D> ("Icons/checkbox_filled");
        internal readonly static Texture2D CheckboxFilled = Resources.Load<Texture2D> ("Icons/checkbox_empty");

        internal readonly static Texture2D Checked = Resources.Load<Texture2D> ("Icons/checked");
        internal readonly static Texture2D Checkbox = Resources.Load<Texture2D> ("Icons/checkBox");

        internal readonly static Texture2D Checkmark = Resources.Load<Texture2D> ("Icons/checkmark");
        #endregion

        internal readonly static Texture2D Banner = Resources.Load<Texture2D> ("HierarchyDecoratorLogo");
        }

    internal static class Style
        {
        public static readonly GUIStyle foldoutHeaderStyle;
        public static readonly GUIStyle tabBackgroundStyle;
        public static readonly GUIStyle listControlStyle;

        static Style()
            {
            foldoutHeaderStyle = new GUIStyle (EditorStyles.foldout)
                {
                stretchHeight = true,

                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleLeft,

                };

            tabBackgroundStyle = new GUIStyle (EditorStyles.helpBox)
                {
                stretchHeight = true,
                fixedHeight = 0,

                alignment = TextAnchor.MiddleLeft
                };

            listControlStyle = new GUIStyle (EditorStyles.centeredGreyMiniLabel)
                {
                fontSize = 28,
                };
            }
        }
    }
