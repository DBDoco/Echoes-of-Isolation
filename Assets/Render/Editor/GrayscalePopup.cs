using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace QFSW.RetroFXUltimate
{
    public partial class PaletteInspector
    {
        /// <summary>Popwindow that creates a grayscale palette.</summary>
        private class GrayscalePopup : PopupWindowContent
        {
            /// <summary>Width of the window in pixels.</summary>
            public const int WINDOW_WIDTH = 300;

            /// <summary>Height of the window in pixels.</summary>
            public const int WINDOW_HEIGHT = 45;

            /// <summary>The palette that this popup window belongs to.</summary>
            public PaletteInspector ParentPalette;

            /// <summary>The size of the palette to be created.</summary>
            int PaletteSize = Palette.MAX_PALETTE_SIZE / 4;

            //Custom GUIStyles
            /// <summary>GUIStyle for error messages.</summary>
            private GUIStyle ErrorStyle;
            /// <summary>GUIStyle for success messages.</summary>
            private GUIStyle SuccessStyle;

            /// <summary>Constructs a new popup window for creating a grayscale palette.</summary>
            /// <param name="ParentPalette">The palette that this popup window belongs to.</param>
            public GrayscalePopup(PaletteInspector ParentPalette) { this.ParentPalette = ParentPalette; }

            //Forces window size
            public override Vector2 GetWindowSize() { return new Vector2(WINDOW_WIDTH, WINDOW_HEIGHT); }

            //Draws window
            public override void OnGUI(Rect DrawRect)
            {
                //Gets size
                PaletteSize = Mathf.Min(Palette.MAX_PALETTE_SIZE, Mathf.Max(2, EditorGUILayout.IntField(new GUIContent("Size", "Size of the grayscale palette to create."), PaletteSize)));

                //Create preset button
                if (GUILayout.Button("Create Grayscale Palette"))
                {
                    ParentPalette.PaletteInstance.PopulateGrayscalePalette(PaletteSize);
                    ParentPalette.PushPaletteColors();
                }
            }
        }
    }
}
