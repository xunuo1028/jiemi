Shader "Custom/TestSpecular" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_SpecColor("Specular Color", Color) = (1, 1, 1, 1)
		_SpecPower("Specular Power", Range(0, 1)) = 0.05
		_SpecGloss("Specular Gloss", Range(0, 2)) = 1
		_SpecTex("Specular Mask", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		
		#pragma surface surf CustomBlinnPhong

		sampler2D _MainTex;
		fixed _SpecPower;
		fixed _SpecGloss;
		sampler2D _SpecTex;

		struct Input {
			float2 uv_MainTex;
			float2 uv_SpecTex;
		};

		struct CustomSurfaceOutput {
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half Specular;
			half Gloss;
			half Alpha;
			half3 SpecularColor;
		};

		void surf (Input IN, inout CustomSurfaceOutput o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			fixed4 m = tex2D(_SpecTex, IN.uv_SpecTex) * _SpecColor;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
			o.Specular = _SpecPower;
			o.Gloss = _SpecGloss;
			o.SpecularColor = m.rgb;
		}


		inline fixed4 LightingCustomBlinnPhong(CustomSurfaceOutput s, fixed3 lightDir, fixed3 viewDir, fixed atten) {
			half3 h = normalize(lightDir + viewDir);
			fixed diff = max(0, dot(s.Normal, lightDir));
			float nh = max(0, dot(s.Normal, h));
			float spec = pow(nh, s.Specular * 128.0) * s.Gloss;

			fixed4 col;
			col.rgb = s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * s.SpecularColor * spec;
			col.a = s.Alpha;
			col *= atten;

			return col;
		}

		ENDCG
	}
	FallBack "Diffuse"
}
