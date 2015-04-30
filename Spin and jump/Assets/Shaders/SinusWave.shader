Shader "Custom/SinusWave" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_CMult ("Multiplier", Color) = (1.0,1.0,1.0,1.0)
	_Speed ("Swirl Speed", Float) = 4.0
	_Tiles ("Tiles", Float) = 2.0
	_Frequ ("Frequency", Float) = 2.0
	_Ampli ("Amplitude", Float) = 0.2
	_Funky ("Funky Magic", Float) = 0.987
}

SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 100
	
	Pass {  
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _CMult;
			float _Speed;
			float _Frequ;
			float _Ampli;
			float _Tiles;
			float _Funky;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				half2 uv = i.texcoord;

				float t = _Time * _Speed;

				uv *= _Tiles;
    
				uv += half2(
					_Ampli * sin(t/2.0) * cos(t + uv.y*_Frequ),
					_Ampli * cos(t/2.0 + cos(t*_Funky)) * sin(t + uv.x*_Frequ) );

				// Translate
				uv += half2(-t*0.1, t*0.3);

				fixed4 col = tex2D(_MainTex, uv) * _CMult;

				// Specular highlight
				//uv *= half2(4.0,4.0);
				col += 0.025 * cos(t + uv.y + uv.x);

				return col;
			}
		ENDCG
	}
}

}
