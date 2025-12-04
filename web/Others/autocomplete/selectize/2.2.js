var locationsDtos, locationsSelectize;
InitLocationsAsync();

async function InitLocationsAsync() {
  let locations = await GetLocationsAsync("englishDefault");
  locationsDtos = getLocationsDtos(locations);

  for (const l of locationsDtos) {
    var option = new Option(l.text, l.id, false, false);
    document.querySelector("#locations").append(option);
  }

  locationsSelectize = $("#locations").selectize({ maxOptions: 7, placeholder: "Search City, Region, District..." });
}

function getLocationsDtos(locations) {
  var results = [];

  var subRegions = locations.filter((x) => x.SubregionId != null);
  for (const x of subRegions) {
    results.push({
      id: `SubregionId=${x.SubregionId}`,
      text: x.LocationKeyword,
    });
  }

  var nomoi = locations.filter((x) => x.NomosId != null && x.DimosId == null);
  for (const nomos of nomoi) {
    results.push({
      id: `NomosId=${nomos.NomosId}`,
      text: nomos.LocationKeyword,
    });

    var dimoiNomou = locations.filter(
      (x) =>
        x.NomosId == nomos.NomosId && x.DimosId != null && x.PerioxiId == null
    );
    for (const dimos of dimoiNomou) {
      var dimosKeyword = `${nomos.LocationKeyword} / ${dimos.LocationKeyword}`;
      results.push({ id: `DimosId=${dimos.DimosId}`, text: dimosKeyword });

      var perioxesDimou = locations.filter(
        (x) => x.PerioxiId != null && x.DimosId == dimos.DimosId
      );
      for (const perioxi of perioxesDimou) {
        var perioxiKeyword = `${dimosKeyword} / ${perioxi.LocationKeyword}`;
        results.push({
          id: `PerioxiId=${perioxi.PerioxiId}`,
          text: perioxiKeyword,
        });
      }
    }
  }

  return results;
}

async function GetLocationsAsync(lang) {
  const reqUrl = `https://api.grekodom.com/api/Locations?lang=${lang}`;
  const response = await fetch(reqUrl, {
    method: "GET",
    headers: { "Content-Type": "application/json" },
  })
    .then((response) => {
      return response.json();
    })
    .catch((error) => {
      console.error("Error:", error);
    });

  return response.value;
}