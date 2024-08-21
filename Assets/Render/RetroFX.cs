using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFSW.RetroFXUltimate
{
    /// <summary>Image effect for retro style effects..</summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Image Effects/RetroFX")]
    public class RetroFX : MonoBehaviour
    {
        /// <summary>The material created for the fx.</summary>
        [HideInInspector]
        [SerializeField]
        private Material RetroMaterial;

        /// <summary>The percentage of the resolution to pixelate down to.</summary>
        public float ResolutionPercentage
        {
            get { return _ResolutionPercentage; }
            set { _ResolutionPercentage = Mathf.Max(0.1f, value); }
        }
        [SerializeField]
        [HideInInspector]
        private float _ResolutionPercentage = 1f;

        /// <summary>Match reference resolution instead of using a resolution percentage.</summary>
        public bool MatchReferenceResolution;

        /// <summary>The reference height to match after pixelation.</summary>
        public int ReferenceHeight
        {
            get { return _ReferenceHeight; }
            set { _ReferenceHeight = Mathf.Max(1, value); }
        }
        [SerializeField]
        [HideInInspector]
        private int _ReferenceHeight = 1080 / 2;

        /// <summary>The amount to boost the contrast by.</summary>
        public float ContrastBoost
        {
            get { return _ContrastBoost; }
            set { _ContrastBoost = Mathf.Max(-1, value); }
        }
        [SerializeField]
        [HideInInspector]
        private float _ContrastBoost = 0f;

        /// <summary>The amount to boost the brightness by.</summary>
        public float BrightnessBoost
        {
            get { return _BrightnessBoost; }
            set { _BrightnessBoost = Mathf.Max(-1, value); }
        }
        [SerializeField]
        [HideInInspector]
        private float _BrightnessBoost = 0f;

        /// <summary>The number of bits for each color channel.</summary>
        public int ColorDepth
        {
            get { return _ColorDepth; }
            set { _ColorDepth = Mathf.Max(1, value); }
        }
        [SerializeField]
        [HideInInspector]
        private int _ColorDepth = 8;

        /// <summary>The color palette to use.</summary>
        public Palette ColorPalette;

        /// <summary>If dithering should be enabled.</summary>
        public bool EnableDithering;

        /// <summary>If dithering should be applied to each channel seperately.</summary>
        public bool RGBDithering;

        /// <summary>If the pixelation should also be applied to the dither map.</summary>
        public bool PixelateDitherMap;

        /// <summary>Use a predefined color palette.</summary>
        public bool UsePalette;

        [SerializeField]
        [HideInInspector]
        private Camera _Camera;

        [SerializeField]
        [HideInInspector]
        private Vector4[] _PaletteColorVectors = new Vector4[0];

        /// <summary>Pushes the current palette to the GPU.</summary>
        private void PushPaletteToGPU()
        {
            if (UsePalette && ColorPalette != null)
            {
                //Converts from colors to vector4s
                if (_PaletteColorVectors == null || _PaletteColorVectors.Length != ColorPalette.ColorPalette.Length)
                {
                    _PaletteColorVectors = new Vector4[ColorPalette.ColorPalette.Length];
                    CreateMaterial();
                }
                for (int i = 0; i < _PaletteColorVectors.Length; i++) { _PaletteColorVectors[i] = ColorPalette.ColorPalette[i]; }

                //Pushes to GPU
                RetroMaterial.SetInt("_PaletteSize", ColorPalette.ColorPalette.Length);
                RetroMaterial.SetVectorArray("_Palette", _PaletteColorVectors);
            }
            else { RetroMaterial.SetInt("_PaletteSize", 2); }
        }

        /// <summary>Creates the retro material.</summary>
        private void CreateMaterial()
        {
            Shader RetroShader = Shader.Find("Hidden/ImageEffects/RetroFX");
            RetroMaterial = new Material(RetroShader);
        }

        private void OnEnable()
        {
            if (RetroMaterial == null) { CreateMaterial(); }
            if (!_Camera) { _Camera = GetComponent<Camera>(); }
        }

        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            //Passes data to the shader and performs the inversion
            float ResPercentage = MatchReferenceResolution ? ReferenceHeight / (float)_Camera.pixelHeight : _ResolutionPercentage;
            Vector2 BlockCount = new Vector2(_Camera.pixelWidth, _Camera.pixelHeight) * ResPercentage;
            Vector2 BlockSize = new Vector2(1 / BlockCount.x, 1 / BlockCount.y);
            PushPaletteToGPU();
            RetroMaterial.SetVector("_BlockCount", BlockCount);
            RetroMaterial.SetVector("_BlockSize", BlockSize);
            RetroMaterial.SetInt("qlevel", 2 << (_ColorDepth - 1));
            RetroMaterial.SetFloat("qleveld", 1.0f / (2 << (_ColorDepth - 1)));
            RetroMaterial.SetFloat("_Contrast", _ContrastBoost + 1);
            RetroMaterial.SetFloat("_Brightness", _BrightnessBoost + 1);
            RetroMaterial.SetKeyword("PIXELATE_ON", ResPercentage < 1);
            RetroMaterial.SetKeyword("DITHERING_ON", EnableDithering);
            RetroMaterial.SetKeyword("RGB_DITHERING_ON", RGBDithering);
            RetroMaterial.SetKeyword("PALETTE_ON", UsePalette && ColorPalette);
            RetroMaterial.SetInt("_PixelateDitherMap", PixelateDitherMap ? 1 : 0);
            RetroMaterial.SetFloat("_ResolutionPercentage", ResPercentage);
            Graphics.Blit(src, dest, RetroMaterial);
        }
    }

    /// <summary>Extension methods for materials.</summary>
    public static class MaterialExtension
    {
        /// <summary>Sets a keyword on a shader.</summary>
        /// <param name="Keyword">Keyword to set.</param>
        /// <param name="Enabled">If the keyword should be enabled or disabled.</param>
        public static void SetKeyword(this Material Mat, string Keyword, bool Enabled)
        {
            if (Enabled) { Mat.EnableKeyword(Keyword); }
            else { Mat.DisableKeyword(Keyword); }
        }
    }
}
