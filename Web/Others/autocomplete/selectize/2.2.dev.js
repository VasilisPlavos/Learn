var locationsDtos, locationsSelectize;
InitLocationsAsync();

async function InitLocationsAsync() {
  let locations = await GetLocationsAsync("englishDefault");
  locationsDtos = getLocationsDtos(locations);

  for (const l of locationsDtos) {
    var option = new Option(l.text, l.id, false, false);
    document.querySelector("#locations").append(option);
  }

  var eventHandler = function (name) {
    return function () {
      console.log(name, arguments);
      $("#log").append('<div><span class="name">' + name + "</span></div>");
    };
  };

  locationsSelectize = $("#locations").selectize({
    maxOptions: 7,
    placeholder: "Search City, Region, District...",
    create: true,
    onChange: function (value) {
      console.log(document.querySelector("#locations-selectized").value);
    },
    getItem: function (value) {
      console.log(document.querySelector("#locations-selectized").value);
    },
    onItemRemove: eventHandler("onItemRemove"),
    onOptionAdd: function (value, data) {
      console.log(document.querySelector("#locations-selectized").value);
    },
    onLoad: function (data) {
      console.log(data);
    },
    onOptionRemove: eventHandler("onOptionRemove"),
    onDropdownOpen: eventHandler("onDropdownOpen"),
    onDropdownClose: eventHandler("onDropdownClose"),
    onFocus: eventHandler("onFocus"),
    onBlur: eventHandler("onBlur"),
    onInitialize: eventHandler("onInitialize"),
  });

  document.querySelector("#locations-selectized")
.addEventListener("keyup", function (e) 
{ 
  locationInput  = document.querySelector("#locations-selectized").value; 
});

  // function contains(str1, str2) {
  //   return new RegExp(str2, "i").test(str1);
  // }

  // $("#locations").select2({
  //   data: locationsDtos,
  //   query: function (q) {
  //     var pageSize = 50,
  //       results = this.data.filter(function (e) {
  //         return contains(e.text, q.term);
  //       });

  //     // Get a page sized slice of data from the results of filtering the data set.

  //     var paged = results.slice((q.page - 1) * pageSize, q.page * pageSize);

  //     q.callback({
  //       results: paged,
  //       more: results.length >= q.page * pageSize,
  //     });
  //   },
  // });
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

// Seems fast enough to me even with OVER 9000 items.

var HOW_MANY = 9002;

var testdata = [];
for (var i = 0; i < HOW_MANY; i++) {
  testdata.push({
    id: i,
    text: "Item #" + i,
  });
}

// Important: Make sure to return true when your filter
// receives an empty string as a search term.
