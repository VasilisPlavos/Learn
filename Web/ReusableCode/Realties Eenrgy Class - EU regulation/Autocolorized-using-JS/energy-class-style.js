function start() {
    const elements = document.querySelectorAll("#EnergyClassComponent > p");
    elements.forEach(element => {
        if (element != null) {
            const energyClassValue = element.innerText;
    
            if (energyClassValue === "A+") {
                element.innerHTML = 
                `<p class="energy-label energy-label-right energy-class-aplus"><span class="energy-class-percent">(98-100)</span>${energyClassValue}</p>`;
                return;
            }
    
            if (energyClassValue === "A") {
                element.innerHTML = 
                `<p class="energy-label energy-label-right energy-class-a"><span class="energy-class-percent">(92-98)</span>${energyClassValue}</p>`;
                return;
            }
    
            if (energyClassValue === "B+") {
                element.innerHTML = 
                `<p class="energy-label energy-label-right energy-class-bplus"><span class="energy-class-percent">(89-91)</span>${energyClassValue}</p>`;
                return;
            }
    
            if (energyClassValue === "B") {
                element.innerHTML = 
                `<p class="energy-label energy-label-right energy-class-b"><span class="energy-class-percent">(81-88)</span>${energyClassValue}</p>`;
                return;
            }
    
            if (energyClassValue === "C") {
                element.innerHTML = 
                `<p class="energy-label energy-label-right energy-class-c"><span class="energy-class-percent">(69-80)</span>${energyClassValue}</p>`;
                return;
            }
    
            if (energyClassValue === "D") {
                element.innerHTML = 
                `<p class="energy-label energy-label-right energy-class-d"><span class="energy-class-percent">(55-68)</span>${energyClassValue}</p>`;
                return;
            }
    
            if (energyClassValue === "E" || energyClassValue === "F" || energyClassValue === "G") {
                element.innerHTML = 
                `<p class="energy-label energy-label-right energy-class-e"><span class="energy-class-percent">(<55)</span>${energyClassValue}</p>`;
                return;
            }
        }        
    });
}


start();