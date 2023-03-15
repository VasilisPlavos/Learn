using MagicVilla.VillaAPI.Models.Dto;

namespace MagicVilla.VillaAPI.Data;

public static class VillaStore
{
	public static readonly List<VillaDto> VillaList = new()
	{
		new VillaDto { Id = 1, Name = "Pool View", Owner = "Panos" },
		new VillaDto { Id = 2, Name = "Beach View", Owner = "Katy" }
	};
}