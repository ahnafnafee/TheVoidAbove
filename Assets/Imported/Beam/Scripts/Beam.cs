//	Copyright 2013 Unluck Software	
//	www.chemicalbliss.com	 

using UnityEngine;
using System;


public class Beam:MonoBehaviour{
    float counter = 2.0f;
    Vector3 scaleBuffer;
    Transform beam;
    
    public float startDelay;
    public float pauseDelay = 2.0f;
    public float beamDuration = 3.0f; //Negative = beam always on
    public float animationSpeed = 2.0f;
    public float xMultiplier = 0.0f;
    public float yMultiplier = 1.0f;
    public ParticleSystem[] particles;	//Place particle systems to show / hide
    bool playAudio;
    
    public void Start() {
    	counter -= startDelay;
    	toggleParticles(false);
    	beam = transform.Find("Beam");
    	scaleBuffer = beam.localScale;
    }
    
    public void matOffset() {
    	var tmp_cs1 = beam.GetComponent<Renderer>().sharedMaterial.mainTextureOffset;
        tmp_cs1.x = (beam.GetComponent<Renderer>().material.mainTextureOffset.x+(animationSpeed*Time.deltaTime*xMultiplier))%1;
        beam.GetComponent<Renderer>().sharedMaterial.mainTextureOffset = tmp_cs1;//Offsets texture for simple flowing animation
    	var tmp_cs2 = beam.GetComponent<Renderer>().sharedMaterial.mainTextureOffset;
        tmp_cs2.y = (beam.GetComponent<Renderer>().material.mainTextureOffset.y+(animationSpeed*Time.deltaTime*yMultiplier))%1;
        beam.GetComponent<Renderer>().sharedMaterial.mainTextureOffset = tmp_cs2;//%1 fixates offset between 0 and 1, never above
    }
    
    public void Update() {
    	if(counter > 2)
    	matOffset();
    	if(beamDuration < 0)
    	counter = 2.35f;
    	else
    	counter += Time.deltaTime;
    	if(counter < 2 && counter > 1.9f){
    		var tmp_cs3 = beam.localScale;
            tmp_cs3.x = scaleBuffer.x*.3f;
            tmp_cs3.z = scaleBuffer.x*.3f;
            beam.localScale = tmp_cs3;
    		beam.gameObject.SetActive(true);
    		toggleParticles(true);
    		if(GetComponent<AudioSource>() != null)
    		GetComponent<AudioSource>().Play();
    		
    	}else if(counter < 2.1f){
    		beam.gameObject.SetActive(false);
    		toggleParticles(false);
    		
    	}else if(counter < 2.2f){
    		var tmp_cs4 = beam.localScale;
            tmp_cs4.x = scaleBuffer.x*.7f;
            tmp_cs4.z = scaleBuffer.x*.7f;
            beam.localScale = tmp_cs4;
    		beam.gameObject.SetActive(true);
    		toggleParticles(true);
    		
    	}else if(counter < 2.3f){
    		beam.gameObject.SetActive(false);
    		toggleParticles(false);
    	}else if(counter < 2.4f){
    		var tmp_cs5 = beam.localScale;
            tmp_cs5.x = scaleBuffer.x;
            tmp_cs5.z = scaleBuffer.x;
            beam.localScale = tmp_cs5;
    		beam.gameObject.SetActive(true);
    		toggleParticles(true);
    		
    	}else if(counter > 2.4f + beamDuration){
    		beam.gameObject.SetActive(false);
    		toggleParticles(false);
    		if(GetComponent<AudioSource>() != null)
    		GetComponent<AudioSource>().Stop();
    		counter = 0 -pauseDelay +2;
    	}
    	
    }
    
    public void toggleParticles(bool emit){
    	for(int i = 0;i<particles.Length;i++){
    		if(particles[i] != null)
    		particles[i].enableEmission = emit;
    	}
    }

}