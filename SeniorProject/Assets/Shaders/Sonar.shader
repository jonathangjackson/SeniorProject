Shader "Unlit/Sonar"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_WaveActive("Activate Wave", Float) = 0.0
		_WaveDistance("Wave Distance", Float) = 1.0
		_WaveTrail("Wave Trail", Float) = 1.0
		_WaveColor("Wave Color", Color) = (0.0, 0.0, 0.0, 100.0)
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			Pass
			{
				HLSLPROGRAM
					#include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/SurfaceInput.hlsl"
					#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"

					TEXTURE2D(_CameraDepthTexture);
					SAMPLER(sampler_CameraDepthTexture);

					TEXTURE2D(_MainTex);
					SAMPLER(sampler_MainTex);

					float _WaveDistance;
					float _WaveTrail;
					float4 _WaveColor;
					float _WaveActive;


					struct Attributes
					{
						float4 positionOS       : POSITION;
						float2 uv               : TEXCOORD0;
					};

					struct Varyings
					{
						float2 uv        : TEXCOORD0;
						float4 vertex : SV_POSITION;
						UNITY_VERTEX_OUTPUT_STEREO
					};

					Varyings vert(Attributes input)
					{
						Varyings output = (Varyings)0;
						UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

						VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
						output.vertex = vertexInput.positionCS;
						output.uv = input.uv;
						return output;
					}

					half4 frag(Varyings input) : SV_Target
					{
						//UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
						float4 scaleOffset = unity_StereoScaleOffset[unity_StereoEyeIndex];

						if (scaleOffset.x > 0) {

							input.uv.x /= 2;
							input.uv.x += scaleOffset.z;

						}

						float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, input.uv);
						float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv);
						float grey = ((color.x + color.y + color.z) / 3.0f);

						if (depth >= _ProjectionParams.z)
							return color;
						if (_WaveActive == 1.0) {

							float waveFront = step(depth, _WaveDistance);

							float waveTrail = smoothstep(_WaveDistance - _WaveTrail, _WaveDistance, depth);
							float wave = waveFront * waveTrail;
							color = (grey, grey, grey, color.z);

							float4 outCOl = lerp(color, _WaveColor, wave);
							return outCOl;//col * s
						}
						else
							return color;
					}

					#pragma vertex vert
					#pragma fragment frag
				ENDHLSL
			}
		}
}
