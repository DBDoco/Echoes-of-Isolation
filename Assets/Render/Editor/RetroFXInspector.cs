using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace QFSW.RetroFXUltimate
{
    /// <summary>Custom inspector for the RetroFX effect.</summary>
    [CustomEditor(typeof(RetroFX))]
    public class RetroFXInspector : Editor
    {
        /// <summary>RFXU Banner.</summary>
        [SerializeField]
        private Texture2D Banner;

        /// <summary>The color depth.</summary>
        private SerializedProperty ColorDepth;

        /// <summary>The percentage of the resolution to pixelate down to.</summary>
        private SerializedProperty ResolutionPercentage;

        /// <summary>Match reference resolution instead of using a resolution percentage.</summary>
        private SerializedProperty MatchReferenceResolution;

        /// <summary>The reference height to match after pixelation.</summary>
        private SerializedProperty ReferenceHeight;

        /// <summary>The amount to boost the contrast by.</summary>
        private SerializedProperty ContrastBoost;

        /// <summary>The amount to boost the brightness by.</summary>
        private SerializedProperty BrightnessBoost;

        /// <summary>If dithering should be enabled.</summary>
        private SerializedProperty EnableDithering;

        /// <summary>If dithering should be applied to each channel seperately.</summary>
        private SerializedProperty RGBDithering;

        /// <summary>Use a predefined color palette.</summary>
        private SerializedProperty UsePalette;

        /// <summary>The color palette to use.</summary>
        private SerializedProperty ColorPalette;

        /// <summary>If the pixelation should also be applied to the dither map.</summary>
        private SerializedProperty PixelateDitherMap;


        //Initialises inspector
        private void OnEnable()
        {
            //Caches serialised properties
            ColorDepth = serializedObject.FindProperty("_ColorDepth");
            ResolutionPercentage = serializedObject.FindProperty("_ResolutionPercentage");
            MatchReferenceResolution = serializedObject.FindProperty("MatchReferenceResolution");
            ReferenceHeight = serializedObject.FindProperty("_ReferenceHeight");
            ContrastBoost = serializedObject.FindProperty("_ContrastBoost");
            BrightnessBoost = serializedObject.FindProperty("_BrightnessBoost");
            EnableDithering = serializedObject.FindProperty("EnableDithering");
            RGBDithering = serializedObject.FindProperty("RGBDithering");
            UsePalette = serializedObject.FindProperty("UsePalette");
            ColorPalette = serializedObject.FindProperty("ColorPalette");
            PixelateDitherMap = serializedObject.FindProperty("PixelateDitherMap");
        }

        //Draws RetroFX inspector
        public override void OnInspectorGUI()
        {
            //Banner display
            Rect BannerRect = GUILayoutUtility.GetRect(0.0f, 0.0f);
            BannerRect.height = Screen.width * 360f / 1600;
            GUILayout.Space(BannerRect.height);
            GUI.Label(BannerRect, Banner);

            serializedObject.Update();

            //Resolution and color depth
            if (!UsePalette.boolValue) { ColorDepth.intValue = Mathf.Max(1, EditorGUILayout.IntField(new GUIContent("Color Depth", "The number of bits for each color channel."), ColorDepth.intValue)); }
            MatchReferenceResolution.boolValue = EditorGUILayout.Toggle(new GUIContent("Match Reference Resolution", "Match reference resolution instead of using a resolution percentage."), MatchReferenceResolution.boolValue);
            if (MatchReferenceResolution.boolValue) { ReferenceHeight.intValue = EditorGUILayout.IntField(new GUIContent("Reference Height", "The reference height to match after pixelation."), ReferenceHeight.intValue); }
            else { ResolutionPercentage.floatValue = EditorGUILayout.Slider(new GUIContent("Resolution Percentage", "The percentage of the resolution to pixelate down to."), ResolutionPercentage.floatValue, 0.1f, 1f); }

            //Color adjustments
            ContrastBoost.floatValue = EditorGUILayout.Slider(new GUIContent("Contrast Boost", "The amount to boost the contrast by."), ContrastBoost.floatValue, -1f, 3f);
            BrightnessBoost.floatValue = EditorGUILayout.Slider(new GUIContent("Brightness Boost", "The amount to boost the brightness by."), BrightnessBoost.floatValue, -1f, 3f);

            //Dithering
            EnableDithering.boolValue = EditorGUILayout.Toggle(new GUIContent("Enable Dithering", "If dithering should be enabled."), EnableDithering.boolValue);
            if (EnableDithering.boolValue) { PixelateDitherMap.boolValue = EditorGUILayout.Toggle(new GUIContent("Pixelate Dither Map", "If the pixelation should also be applied to the dither map."), PixelateDitherMap.boolValue); }
            if (!UsePalette.boolValue && EnableDithering.boolValue) { RGBDithering.boolValue = EditorGUILayout.Toggle(new GUIContent("RGB Dithering", "If dithering should be applied to each channel seperately."), RGBDithering.boolValue); }

            //Palette settings
            UsePalette.boolValue = EditorGUILayout.Toggle(new GUIContent("Use Palette", "Use a predefined color palette."), UsePalette.boolValue);
            if (UsePalette.boolValue) { ColorPalette.objectReferenceValue = EditorGUILayout.ObjectField(new GUIContent("Palette", "The color palette to use"), ColorPalette.objectReferenceValue, typeof(Palette), false); }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
