Shader "Unlit/OutlineGlow"
{
	Properties
	{
		_Color("Glow Color", Color) = (1,1,1,1)
		_Intensity("Intensity", Range(0,2)) = 0.5
	}
	SubShader
	{
		Tags 
		{ 
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
			"XRay" = "ColoredOutline" 
		}

		ZWrite Off
		Blend One One


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
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float3 viewDir : TEXCOORD1;
			};
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				o.normal = UnityObjectToWorldNormal(v.normal);
				o.viewDir = normalize(_WorldSpaceCameraPos.xyz - mul(_Object2World, v.vertex).xyz);
				return o;
			}
			
			float4 _Color;
			float _Intensity;

			fixed4 frag (v2f i) : SV_Target
			{
				float ndotv = 1 - dot(i.normal, i.viewDir) * _Intensity;
				//Making the object a solid glow, rather than a fadeing glow
				//float ndotv = dot(i.normal, i.viewDir) * _Intensity;
				return ndotv * _Color;
			}
			ENDCG
		}
	}
}
