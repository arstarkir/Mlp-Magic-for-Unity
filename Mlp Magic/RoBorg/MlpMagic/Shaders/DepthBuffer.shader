// Render out the depth buffer
Shader "Hidden/MlpMagic/DepthBuffer"
{
	Properties
	{
	}

	SubShader
	{
		Cull Off ZWrite Off

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
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;

				return o;
			}

			sampler2D _CameraDepthTexture;

			fixed4 frag(v2f i) : SV_Target
			{
				return tex2D(_CameraDepthTexture, i.uv);
			}

			ENDCG
		}
	}
}
