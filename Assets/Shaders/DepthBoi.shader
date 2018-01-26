﻿Shader "DepthMaster/DepthBoi" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_Tex ("Texture", 2D) = "white" {}
		_DepthFactor ("Depth Factor", Range(0.0, 1024.0)) = 256.0
		[MaterialToggle] _FlipDepth("Flip Depth", Int) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf DepthBoi fullforwardshadows vertex:vert finalcolor:finalcolor 
		#pragma target 3.0

		fixed4 _Color;
		sampler2D _Tex;
		float _DepthFactor;
		int _FlipDepth;

		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		struct Input {
			float2 uv_Tex;
			float3 worldPos : TEXCOORD0;
		};

		void vert (inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT(Input, o)
			o.worldPos = mul(UNITY_MATRIX_M, v.vertex).xyz;
		}

		void surf (Input IN, inout SurfaceOutput o) {
			float4 tex = tex2D(_Tex, IN.uv_Tex);
			float depth = (length(IN.worldPos - _WorldSpaceCameraPos) / _DepthFactor);
			depth = pow(depth, 0.5);
			if(_FlipDepth)
				depth = (1.0 - depth) * 1.2;
			o.Albedo = depth * tex.rgb;
			o.Alpha = tex.a;
		}

		void finalcolor (Input IN, SurfaceOutput o, inout fixed4 color) {
			color.rgb *= _Color.rgb;
		}

		half4 LightingDepthBoi (SurfaceOutput s, half3 lightDir, half atten) {
			half NdotL = dot (s.Normal, lightDir);
			half diff = NdotL * 0.5 + 0.5;
			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * (diff * atten);
			c.a = s.Alpha;
			return c;
		}
		ENDCG
	}
	FallBack "Diffuse"
}