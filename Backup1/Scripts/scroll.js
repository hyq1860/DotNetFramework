// JavaScript Document
function ScrollPic(scrollContId, arrLeftId, arrRightId, frameWidth,pageWidth,speed,space,autoPlay,autoPlayTime) {
	var _=this;
	_.index=0;
    _.stripDiv = document.createElement("DIV");
    _.listDiv01 = document.createElement("DIV");
    _.listDiv02 = document.createElement("DIV");
	this.initialize=function(){
		this.autoPlayTime=_.autoPlayTime*1000;
		_.pageWidthmo=_.pageWidth%_.space;
		_.pageWidth=_.pageWidth-_.pageWidthmo;
		
		//alert(_.pageWidthmo);
		_.scrollContId=document.getElementById(_.scrollContId);
		_.conInnerHtml=_.scrollContId.innerHTML;
		_.listDiv01.innerHTML=_.listDiv02.innerHTML=_.conInnerHtml;
		_.stripDiv.appendChild(_.listDiv01);
		_.stripDiv.appendChild(_.listDiv02);
		_.scrollContId.innerHTML="";
		_.scrollContId.appendChild(_.stripDiv);
		_.scrollContId.style.width=_.frameWidth+"px";
		_.stripDiv.style.width=32766+"px";
		_.scrollContId.style.overflow="hidden";
		_.listDiv01.style.cssFloat="left";
		_.listDiv02.style.cssFloat="left";
		_.listDiv01.style.styleFloat="left";
		_.listDiv02.style.styleFloat="left";
		_.btnL();_.btnR();
		if(_.autoPlay){_.auto();}
		
	},
	this.btnL=function(){
		_.L=document.getElementById(_.arrLeftId);
		_.L.onclick=function(){
		if(_.timer==null){
			if(_.scrollContId.scrollLeft < 1){_.scrollContId.scrollLeft=_.listDiv01.offsetWidth;}else{}
			_.timespeed=0;
			_.timer=setInterval(function(){if(_.timespeed<_.pageWidth){_.timespeed+=_.space;_.scrollContId.scrollLeft-=_.space;}else{_.scrollContId.scrollLeft-=_.pageWidthmo;clearInterval(_.timer);_.timer=null;}}, _.speed);
			}
		}
	},
	this.btnR=function(index){
		_.R=document.getElementById(_.arrRightId);
		_.R.onclick=function(){
			if(_.timer==null){
			if(_.scrollContId.scrollLeft>=_.listDiv01.offsetWidth){_.scrollContId.scrollLeft=0}else{}
			_.timespeed=0;
			_.timer=setInterval(function(){if(_.timespeed<_.pageWidth){_.timespeed+=_.space;_.scrollContId.scrollLeft+=_.space;}else{_.scrollContId.scrollLeft+=_.pageWidthmo;clearInterval(_.timer);_.timer=null;}}, _.speed);
			
			}
		}
	},
	this.fnMove=function(index){
		if(_.timer==null){
			if(_.scrollContId.scrollLeft>=_.listDiv01.offsetWidth){_.scrollContId.scrollLeft=0}else{}
			_.timespeed=0;
			_.timer=setInterval(function(){if(_.timespeed<_.pageWidth){_.timespeed+=_.space;_.scrollContId.scrollLeft+=_.space;}else{_.scrollContId.scrollLeft+=_.pageWidthmo;clearInterval(_.timer);_.timer=null;}}, _.speed);
		}
	},
	this.auto=function(){
		_.arrlength=_.scrollContId.offsetWidth/_.pageWidth
		_.doIndex=0;
		_.scrollContId.onmouseover=function(){clearInterval(_.oTimer);_.oTimer=null;};
		_.scrollContId.onmouseout=function(){_.oTimer=setInterval(function(){_.fnMove((_.doIndex+1)%_.arrlength,1000);},_.autoPlayTime);};
		_.oTimer=setInterval(function(){_.fnMove((_.doIndex+1)%_.arrlength,1000); },_.autoPlayTime);
	}
};