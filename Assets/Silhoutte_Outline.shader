//Used to render an outline of an object's silhouette
//Credit to user AnomalousUnderdog of Unity3D's wikipedia (http://wiki.unity3d.com/index.php/Silhouette-Outlined_Diffuse)
//All comments made for my own educational purpose

Shader "Unlit/Silhoutte_Outline"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Main Color", Color) = (1,1,1,1)
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_OutlineWidth("width", Range(0.0, 0.3)) = .005

	}
		CGINCLUDE
#		include "UnityCG.cginc"
		struct appdata {
			float4 vertex : POSITION;
			float3 normal : normal;
		};
		struct v2f {
			float4 pos : POSITION;
			float4 color : COLOR;
		};
		uniform float _Outline;
		uniform float4 _OutlineColor;

		v2f vert(appdata v) {
			//making a copy of the vertex data of the base mesh, but scaled up
			v2f o;
			o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
			float3 normal = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
			float2 offset = TransformViewToProjection(normal.xy);

			o.pos.xy += offset * o.pos.z * _Outline;
			o.color = _OutlineColor;
			return o;
		}
		ENDCG
			SubShader{
				Tags{
					"Queue" = "Transparent"
				}
				Pass{
					Name "OUTLINE"
					Tags{
						"LightMode" = "Always"
					}
					Cull Off
					ZWrite Off	//no ZTest becasue we don't need x-ray
					ColorMask RGB //alpha unused
					Blend SrcAlpha OneMinusSrcAlpha
					CGPROGRAM
					#pragma vertex vert
					#pragma fragment frag

					half4 frag(v2f i) :COLOR{
						return i.color;
					}
					ENDCG
				}
				Pass{
					Name "BASE"
					ZWrite On
					ZTest LEqual
					Blend SrcAlpha OneMinusSrcAlpha
					Material{
						Diffuse[_Color]
						Ambient[_Color]
					}
					Lighting On
					SetTexture[_MainTex]{
						ConstantColor[_Color]
						Combine texture * constant
					}
					SetTexture[_MainTex]{
						Combine previous * primary DOUBLE
					}
				}
		}
			SubShader{
				Tags{"Queue" = "Transparent"}
				Pass {
					Name "OUTLINE"
					Tags{"LightMode" = "Always"}
					Cull Front
					ZWrite Off
					ColorMask RGB
					Blend SrcAlpha OneMinusSrcAlpha
					CGPROGRAM
					#pragma vertex vert
					#pragma fragment frag
					ENDCG
					SetTexture[_MainTex]{combine primary}
					}
						Pass{
							Name "BASE"
							ZWrite On
							ZTest LEqual
							Blend SrcAlpha OneMinusSrcAlpha
							Material{
								Diffuse[_Color]
								Ambient[_Color]
							}
							Lighting On
							SetTexture[_MainTex]{
								ConstantColor[_Color]
								Combine texture * constant
							}
							SetTexture[_MainTex]{
								Combine previous * primary DOUBLE
							}
					}
			}
						Fallback "Diffuse"
}