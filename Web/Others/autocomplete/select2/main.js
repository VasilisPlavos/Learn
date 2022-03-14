var locationsDtos, dataTest; // select2LocationDtos
InitLocationsAsync();

// Add Event Listener
$("#locations").on("select2:select", function (e) {
  console.log(e.params.data);
});

async function InitLocationsAsync() {
  document.querySelector("#locations").disabled = true;

  let locations = await GetLocationsAsync("englishDefault");
  locationsDtos = getLocationsDtos(locations);

  dataTest = {
    "results": locationsDtos,
    "pagination": {
      "more": true
    },
  }

  InitSelect2();
  document.querySelector("#locations").disabled = false;
}

function InitSelect2() {
  (function () {
    // init select 2
    $("#locations").select2({
      data: dataTest.results,
      placeholder: "Select City, Region, District",
      allowClear: true,
      multiple: true,
      maximumSelectionLength: 10,
      closeOnSelect: false
      // minimumInputLength: 3
    });
  })();
}

function matchCustom(params, data) {
  // If there are no search terms, return all of the data
  if ($.trim(params.term) === '') {
    return data;
  }

  // Do not display the item if there is no 'text' property
  if (typeof data.text === 'undefined') {
    return null;
  }

  var counter = document.querySelector("#locations").nextElementSibling.nextElementSibling.nextElementSibling.querySelectorAll("li").length;
  // if (counter > 20) return null;

  // `params.term` should be the term that is used for searching
  // `data.text` is the text that is displayed for the data object
  if (data.text.indexOf(params.term) > -1) {
    if (counter < 20) {
      var modifiedData = $.extend({}, data, true);
      modifiedData.text += ' (matched)';
  
      // You can return modified objects from here
      // This includes matching the `children` how you want in nested data sets
      return modifiedData;
    }
  }

  // Return `null` if the term should not be displayed
  return null;
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
        perioxiKeyword = `${dimosKeyword} / ${perioxi.LocationKeyword}`;
        results.push({
          id: `PerioxiId=${perioxi.PerioxiId}`,
          text: perioxiKeyword,
        });
      }
    }
  }

  return results;
}

function mockData() {
  let results = locationsDtos;
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


// $('#locations').select2('data'); to get the selected data
// $('#locations').trigger('change');
// $('#locations').val(['1'])
// $('#locations').find("option")[7]
// var option = new Option("Trikala", 1, false, false)
// $('#locations').append(option).trigger('change')
// $('#locations').select2({placeholder: "34" })
// for(var opt of document.querySelectorAll('#locations option')) { opt.remove() }
