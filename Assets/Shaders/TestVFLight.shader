// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Unlit/TestVFLight"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque"}
		LOD 200

		Pass
		{
			Tags{"LightMode" = "ForwardBase"}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#pragma enable_d3d11_debug_symbols
			
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 pos: POSITION;
				float3 worldNormal: TEXCOORD1;
				float3 lightDir: TEXCOORD2;
				float3 viewDir: TEXCOORD3;
				LIGHTING_COORDS(4, 5)
			};

			struct a2v {
				float4 vertex: POSITION;
				fixed3 normal : NORMAL;
				fixed4 texcoord : TEXCOORD0;
				fixed4 color : COLOR;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (a2v v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex); // transform the vertex to projection space
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex); // get the UV coordinates
				o.worldNormal = mul(SCALED_NORMAL, (float3x3)unity_WorldToObject); // normal
				o.lightDir = mul((float3x3)unity_ObjectToWorld, ObjSpaceLightDir(v.vertex)); // light direction
				o.viewDir = mul((float3x3)unity_ObjectToWorld, ObjSpaceViewDir(v.vertex)); // view direction

				TRANSFER_VERTEX_TO_FRAGMENT(o); // shadow
				return o;
			}

			inline fixed4 LightingFragLambert(fixed4 fcol, fixed3 lightDir, fixed atten, half3 worldNormal) {
				fixed difLight = max(0, dot(normalize(worldNormal), normalize(lightDir)));
				fixed4 col;
				col.rgb = fcol.rgb * _LightColor0.rgb * (difLight * atten);
				col.a = fcol.a;

				return col;
			}

			half3 calDiffuse(half3 pos, half3 worldNormal) {
				half3 ambient = Shade4PointLights(unity_4LightPosX0, unity_4LightPosY0, unity_4LightPosZ0,
					unity_LightColor[0].rgb, unity_LightColor[1].rgb, unity_LightColor[2].rgb, unity_LightColor[3].rgb,
					unity_4LightAtten0, pos, worldNormal);
				ambient = ShadeSHPerVertex(worldNormal, ambient);
				return ShadeSHPerPixel(worldNormal, ambient, pos);
			}
			
			float4 frag (v2f i):SV_TARGET
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 fragColor = LightingFragLambert(col, i.lightDir, LIGHT_ATTENUATION(i), i.worldNormal);
				half3 diffuse = calDiffuse(i.pos, i.worldNormal);
				fragColor.rgb += col.rgb * diffuse;

				return fragColor;
			}
			ENDCG
		}
		
	}
	FallBack "Diffuse"
}
