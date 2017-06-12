Shader "Unlit/TestBlend"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_BlendTex("Blend Texture", 2D) = "white" {}
		_Opacity("Blend Opacity", Range(0, 1)) = 1
		[Space(50)]
		[Header(MaterialPropertyDrawer)]
		[KeywordEnum(None, Add, Multiply, Screen)] _BlendMode("Blend Mode", Float) = 0
		[Space]
		[Toggle] _Invert("Invert?", Float) = 0
		[Toggle(ENABLE_DOUBLE)] _Double("Double?", Float) = 0
		[PowerSlider(3.0)] _Power("Power", Range(1, 1024)) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			sampler2D _BlendTex;
			fixed _Opacity;
			int _BlendMode;
			bool _Invert;
			bool _Double;

			fixed4 getBlended(fixed4 renderTex, fixed4 blendTex)
			{
				fixed4 blended;

				switch (_BlendMode) {
				case 0:
					blended = renderTex;
					break;
				case 1:
					blended = renderTex + blendTex;
					break;
				case 2:
					blended = renderTex * blendTex;
					break;
				case 3:
					blended = (1.0 - (1.0 - renderTex) * (1.0 - blendTex));
					break;

				}

				if (_Invert) {
					blended = 1 - blended;
				}
				if (_Double) {
					blended *= 2;
				}

				return blended;
			}
			
			fixed4 frag (v2f_img i) : COLOR
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 blendTex = tex2D(_BlendTex, i.uv);
				fixed4 blended = getBlended(col, blendTex);

				col = lerp(col, blended, _Opacity);
				return col;
			}

			ENDCG
		}
	}
}
