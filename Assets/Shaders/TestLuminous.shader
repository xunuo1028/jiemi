Shader "Custom/TestLuminous" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_RimColor("Rim Color", Color) = (1, 1, 1, 1)
		_RimPower("Rim Power", Range(0.5, 8.0)) = 3.0 
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		fixed4 _RimColor;
		float _RimPower;

		struct Input {
			float2 uv_MainTex;
			float3 viewDir;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
			half rim = 1.0 - abs(dot(normalize(IN.viewDir), o.Normal));
			o.Emission = _RimColor.rgb * pow(rim, _RimPower);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
