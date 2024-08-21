using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFSW.RetroFXUltimate
{
    /// <summary>A color palette to be used by RetroFX.</summary>
    [ExecuteInEditMode]
    [CreateAssetMenu(fileName = "Untitled Palette", menuName = "RetroFX/Palette", order = 1)]
    public class Palette : ScriptableObject
    {
        public const int MAX_PALETTE_SIZE = 128;

        /// <summary>The different colors in the palette (max 128).</summary>
        public Color[] ColorPalette
        {
            get { return _ColorPalette; }
            set { if (value.Length <= MAX_PALETTE_SIZE) { _ColorPalette = value; } }
        }
        [SerializeField]
        [HideInInspector]
        private Color[] _ColorPalette = new Color[MAX_PALETTE_SIZE / 4];

        /// <summary>Populates the color palette with the NES palette.</summary>
        public void PopulateNESPalette()
        {
            _ColorPalette = new Color[] { new Color(124, 124, 124), new Color(0, 0, 252), new Color(0, 0, 188), new Color(68, 40, 188)
                                    , new Color(148, 0, 132), new Color(168, 0, 32), new Color(168, 16, 0), new Color(136, 20, 0)
                                    , new Color(80, 48, 0), new Color(0, 120, 0), new Color(0, 104, 0), new Color(0, 88, 0)
                                    , new Color(0, 64, 88), new Color(0, 0, 0), new Color(1, 1, 1), new Color(188, 188, 188)
                                    , new Color(0, 120, 248), new Color(0, 88, 248), new Color(104, 68, 252), new Color(216, 0, 204)
                                    , new Color(228, 0, 88), new Color(248, 56, 0), new Color(228, 92, 16), new Color(172, 124, 0)
                                    , new Color(0, 184, 0), new Color(0, 168, 0), new Color(0, 168, 68), new Color(0, 136, 136)
                                    , new Color(248, 248, 248), new Color(60, 188, 252), new Color(104, 136, 252), new Color(152, 120, 248)
                                    , new Color(248, 120, 248), new Color(248, 88, 152), new Color(248, 120, 88), new Color(252, 160, 68)
                                    , new Color(248, 184, 0), new Color(184, 248, 24), new Color(88, 216, 84), new Color(88, 248, 152)
                                    , new Color(0, 232, 216), new Color(120, 120, 120), new Color(252, 252, 252), new Color(164, 228, 252)
                                    , new Color(184, 184, 248), new Color(216, 184, 248), new Color(248, 184, 248), new Color(248, 164, 192)
                                    , new Color(240, 208, 176), new Color(252, 224, 168), new Color(248, 216, 120), new Color(216, 248, 120)
                                    , new Color(184, 248, 184), new Color(184, 248, 216), new Color(0, 252, 252), new Color(248, 216, 248)};

            for (int i = 0; i < _ColorPalette.Length; i++)
            {
                _ColorPalette[i] /= 256;
                _ColorPalette[i].a = 1;
            }
        }

        /// <summary>Populates the color palette with the C64 palette.</summary>
        public void PopulateC64Palette()
        {
            _ColorPalette = new Color[] { new Color(0, 0, 0), new Color(255, 255, 255), new Color(136, 0, 0), new Color(170, 255, 238)
                                    , new Color(204, 68, 204), new Color(0, 204, 85), new Color(0, 0, 170), new Color(238, 238, 119)
                                    , new Color(221, 136, 85), new Color(102, 68, 0), new Color(255, 119, 119), new Color(51, 51, 51)
                                    , new Color(119, 119, 119), new Color(170, 255, 102), new Color(0, 136, 255), new Color(187, 187, 187)};

            for (int i = 0; i < _ColorPalette.Length; i++)
            {
                _ColorPalette[i] /= 256;
                _ColorPalette[i].a = 1;
            }
        }

        /// <summary>Populates the color palette with a 1bit palette.</summary>
        public void Populate1BitPalette()
        {
            _ColorPalette = new Color[] { new Color(0, 0, 0, 1), new Color(1, 1, 1, 1) };
        }

        /// <summary>Populates the color palette with a grayscale palette.</summary>
        public void PopulateGrayscalePalette(int Size)
        {
            Size = Mathf.Max(2, Mathf.Min(MAX_PALETTE_SIZE, Size));
            _ColorPalette = new Color[Size];
            for (int i = 0; i < _ColorPalette.Length; i++)
            {
                _ColorPalette[i].r = _ColorPalette[i].g = _ColorPalette[i].b = i / (Size - 1f);
                _ColorPalette[i].a = 1;
            }
        }
    }
}
