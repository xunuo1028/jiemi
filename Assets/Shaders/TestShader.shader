Shader "Custom/TestShader" {
	Properties{ //包含shader的属性
		_MainTex("Albedo (RGB)", 2D) = "white" {} //_MainTex表示变量名，Albedo(RGB)是在编辑器里显示的名称，2D是他的类型，表示他是一个纹理，white是默认值
	}
	SubShader{ // 可以包含多个subShader，计算着色的时候平台会按顺序选择一个可以使用的子着色器进行执行
		Tags{  // 标记了着色器的一些特性
			"RenderType" = "Opaque" // 渲染类型-不透明
		}
		LOD 200 // Level of details着色器的细节层级

		CGPROGRAM // 此内是一段C for graphics代码
	
		#pragma surface surf Lambert // 表明使用的是一个表面着色器，方法名称是surf，光照模型是Lambert

		sampler2D _MainTex; // 对应于propertie里面的2D，是2D贴图的数据结构，_MainTex对应于Properties里面的_MainTex，保存了编辑器里设置的贴图。二者必须同名才能将贴图数据连接起来。（下面的_MainTex是上面的_MainTex在Cg代码里的代理）

		struct Input { // 为surf方法定义了输入参数的数据结构
			float2 uv_MainTex; // float2-二维的浮点型坐标，uv_MainTex表示_MainTex的纹理坐标
		};

		void surf(Input IN, inout SurfaceOutput o) {

			fixed4 c = tex2D(_MainTex, IN.uv_MainTex); // 使用tex2D方法从_MainTex里取出指定纹理坐标IN.uv_MainTex的色彩值
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse" // 如果所有的subshader都无法使用则会执行这个指定的着色器
}

