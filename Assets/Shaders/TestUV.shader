Shader "Custom/TestUV" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_CellAmount("Cell Amount", float) = 1
		_Speed("Speed", Range(0.01, 32)) = 12
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
	
		#pragma surface surf Lambert

		sampler2D _MainTex;
		half _CellAmount; // 动作的数量
		half _Speed; // 播放的速度

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			fixed2 texUV = IN.uv_MainTex;
			half cellUVX = 1 / _CellAmount;
			half timeVal = ceil(fmod(_Time.y * _Speed, _CellAmount));
			fixed uvx = texUV.x;
			uvx *= cellUVX;
			uvx += timeVal * cellUVX;
			texUV = fixed2(uvx, texUV.y);
			fixed4 c = tex2D (_MainTex, texUV);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
