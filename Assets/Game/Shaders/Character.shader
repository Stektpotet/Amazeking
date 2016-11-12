Shader "Sprites/CharacterBumped"
{
	Properties
	{
		_MainTex("Sprite Texture", 2D) = "white" {}
	_Color("Tint", Color) = (1,1,1,1)
		_ColorEyes("Eye Color", Color) = (1,1,1,1)
		_ColorCloak("Cloak Color", Color) = (1,1,1,1)
		_BumpMap("Normalmap", 2D) = "bump" {}
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
#pragma multi_compile _ ETC1_EXTERNAL_ALPHA

#pragma target 3.0
		sampler2D _MainTex;
	sampler2D _AlphaTex;
	sampler2D _BumpMap;
	fixed4 _Color;
	fixed4 _ColorEyes;
	fixed4 _ColorCloak;

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
		o.Albedo = IN.color * (c.rgb * (1 - step(1, c.g)) * (1 - step(1, c.b)) + step(1, c.g) *_ColorEyes + step(1,c.b) * _ColorCloak) * c.a;
		o.Alpha = c.a;
		o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
	}
	ENDCG
	}

		Fallback "Transparent/VertexLit"
}