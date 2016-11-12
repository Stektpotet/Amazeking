Shader "Custom/AlphaMappedSky"
{
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGBA)", 2D) = "white" {}
		_Cutout ("Cutout (R)", 2D) = "black" {}
		_SkyTex ("Sky Texture (RGBA)", 2D) = "white" {}
		_SkyColor ("Sky Color", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}
		LOD 500
		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha


		CGPROGRAM
#pragma surface surf Lambert vertex:vert nofog alpha:fade
#pragma multi_compile _ PIXELSNAP_ON
#pragma multi_compile _ ETC1_EXTERNAL_ALPHA


		// Use shader model 3.0 target, to get nicer looking lighting
#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Cutout;
		sampler2D _SkyTex;
		sampler2D _AlphaTex;
		fixed4 _Color;
		fixed4 _SkyColor;
		float4 _SkyTex_TexelSize;

		struct Input
		{
			float2 uv_MainTex;
			float2 worldPos;
			fixed4 color;
		};

	
		void vert(inout appdata_full v, out Input o)
		{
#if defined(PIXELSNAP_ON)
			v.vertex = UnityPixelSnap(v.vertex);
#endif
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.color = v.color * _Color;
			o.worldPos = v.vertex.xy;
		}

		fixed4 SampleSpriteTexture(float2 uv)
		{
			fixed4 color = tex2D(_MainTex, uv);

#if ETC1_EXTERNAL_ALPHA
			color.a = tex2D(_AlphaTex, uv).r;
#endif //ETC1_EXTERNAL_ALPHA

			return color;
		}


		void surf (Input IN, inout SurfaceOutput o) 
		{
			
			fixed4 sky = tex2D(_SkyTex, IN.worldPos * 16 *_SkyTex_TexelSize.xy + float2(_Time.x*0.125,0)) ;
			// Albedo comes from a texture tinted by color
			fixed4 c = SampleSpriteTexture( IN.uv_MainTex ) * _Color ;
			fixed4 cutout = tex2D(_Cutout, IN.uv_MainTex);
			o.Albedo = c.rgb * (1 - cutout.r) + cutout.r* _SkyColor * (1 - sky.a) + sky.rgb*sky.a * cutout.r;
			o.Alpha = c.a;
		}
	ENDCG
	}

	Fallback "Transparent/VertexLit"
}
