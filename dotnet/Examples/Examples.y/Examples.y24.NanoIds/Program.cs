// See https://aka.ms/new-console-template for more information

using Examples.y24.NanoIds.Helpers;
using NanoidDotNet;

// Custom
var convertedyoutubeIdLong = YoutubeConverter.Decode("-NIT9LYnylw");
var youtubeId = YoutubeConverter.GenerateYoutubeIdFromUnixTimeMilliseconds();
var youtubeId2 = YoutubeConverter.GenerateYoutubeIdFromRandomLong();
var youtubeId3 = YoutubeConverter.GenerateYoutubeId3();

// Nanoid nuget - url friendly ids

// Safe: It uses cryptographically strong random generator.

// Compact: It uses more symbols than UUID (A-Za-z0-9_-)
//          and has the same number of unique options in just 22 symbols instead of 36.

// Fast: Nanoid is as fast as UUID but can be used in URLs.

var randomId = Nanoid.Generate();
randomId = Nanoid.Generate(size: 10);
randomId = Nanoid.Generate("1234567890abcdef", 10);

return ;