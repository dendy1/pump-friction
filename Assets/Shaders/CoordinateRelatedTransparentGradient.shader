Shader "Unlit/CoordinateRelatedTransparentGradient"
{
    Properties
    {
        
        _MainColor ("Default color RGB", Color) = (0,0.5,1,0)
        _CullOffIntensity ("Dissolve intensity", float) = 0
        
        _MainTex ("Color (RGB) Alpha (A)", 2D) = "white"
        _PivotVectorForTransparency ("Transparency Pivot", Vector) = (0,0,0)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

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
                float4 worldPos : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _CullOffIntensity;
            half4 _MainColor;
            Vector _PivotVectorForTransparency;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float transparencyFac = (_PivotVectorForTransparency.z - i.worldPos.z + _PivotVectorForTransparency.x - i.worldPos.x + _PivotVectorForTransparency.y - i.worldPos.y) /3;
                fixed4 col = tex2D(_MainTex, i.uv) * _MainColor;
                UNITY_APPLY_FOG(i.fogCoord, col);
                col.a = tex2D (_MainTex, i.uv).a;
                col.a /= pow(transparencyFac, 4) * _CullOffIntensity;
                return col;
            }
            ENDCG
        }
    }
}
