Shader "Unlit/PulseNoise"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
		_Amount("Distort",Float) = 0.0
		_Random("Random",Float) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100
		Cull off
		Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float _Amount;
			float _Random;

			float sankakuha(float syuuki, float sanpurureto)
			{
				float phase = 0.0;
				phase += syuuki / sanpurureto;
				phase -= floor(phase);

				float tr;
				if (phase > 0.5) {
					tr = 1.0 - phase;    // 0.5 より大きいので反転
				}
				else {
					tr = phase;          // そうでなければそのまま
				}
				tr = tr * 2.0; // 2倍して 0.0～1.0 の範囲に
				return 2.0 * (tr - 0.5); // -1.0～1.0 の範囲に
			}

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				float2 uv = i.uv;
				float x = 2 * uv.y;
				//uv.x += _Amount * sin(10 * x)*(-(x - 1)*(x - 1) + _Random);
				uv.x += _Amount * sankakuha(2 * x + sin(_Time.x * 100), 0.01);
                fixed4 col = tex2D(_MainTex, uv);
				if (col.a < 0.1)
					discard;
                return col;
            }
            ENDCG
        }
    }
}