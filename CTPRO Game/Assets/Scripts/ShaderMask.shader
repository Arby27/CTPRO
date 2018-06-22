Shader "Custom/ShaderMask" {
	Properties {
		//creates a mask that when applied to a material allows for transparency through the alpha channel of texture, 
		//with white being completly visible and black being invisible
		//used to give the buildings a more destroyed look
		_MainTex ("MainTexture", 2D) = "white" {}
		_Mask("MaskTexture",2D) = "white" {}
		_SecondTex("SecondTexture",2D) = "white" {}

		
		
	}
	SubShader{

		Tags {"Queue" = "Transparent"}
	ZWrite Off
	Lighting On

	Blend SrcAlpha OneMinusSrcAlpha

			Pass{

					SetTexture[_MainTex]
					{
						Combine texture
					}

					SetTexture[_SecondTex]
					{
						Combine previous,texture
					}

					SetTexture[_Mask]
					{
						Combine previous,texture
					}
	
	

				}

	

			}
		
	
}
