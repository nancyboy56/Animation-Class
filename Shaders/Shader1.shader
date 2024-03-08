Shader "Unlit/Shader1"
{

    // input data
    //exepect for mesh and lightinng and the stuff unity automaticaly supplies
    Properties 
    {
        _MainTex ("Texture", 2D) = "white" {}

        // _ValueTemp is the internal variable, Value Temp is what you see in the editor, type, default value
        _ValueTemp("Value Temp", Float) =1.0
    }

    SubShader
    {
        //Tags rendertype, queue (before or after another shader)
        // all that is set in subshader
        //more render pipline related 
        Tags { "RenderType"="Opaque" }


        //you can set a LOD level of an object and it will pick different subshaders
        //depending on what you set LOD too
        //Fredya doesnt uses it, she just deletes it
        LOD 100

        //pass has the rendering stuff
        //blending mode, stencil properties
        //graphics related
        Pass
        {

            // anything inside of the CGPROGRAM and ENDCG is shader code
            // unity is practially HLSL but they have there own version called CG
            CGPROGRAM

            //way of telling the complier what function the vertex shader is in
            //and what function the fargement shader is in
            #pragma vertex vert
            #pragma fragment frag
            
            //if you dont need foog you can delete it
            // make fog work
            #pragma multi_compile_fog

            //takes a file and pastes code right there
            //contains a lot of unity specific things
            //bulit in functions, very useful
            //normally always have it there
            //can also have your own things included eg maths library
            #include "UnityCG.cginc"

            //define variables
            //sometimes called uniforms
            //if you have a propoerty you need a variable to go along with it
            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            //need a value always to go along with the property
            //automaticaly gets the values from the property 
            float _Value;


            //normally called appdata but it isnt a very good name for the struct
            //renamed to meshdata
            //per vertex mesh data
            struct MeshData
            {
                //even though the float4 all isnt used in the position,
                //most data from the mersh comes in as a float4, all in clusters of 4
                //sometimes uv have float4 data when your not using actually uv in the uv channels

                //vertex position
                //vertex and uv are varible names, so you can all them whatever you want!
                //the colon is called a semantic, tells the complier we want position data
                float4 vertex : POSITION;

                //usually have normals there
                float3 normals: NORMAL;

                //could not be COLOR but you can get the colour of the vertex
                float4 colour: COLOR;

                //tangents have be float4s!
                float4 tangent: TANGENT;

                //uv coordinates
                //very general, you can use them for almost anything
                //often they are used for mapping textures to objects
                //TEXCOORD0 refers to UV channel 0
                float2 uv0 : TEXCOORD0;

                //uvs can be whatver you define them to be
                //for example uv0 diffuse/normal map textures and uv1 lightmap corrdinates 
                //you can keep going like this and get the other uv corrdinate
                //you can make them float4s
                float4 uv1 : TEXCOORD1;
            };


            // v2f is the default name for the data that gets passed from
            //the vertex shader to the fragment shader
            // fredya likes naming it something other than vf2
            //like FragInput, Interpolators

            //renamed Interpolators
            struct Interpolators
            {

                // colon is sematics
                //clip space postion of this vertex, between -1,1 for this particular position
                float4 vertex : SV_POSITION;

                //this can be any date you want it to be
                // in this case TEXCOORD0 does not refer to uv channels!

                float2 uv : TEXCOORD0;

                // you can write a bunch of textcoords in here 
                //what they mean is whatever you want them to have
                //as long as they data types you have
                //max is float4
                float4 uv1 : TEXCOORD1;
                float4 uv2 : TEXCOORD2;

                //ignoring fog
                UNITY_FOG_COORDS(1)
                
            };

            
            
            Interpolators vert (MeshData v)
            {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);

                // all it returns is the interpolators
                return o;
            }

            fixed4 frag (Interpolators i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
