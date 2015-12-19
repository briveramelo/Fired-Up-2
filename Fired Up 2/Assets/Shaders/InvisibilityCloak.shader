Shader "Custom/InvisibilityCloak" {
	Properties{
		_Color ("Main Color", Color) = (1,1,1,0.5)
		_MainTex("Cloak", 3D) = "defaulttexture" {}
	}
	
	SubShader{
		// draw after all opaque objects (queue = 2001)
		Tags{ "Queue" = "Geometry+1" }

		// cloak the image behind
		Pass{	Blend SrcColor SrcAlpha
				SetTexture[_MainTex]{}
		} 
	}
}
