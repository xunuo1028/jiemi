Shader "Custom/TestNormalMap" {
	Properties{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_NormalMap("Normal Map", 2D) = "white" {}
	}
	SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert  

		sampler2D _MainTex;
		sampler2D _NormalMap;

		struct Input {
			float2 uv_MainTex;
			half2 uv_NormalMap;
		};

		void surf(Input IN, inout SurfaceOutput o) {
			fixed2 texUV = IN.uv_MainTex;
			fixed4 c = tex2D(_MainTex, texUV);
			fixed3 n = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
			o.Normal = n;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}

		ENDCG
	}
	FallBack "Diffuse"
}
