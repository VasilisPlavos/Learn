function start() {
  const elements = document.querySelectorAll("#EnergyClassComponent > p");
  elements.forEach((element) => {
    if (element != null) {
      const energyClassValue = element.innerText;
      element.innerHTML = GetEnergyClassHTML(energyClassValue);
    }
  });
}

function GetEnergyClassHTML(selectedEnergyClass) {
    if (selectedEnergyClass === "A+") return `<p class="energy-label energy-label-right energy-class-aplus"><span class="energy-class-percent">(98-100)</span>${selectedEnergyClass}</p>`;
    if (selectedEnergyClass === "A")  return `<p class="energy-label energy-label-right energy-class-a"><span class="energy-class-percent">(92-98)</span>${selectedEnergyClass}</p>`;
    if (selectedEnergyClass === "B+")  return `<p class="energy-label energy-label-right energy-class-bplus"><span class="energy-class-percent">(89-91)</span>${selectedEnergyClass}</p>`;
    if (selectedEnergyClass === "B")  return `<p class="energy-label energy-label-right energy-class-b"><span class="energy-class-percent">(81-88)</span>${selectedEnergyClass}</p>`;
    if (selectedEnergyClass === "C")  return `<p class="energy-label energy-label-right energy-class-c"><span class="energy-class-percent">(69-80)</span>${selectedEnergyClass}</p>`;
    if (selectedEnergyClass === "D")  return `<p class="energy-label energy-label-right energy-class-d"><span class="energy-class-percent">(55-68)</span>${selectedEnergyClass}</p>`;
    if (selectedEnergyClass === "E" || selectedEnergyClass === "F" || selectedEnergyClass === "G")  return `<p class="energy-label energy-label-right energy-class-e"><span class="energy-class-percent">(<55)</span>${selectedEnergyClass}</p>`;
}

start();