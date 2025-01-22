Shader "Custom/CharactersV4Players"{
    Properties
    {
        [PerRendererData] _MainTex("Texture", 2D) = "white" {}      
        _EffectColor1("Effect Color", Color) = (1,1,1,1)
        _Crossfade("Fade", float) = 0
        _FlashColor("Flash Color", Color) = (1,1,1,1)
        _FlashAmount("Flash Amount",Range(0.0,1.0)) = 0
        _Cutoff("Alpha Cutoff", Range(0,1)) = 0.9
      
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "TransparentCutOut"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }

      
        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha


        CGPROGRAM
            #pragma surface surf Lambert alpha:blend fullforwardshadows addshadow alphatest:_Cutoff  

            #pragma target 3.0
      
            struct Input {
                fixed2 uv_MainTex;
                fixed4 color : COLOR;
            };

            sampler2D _MainTex;
            fixed4 _EffectColor1;
            fixed _Crossfade;
            fixed4 _FlashColor;
            float _FlashAmount;

            void surf(Input IN, inout SurfaceOutput o)
            {
                fixed4 col = tex2D(_MainTex, IN.uv_MainTex);
                fixed4 returnColor = lerp(col, col * _EffectColor1, _Crossfade) * _EffectColor1.a + col * (1.0 - _EffectColor1.a);

                o.Albedo = returnColor.rgb * IN.color.rgb;
                o.Alpha = col.a * IN.color.a;

                o.Albedo = lerp(o.Albedo,_FlashColor.rgb,_FlashAmount);
              
            }
        ENDCG
    }
    Fallback "Legacy Shaders/Transparent/Cutout/VertexLit"
}