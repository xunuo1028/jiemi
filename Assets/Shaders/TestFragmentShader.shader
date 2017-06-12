Shader "Unlit/TestFragmentShader"
{
	Properties
	{
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Luminosity("GrayScale Amount", Range(0, 1)) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img // 使用了方法名为vert_img的顶点着色器
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			
			#include "UnityCG.cginc"

/*			struct appdata_img
			{
				float4 vertex : POSITION;
				half2 texcoord : TEXCOORD0;
			};

			struct v2f_img
			{
				half2 uv : TEXCOORD0;
				float4 pos : SV_POSITION;
			};
	
			v2f_img vert_img (appdata_img v)
			{
				v2f_img o;
				o.pos = mul(UNITY_MARIX_MVP, v.vertex);
				o.uv = v.texcoord;
				return o;
			}
*/		
			uniform sampler2D _MainTex;
			fixed _Luminosity;

/*			struct Input{
				float2 uv_MainTex;
			};
*/	
			fixed4 frag (v2f_img i) : COLOR
			{

				fixed4 renderTex = tex2D(_MainTex, i.uv);
				half lum = 0.299 * renderTex.r + 0.587 * renderTex.g + 0.114 * renderTex.b;
				fixed4 finalColor = lerp(renderTex, lum, _Luminosity);

				return finalColor;
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}
