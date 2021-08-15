const bulb = document.querySelector('#myimage');

bulb.addEventListener('click', switchlight);

function switchlight() {
	var fullpath = this.src;
	fullpath = fullpath.split('/');
	var filename = fullpath[fullpath.length-1];
	if (filename === 'bulboff.gif'){
		this.src = 'bulbon.gif';
	} else {
		this.src = 'bulboff.gif'
	}	
}