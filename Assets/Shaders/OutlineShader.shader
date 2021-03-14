Shader "Custom/OutlineShader" {
   Properties {
        _MainColor ("Diffuse Color", Color) = (1,1,1,1)
        _MainTex ("Base layer (RGB)", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (1,1,1,1)
 
        _Dist ("Shift", Range(0, 0.001)) = 0.0002
    }
 
    SubShader {
 
        /// first pass
     
        Tags { "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
        LOD 200
        Blend SrcAlpha OneMinusSrcAlpha
        Lighting On
        ZWrite On
        ZTest LEqual
        cull Front
        CGPROGRAM
 
        #pragma surface surf StandardSpecular fullforwardshadows addshadow alphatest:_Cutoff vertex:vert
        #pragma target 3.0
        #include "UnityCG.cginc"
 
 
        float4 _MainColor;
        float _Dist;
        float4 _OutlineColor;
     
        struct Input {
            float2 uv_MainTex;
        };
     
        void vert (inout appdata_full v) {
            v.vertex.xyz += float3(v.normal.xyz)*_Dist;    
        }
     
        void surf (Input i, inout SurfaceOutputStandardSpecular o) {
            o.Emission = _MainColor.rgb;  // main albedo color
            o.Specular =0;
            o.Smoothness = 0;
            o.Alpha = 1;
            //if (o.rgb = (1,1,1)) 
            //o.Color = _OutlineColor;
            ///////////////
        }
        ENDCG
        //// end first pass
     
        /// second pass
 
        Tags { "IgnoreProjector"="True" "RenderType"="Opaque"}
        Blend SrcAlpha OneMinusSrcAlpha
        Lighting On
        ZWrite On
        Cull Off
 
 
        CGPROGRAM
     
        #pragma surface surf StandardSpecular fullforwardshadows addshadow alphatest:_Cutoff
        #pragma target 3.0
        #include "UnityCG.cginc"
 
         sampler2D _MainTex;
 
     
        struct Input {
            float2 uv_MainTex;
            float3 norm :  TEXCOORD1;
        };
     
        void surf (Input i, inout SurfaceOutputStandardSpecular o) {
 
            // Main Albedo
            fixed4 tex = tex2D (_MainTex, i.uv_MainTex);
            o.Albedo = tex.rgb;  // main albedo
            o.Specular = 0;
            o.Smoothness = 0;
            o.Alpha = tex.a;
            //o.Color *= _OutlineColor;
            
            //o.rgb = (0,0,0);
        }
        ENDCG
        // end first pass
 
  } //subshader
}//shader