// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Noise"
{
	Properties
	{
		_GradientTexture("GradientTexture", 2D) = "white" {}
		_GradientInfluence("GradientInfluence", Range( 0 , 1)) = 0.5
		_BrightColor("BrightColor", Color) = (0.9433962,0.3871485,0.7731246,0)
		_DarkColor("DarkColor", Color) = (0.3491867,0.253115,0.6792453,1)
		_Noise("Noise", Vector) = (100,100,0,0)
		_ScrollSpeed("Scroll Speed", Vector) = (250,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred  
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float2 _Noise;
		uniform float2 _ScrollSpeed;
		uniform sampler2D _GradientTexture;
		uniform float4 _GradientTexture_ST;
		uniform float _GradientInfluence;
		uniform float4 _BrightColor;
		uniform float4 _DarkColor;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 panner65 = ( _Time.y * _ScrollSpeed + i.uv_texcoord);
			float2 uv_TexCoord70 = i.uv_texcoord * _Noise + panner65;
			float simplePerlin2D69 = snoise( uv_TexCoord70 );
			simplePerlin2D69 = simplePerlin2D69*0.5 + 0.5;
			float4 temp_cast_0 = (simplePerlin2D69).xxxx;
			float2 uv_GradientTexture = i.uv_texcoord * _GradientTexture_ST.xy + _GradientTexture_ST.zw;
			float4 blendOpSrc72 = temp_cast_0;
			float4 blendOpDest72 = tex2D( _GradientTexture, uv_GradientTexture );
			float4 lerpBlendMode72 = lerp(blendOpDest72, round( 0.5 * ( blendOpSrc72 + blendOpDest72 ) ),_GradientInfluence);
			float4 blendOpSrc77 = ( saturate( lerpBlendMode72 ));
			float4 blendOpDest77 = _BrightColor;
			float4 lerpBlendMode77 = lerp(blendOpDest77,min( blendOpSrc77 , blendOpDest77 ),_GradientInfluence);
			float4 blendOpSrc73 = ( saturate( lerpBlendMode77 ));
			float4 blendOpDest73 = _DarkColor;
			o.Emission = 	max( blendOpSrc73, blendOpDest73 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18921
68;396;1476;701;1852.767;693.9608;1.673885;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;66;-1520.612,836.1434;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;67;-1508.498,966.3674;Inherit;False;Property;_ScrollSpeed;Scroll Speed;7;0;Create;True;0;0;0;False;0;False;250,0;2,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;68;-1346.475,1093.563;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;65;-1105.31,939.0168;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;71;-1227.238,589.6094;Inherit;False;Property;_Noise;Noise;6;0;Create;True;0;0;0;False;0;False;100,100;100,100;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;70;-1006.391,470.8088;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NoiseGeneratorNode;69;-766.8518,279.6759;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;79;-1172.732,-130.5182;Inherit;False;Property;_GradientInfluence;GradientInfluence;3;0;Create;True;0;0;0;False;0;False;0.5;0.992;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;53;-1280.22,59.46786;Inherit;True;Property;_GradientTexture;GradientTexture;0;0;Create;True;0;0;0;False;0;False;-1;None;547f51a8eea214bd7a5863356fd25619;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BlendOpsNode;72;-857.1724,-70.36121;Inherit;True;HardMix;True;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;78;-492.4517,-398.2798;Inherit;False;Property;_BrightColor;BrightColor;4;0;Create;True;0;0;0;False;0;False;0.9433962,0.3871485,0.7731246,0;0.9433962,0.3871485,0.7731246,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BlendOpsNode;77;-734.8518,-334.5691;Inherit;True;Darken;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;74;-245.4989,-298.3184;Inherit;False;Property;_DarkColor;DarkColor;5;0;Create;True;0;0;0;False;0;False;0.3491867,0.253115,0.6792453,1;0.3491867,0.253115,0.6792453,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;45;-1352.13,502.347;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;58;-558.0895,740.5027;Inherit;False;Constant;_Float1;Float 1;5;0;Create;True;0;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;46;-1444.66,309.8046;Inherit;False;Property;_UVScrollSpeed;UV Scroll Speed;2;0;Create;True;0;0;0;False;0;False;0,500;0,50;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.BlendOpsNode;73;-278.4989,-38.31836;Inherit;True;Lighten;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;25.1;False;1;COLOR;0
Node;AmplifyShaderEditor.PannerNode;47;-1187.13,298.347;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;17;-140.6454,726.0782;Inherit;False;Property;_NoiseGranularity;NoiseGranularity;1;0;Create;True;0;0;0;False;0;False;1000,1000;25,25;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.ColorNode;76;-554.9927,527.1724;Inherit;False;Constant;_Color1;Color 1;3;0;Create;True;0;0;0;False;0;False;0,0.539437,0.6320754,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Noise;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;ForwardOnly;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;1;;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;65;0;66;0
WireConnection;65;2;67;0
WireConnection;65;1;68;0
WireConnection;70;0;71;0
WireConnection;70;1;65;0
WireConnection;69;0;70;0
WireConnection;72;0;69;0
WireConnection;72;1;53;0
WireConnection;72;2;79;0
WireConnection;77;0;72;0
WireConnection;77;1;78;0
WireConnection;77;2;79;0
WireConnection;73;0;77;0
WireConnection;73;1;74;0
WireConnection;47;2;46;0
WireConnection;47;1;45;0
WireConnection;0;2;73;0
ASEEND*/
//CHKSM=C9969C01616768C4BAC4422F7BEF415B5B9A66D8