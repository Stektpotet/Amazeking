Shader "Sprites/WorldTiled"
{
	Properties
	{
		[Header(Main Textures)]
	[PerRendererData] _Color("Tint", Color) = (1,1,1,1)
		_SkyTex("Sky Texture (RGBA)", 2D) = "white" {}
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
			Lighting On
			ZWrite Off
			Blend One OneMinusSrcAlpha

			CGPROGRAM
	#pragma surface surf Lambert vertex:vert nofog keepalpha
	#pragma multi_compile _ PIXELSNAP_ON
	#pragma target 3.0

	sampler2D _SkyTex;
	fixed4 _Color;
	sampler2D _AlphaTex;
	float4 _SkyTex_TexelSize;

	struct Input
	{
		float2 worldPos;
		float2 uv_MainTex;
		fixed4 color;
	};

	void vert(inout appdata_full v, out Input o)
	{
#if defined(PIXELSNAP_ON)
		v.vertex = UnityPixelSnap(v.vertex);
#endif
		UNITY_INITIALIZE_OUTPUT(Input, o);
		o.color = v.color;
		o.worldPos = v.vertex.xy;
		o.uv_MainTex = v.texcoord.xy;
	}

	void surf(Input IN, inout SurfaceOutput o)
	{
		float2 uv = IN.worldPos * 16 * _SkyTex_TexelSize.xy + float2(_Time.x*0.125, 0);
		fixed4 c = tex2D(_SkyTex, uv) * IN.color;
		c *= _Color;
		o.Albedo = c.rgb * c.a;
		o.Alpha = c.a;
	}
	ENDCG
	}
		Fallback "Transparent/VertexLit"
}