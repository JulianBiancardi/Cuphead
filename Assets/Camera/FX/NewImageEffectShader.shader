Shader "Custom/AlphaFromBlack"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue"="Transparent" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 col = tex2D(_MainTex, i.uv);
                // Comprueba si el color es negro
                if (col.r < 0.1 && col.g < 0.1 && col.b < 0.1)
                {
                    // Deja intactos los píxeles negros
                    return col;
                }
                else
                {
                    // Establece el valor alfa a 0 en otros casos
                    return half4(col.r, col.g, col.b, 0);
                }
            }
            ENDCG
        }
    }
}