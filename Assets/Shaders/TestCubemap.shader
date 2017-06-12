Shader "Custom/TestCubemap" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MainTint("Main Tint", Color) = (1, 1, 1, 1)
		_CubeMap("CubeMap", CUBE) = "" {}
		_ReflAmount("Reflection Amount", Range(0.01, 1)) = 0.5
		_ReflMask("Reflection Mask", 2D) = "" {}
		_NormalMap("Normal Map", 2D) = "bump" {}
		_RimPower("Fresnel Falloff", Range(0.1, 3)) = 2
		_SpecColor("Specular Color", Color) = (1, 0, 0, 1)
		_SpecPower("Specular Power", Range(0, 1)) = 0.2
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		
		#pragma surface surf Lambert

		sampler2D _MainTex;
		fixed4 _MainTint;
		samplerCUBE _CubeMap;
		fixed _ReflAmount;
		sampler2D _ReflMask;
		sampler2D _NormalMap;
		half _RimPower;
		fixed _SpecPower;

		struct Input {
			float2 uv_MainTex;
			float2 uv_NormalMap;
			float3 worldRefl; // 世界空间的反射向量
			float3 viewDir;
			INTERNAL_DATA
		};

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _MainTint;
			fixed3 n = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap)).rgb;
			o.Normal = n;
			fixed3 r = texCUBE(_CubeMap, WorldReflectionVector(IN, o.Normal)).rgb;

			half rim = saturate(dot(o.Normal, normalize(IN.viewDir)));
			rim = pow(rim, _RimPower);
			//fixed4 m = tex2D(_ReflMask, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
			o.Emission = r * _ReflAmount * rim;
			o.Specular = _SpecPower;
			o.Gloss = 1.0;
			
		}
		ENDCG
	}
	FallBack "Diffuse"
}
