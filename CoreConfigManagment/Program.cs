// See https://aka.ms/new-console-template for more information
// Create a builder object

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

var inMemory = new Dictionary<string, string>
{
    { "level", "10" }
};

//Create Builder Object
var builder = new ConfigurationBuilder();

// setting base path, so it knows where to find files
builder.SetBasePath(Directory.GetCurrentDirectory());

//Add multiple configuration sources
//The ones added late in this, take precedence
builder.AddJsonFile("mySettings.json", true,true)
    .AddJsonFile("prod.json",true,true)
    .AddXmlFile("XMLSettings.config",true,true)
    .AddInMemoryCollection(initialData: inMemory); 

//IConfigurationRoot
var configBuild = builder.Build(); // this build metod read all the keys and values flatens with colon and load the values in IConfigurationRoot object.

//Print all first level child
foreach (var c in configBuild.GetChildren())
{
    Console.WriteLine($"Key:{c.Key}, Value:{c.Value}, Path:{c.Path}");
}

Console.WriteLine("---------Children of database section");
var sectionChild = configBuild.GetSection("Database");

foreach (var c in sectionChild.GetChildren())
    Console.WriteLine($"Key:{c.Key}, Value:{c.Value}, Path:{c.Path}");
Console.WriteLine("--------------------------");

//You can access an element by using flattend key
Console.WriteLine($"Key : {configBuild["Database:password"]}");
