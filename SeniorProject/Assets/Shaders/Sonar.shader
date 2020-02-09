Shader "Unlit/Sonar"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		
        [Header(Wave)]
		_WaveActive("Activate Wave", Float) = 0.0
		_WaveAlpha("Wave Alpha", Float) = 0.0
        _WaveDistance ("Distance from player", float) = 10
        _WaveTrail ("Length of the trail", Range(0,5)) = 1
        _WaveColor ("Color", Color) = (1,0,0,1)
    }
    SubShader
    {
        Tags { 
			"Queue" = "Transparent"
			"RenderType" = "Opaque" 
		}
		
		ZTest Always
		Blend One One

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
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
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			
            sampler2D _CameraDepthTexture;
			float _WaveActive;
			float _WaveDistance;
            float _WaveTrail;
            float _WaveAlpha;
            float4 _WaveColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                UNITY_TRANSFER_FOG(o,o.vertex);
				o.uv = UnityStereoTransformScreenSpaceTex(v.uv);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				fixed4 source = tex2D(_MainTex, i.uv);
				fixed4 col = source;

				if(_WaveActive == 1.0f){
					float depthVal = tex2D(_CameraDepthTexture, i.uv).r;
					//linear depth between camera and far clipping plane
					depthVal = Linear01Depth(depthVal);
					//depth as distance from camera in units 
					depthVal = depthVal * _ProjectionParams.z;

					//get source color
					//skip wave and return source color if we're at the skybox
					if(depthVal >= _ProjectionParams.z)
						return source;

					//calculate wave
					//_WaveColor = (_WaveColor.x, _WaveColor.y, _WaveColor.z, _WaveColor.w * _WaveAlpha); 
					float waveFront = step(depthVal, _WaveDistance);
					float waveTrail = smoothstep(_WaveDistance - _WaveTrail, _WaveDistance, depthVal);
					float wave = waveFront * waveTrail;

					//mix wave into source color
					col = lerp(source, _WaveColor, wave);
				}
                return _WaveColor;
            }
            ENDCG
        }
    }
	


}
