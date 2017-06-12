Shader "Custom/TestColor" {
	Properties{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_LightRate("Light Rate", Range(0,1)) = 0.5
		_StartColor("Start Color", Color) = (1, 0, 0, 1)
		_EndColor("End Color", Color) = (0, 1, 1, 1)
	}

	SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf BasicDiffuse  

		sampler2D _MainTex;
		fixed _LightRate;
		fixed4 _StartColor;
		fixed4 _EndColor;

		struct Input {
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}

		inline fixed4 LightingBasicDiffuse(SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			fixed difLight = max(0, dot(s.Normal, lightDir));
			difLight = difLight * (1 - _LightRate) + _LightRate;
			fixed3 ramp = lerp(_StartColor, _EndColor, difLight).rgb;
			fixed4 col;
			col.rgb = s.Albedo * _LightColor0.rgb * ramp;
			col.a = s.Alpha;
			return col;
		}

		ENDCG
	}
	FallBack "Diffuse"
}
