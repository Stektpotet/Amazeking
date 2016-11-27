Shader "Sprites/SpriteBumped"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		[PerRendererData] _Color("Tint", Color) = (1,1,1,1)
		_BumpMap("Normalmap", 2D) = "bump" {}
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
	}

		SubShader
	{
		Tags
	{
		"Queue" = "Transparent"
		"RenderType" = "Transparent"
		"PreviewType" = "Plane"
	}
			LOD 500
			Cull Off

		CGPROGRAM
#pragma surface surf Lambert vertex:vert nofog keepalpha
#pragma multi_compile _ PIXELSNAP_ON
#pragma multi_compile _ ETC1_EXTERNAL_ALPHA

#pragma target 3.0 
	sampler2D _MainTex;
	sampler2D _BumpMap;
	fixed4	  _Color;
	sampler2D _AlphaTex;

	struct Input
	{
		float2 uv_MainTex;
		fixed4 color;
	};

	void vert(inout appdata_full v, out Input o)
	{
#if defined(PIXELSNAP_ON)
		v.vertex = UnityPixelSnap(v.vertex);
#endif
		UNITY_INITIALIZE_OUTPUT(Input, o);
		o.color = v.color * _Color;
		o.uv_MainTex = v.texcoord;
	}

	void surf(Input IN, inout SurfaceOutput o)
	{
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
		o.Albedo = IN.color * c.rgb * c.a;
		o.Alpha = c.a;
		o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
	}
	ENDCG
	}

		Fallback "Transparent/VertexLit"
}