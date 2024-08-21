using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace QFSW.RetroFXUltimate
{
    /// <summary>Custom inspector for a color palette.</summary>
    [CustomEditor(typeof(Palette))]
    public partial class PaletteInspector : Editor
    {
        /// <summary>RFXU Banner.</summary>
        [SerializeField]
        private Texture2D Banner;

        /// <summary>The Palette that this inspector is displaying.</summary>
        public Palette PaletteInstance { get; private set; }

        /// <summary>The different colors in the palette (max 32).</summary>
        private SerializedProperty ColorPalette;
        private int PaletteSize;

        //Initialises inspector
        private void OnEnable()
        {
            //Retrieves the Palette
            PaletteInstance = (Palette)target;
            PaletteSize = PaletteInstance.ColorPalette.Length;

            //Caches serialised properties
            ColorPalette = serializedObject.FindProperty("_ColorPalette");
        }

        /// <summary>Pushes new palette colors</summary>
        public void PushPaletteColors()
        {
            PaletteSize = PaletteInstance.ColorPalette.Length;
            EditorUtility.SetDirty(PaletteInstance);
            serializedObject.ApplyModifiedProperties();
        }

        //Draws Palette inspector
        public override void OnInspectorGUI()
        {
            //Banner display
            Rect BannerRect = GUILayoutUtility.GetRect(0.0f, 0.0f);
            BannerRect.height = Screen.width * 360f / 1600;
            GUILayout.Space(BannerRect.height);
            GUI.Label(BannerRect, Banner);

            serializedObject.Update();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(new GUIContent("Presets: ", "Various different palette presets"));
            if (GUILayout.Button(new GUIContent("NES", "Creates the color palette used by the Nintendo Entertainment System / Famicon."), EditorStyles.miniButton, GUILayout.Height(16), GUILayout.Width(35)))
            {
                PaletteInstance.PopulateNESPalette();
                PushPaletteColors();
            }
            if (GUILayout.Button(new GUIContent("C64", "Creates the color palette used by the Commodore 64."), EditorStyles.miniButton, GUILayout.Height(16), GUILayout.Width(35)))
            {
                PaletteInstance.PopulateC64Palette();
                PushPaletteColors();
            }
            if (GUILayout.Button(new GUIContent("1 Bit", "Creates a 1 Bit color palette."), EditorStyles.miniButton, GUILayout.Height(16), GUILayout.Width(35)))
            {
                PaletteInstance.Populate1BitPalette();
                PushPaletteColors();
            }
            if (GUILayout.Button(new GUIContent("Grayscale", "Creates grayscale color palette."), EditorStyles.miniButton, GUILayout.Height(16), GUILayout.Width(65)))
            {
                PopupWindow.Show(new Rect((Screen.width - GrayscalePopup.WINDOW_WIDTH) / 2, 50, 0, 0), new GrayscalePopup(this));
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            PaletteSize = Mathf.Min(Palette.MAX_PALETTE_SIZE, Mathf.Max(1, EditorGUILayout.IntField(new GUIContent("Palette Size", "Number of different colors in the palette (max 128)"), PaletteSize)));
            GUI.enabled = PaletteSize > 2;
            if (GUILayout.Button(new GUIContent("-", "Reduces the palette size by 1."), EditorStyles.miniButton, GUILayout.Height(16), GUILayout.Width(17))) { PaletteSize--; }
            GUI.enabled = PaletteSize < Palette.MAX_PALETTE_SIZE;
            if (GUILayout.Button(new GUIContent("+", "Increases the palette size by 1."), EditorStyles.miniButton, GUILayout.Height(16), GUILayout.Width(17))) { PaletteSize++; }
            GUI.enabled = PaletteSize != PaletteInstance.ColorPalette.Length;
            if (GUILayout.Button(new GUIContent("Reset", "Resets to the palette's current size."), EditorStyles.miniButton, GUILayout.Height(16), GUILayout.Width(45))) { PaletteSize = PaletteInstance.ColorPalette.Length; }
            if (GUILayout.Button(new GUIContent("Apply", "Applies the new size."), EditorStyles.miniButton, GUILayout.Height(16), GUILayout.Width(45)))
            {
                while (PaletteSize < ColorPalette.arraySize) { ColorPalette.DeleteArrayElementAtIndex(ColorPalette.arraySize - 1); }
                while (PaletteSize > ColorPalette.arraySize) { ColorPalette.InsertArrayElementAtIndex(ColorPalette.arraySize - 1); }
            }
            EditorGUILayout.EndHorizontal();
            GUI.enabled = true;

            for (int i = 0; i < ColorPalette.arraySize; i++)
            {
                SerializedProperty Col = ColorPalette.GetArrayElementAtIndex(i);
                Col.colorValue = EditorGUILayout.ColorField(Col.colorValue);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
