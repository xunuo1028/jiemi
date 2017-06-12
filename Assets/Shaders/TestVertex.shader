Shader "Custom/TestVertex" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
        _NormalYScale ("Normal Y Scale", Range(-2, 2)) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM

        #pragma surface surf Lambert vertex:vert

        sampler2D _MainTex;
        fixed _NormalYScale;

		struct Input {
			float2 uv_MainTex;
            float4 vertColor;
		};

        void vert(inout appdata_full v, out Input o) {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.vertColor = v.color;
            v.normal = half3(v.normal.x, v.normal.y * _NormalYScale, v.normal.z);
        }

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
