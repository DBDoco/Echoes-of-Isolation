Shader "Hidden/ImageEffects/RetroFX"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Cull Off ZWrite Off ZTest Always
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
            #pragma target 3.0
            #pragma multi_compile DITHERING_OFF DITHERING_ON
            #pragma multi_compile RGB_DITHERING_OFFF RGB_DITHERING_ON
            #pragma multi_compile PALETTE_OFF PALETTE_ON
            #pragma multi_compile PIXELATE_OFF PIXELATE_ON
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
                float4 screenpos : TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
                o.screenpos = ComputeScreenPos(o.vertex);
				return o;
			}

            #define MAX_PALETTE_SIZE 128
			sampler2D _MainTex;
			extern const uniform uint qlevel;
            extern const uniform half qleveld;
			extern const uniform float2 _BlockCount;
			extern const uniform float2 _BlockSize;
			extern const uniform float _Contrast;
			extern const uniform float _Brightness;
			extern const uniform float _ResolutionPercentage;
			extern const uniform int _PaletteSize;
			extern const uniform int _PixelateDitherMap;

			#if DITHERING_ON
            static const int indexMatrix8x8[64] = { 1,  32, 8,  40, 2,  34, 10, 42,
                                                    48, 16, 56, 24, 50, 18, 58, 26,
                                                    12, 44, 4,  36, 14, 46, 6,  38,
                                                    60, 28, 52, 20, 62, 30, 54, 22,
                                                    3,  35, 11, 43, 1,  33, 9,  41,
                                                    51, 19, 59, 27, 49, 17, 57, 25,
                                                    15, 47, 7,  39, 13, 45, 5,  37,
                                                    63, 31, 55, 23, 61, 29, 53, 21 };
			#endif


			#if PALETTE_ON
			extern const uniform half3 _Palette[MAX_PALETTE_SIZE];
			#else
			half3 _Palette[2];
			#endif

            inline uint2 getPixelCoords(v2f i)
            {
                float2 pos = i.screenpos.xy / i.screenpos.w;
                pos *= _ScreenParams.xy;
                return (uint2)pos;
            }

			#if DITHERING_ON
            inline half getIndexValue(v2f i)
            {
				uint2 intpos = getPixelCoords(i);
                #if PIXELATE_ON
				[flatten] if (_PixelateDitherMap) { intpos *= _ResolutionPercentage; }
                #endif
                uint x = intpos.x % 8;
				uint y = intpos.y % 8;
                return (indexMatrix8x8[x + y * 8] / 64.0);
            }
			#endif

            inline half getRGBDistanceSquared(half3 col1, half3 col2)
            {
                half dR = col1.r - col2.r;
                half dG = col1.g - col2.g;
                half dB = col1.b - col2.b;
                return 2 * dR * dR + 4 * dG * dG + 3 * dB * dB;
            }

            half3 getClosestColor(half3 col, uniform int paletteSize)
            {
                half3 temp;
                half3 closest = half3(-20, 0, 0);
                half closestDistance = 1000000;
                for (int i = 0; i < paletteSize; ++i)
                {
                    temp = _Palette[i];
                    float tempDistance = getRGBDistanceSquared(temp, col);
					[flatten] if (tempDistance < closestDistance)
					{
						closest = temp;
						closestDistance = tempDistance;
					}
                }
                return closest;
            }

            void getClosestTwoColors(half3 col, uniform int paletteSize, out half3 out1, out half3 out2)
            {
                half3 temp;
                half closestDistance1 = 1000000;
                half closestDistance2 = 1000000;
				half3 closest1 = { 0, 0, 0 };
				half3 closest2 = { 0, 0, 0 };
				#if PALETTE_ON
				for (int i = 0; i < paletteSize; ++i)
				#else
				[unroll(2)] for (int i = 0; i < 2; ++i)
				#endif
                {
                    temp = _Palette[i];
                    float tempDistance = getRGBDistanceSquared(temp, col);
					[flatten] if (tempDistance < closestDistance1)
					{
						closest2 = closest1;
						closest1 = temp;
						closestDistance2 = closestDistance1;
						closestDistance1 =  tempDistance;
					}
					else if (tempDistance < closestDistance2)
					{
						closest2 = temp;
						closestDistance2 = tempDistance;
					}
                }
                out1 = closest1;
                out2 = closest2;
            }

			#if DITHERING_ON
            half ditherChannel(v2f i, half col, half qcol1, half qcol2)
            {
                bool low = col - qcol1 < qcol2 - col;
                half closestColor = low ? qcol1 : qcol2;
                half secondClosestColor = low ? qcol2 : qcol1;
                half d = getIndexValue(i);
                half dist = abs(closestColor - col);
				[flatten] if (dist < d) { return closestColor; }
				else { return secondClosestColor; }
            }

            half3 ditherRGB(v2f i, half3 col, half3 qcol)
            {
                half3 qcol1 = qcol;
                half3 qcol2 = qcol + qleveld;
                half3 rgb;
                rgb.r = ditherChannel(i, col.r, qcol1.r, qcol2.r);
                rgb.g = ditherChannel(i, col.g, qcol1.g, qcol2.g);
                rgb.b = ditherChannel(i, col.b, qcol1.b, qcol2.b);
                return rgb;
            }

            half3 ditherPalette(v2f i, half3 col)
            {
                half3 closestCol;
                half3 secondClosestCol;
                getClosestTwoColors(col, _PaletteSize, closestCol, secondClosestCol);
                float d = getIndexValue(i);
                half dist1 = sqrt(getRGBDistanceSquared(closestCol, col));
                half dist2 = sqrt(getRGBDistanceSquared(secondClosestCol, col));
                half dist =  dist1 / (dist1 + dist2);

				[flatten] if (dist < d) { return closestCol; }
				else { return secondClosestCol; }
            }

			#if PALETTE_OFF
            half3 ditherColor(v2f i, half3 col, half3 qcol)
            {
                _Palette[0] = qcol;
                _Palette[1] = qcol + qleveld;
                return ditherPalette(i, col);
            }
			#endif
			#endif

            inline half3 paletteSnap(half3 col)
            {
                return getClosestColor(col, _PaletteSize);
            }


			half3 frag (v2f i) : SV_Target
			{
                #if PIXELATE_ON
                    float2 BlockPos = floor(i.uv * _BlockCount);
                    float2 BlockCenter = BlockPos * _BlockSize + _BlockSize * 0.5;
    				half3 col = tex2D(_MainTex, BlockCenter);
                #else
                    half3 col = tex2D(_MainTex, i.uv);
                #endif

                col = (col - 0.5f) * _Contrast + 0.5f;
                col *= _Brightness;

                #if PALETTE_ON
                    #if DITHERING_ON
                        return ditherPalette(i, col);
                    #else
                        return paletteSnap(col);
                    #endif
                #else
                    half3 qcol = col;
                    qcol *= qlevel;
                    qcol = (uint3)qcol;
                    qcol /= qlevel;

                    #if DITHERING_ON
                        #if RGB_DITHERING_ON
                            return ditherRGB(i, col, qcol);
                        #else
                            return ditherColor(i, col, qcol);
                        #endif
                    #else
                        return qcol;
                    #endif
                #endif
			}
			ENDCG
		}
	}
}
