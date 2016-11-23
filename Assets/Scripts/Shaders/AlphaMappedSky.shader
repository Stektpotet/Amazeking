Shader "Custom/AlphaMappedSky"
{
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
		[PerRendererData] _MainTex ("Albedo (RGBA)", 2D) = "white" {}
		_BumpMap("Normalmap(RGB) + Cutout (A)", 2D) = "bump" {}
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
			"RenderType" = "TransparentCutout"
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

		// Use shader model 3.0 target, to get nicer looking lighting
#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _SkyTex;
		sampler2D _AlphaTex;
		sampler2D _BumpMap;
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

		void surf (Input IN, inout SurfaceOutput o) 
		{
			
			fixed4 sky = tex2D(_SkyTex, IN.worldPos * 16 *_SkyTex_TexelSize.xy + float2(_Time.x*0.125,0)) ;
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D(_MainTex , IN.uv_MainTex ) * _Color ;
			fixed cutout = step(1,tex2D(_MainTex, IN.uv_MainTex).r);
			o.Albedo = c.rgb * (1 - cutout) + _SkyColor * cutout * (1-sky.a*_SkyColor.a) + sky.rgb*cutout*sky.a*_SkyColor.a;
			o.Alpha = c.a;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
		}
	ENDCG
	}

	Fallback "Transparent/VertexLit"
}
