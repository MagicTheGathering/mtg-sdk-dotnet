# Magic: The Gathering SDK

[![Build status](https://ci.appveyor.com/api/projects/status/pb5y2hxl53yo9lwu?svg=true)](https://ci.appveyor.com/project/adback03/mtg-sdk-dotnet)
[![codecov](https://codecov.io/gh/MagicTheGathering/mtg-sdk-dotnet/branch/master/graph/badge.svg)](https://codecov.io/gh/MagicTheGathering/mtg-sdk-dotnet)
[![NuGet](https://img.shields.io/badge/nuget-v1.0.0.2-blue.svg)](https://www.nuget.org/packages/MtgApiManager.Lib/)

This is the Magic: The Gathering SDK C# .NET implementation. It is a wrapper around the MTG API of [magicthegathering.io](http://magicthegathering.io/).

## Installation

MtgApiManager is available on the **[NuGet Gallery]**

Use the following command in the **Package Manager Console**.
```
PM> Install-Package MtgApiManager.Lib
```
## Rate Limit
The MTG API has a [rate limit](https://docs.magicthegathering.io/#rate-limits) in order to maintain a reponsive experience for the user. This SDK has a rate limit manager which manages the rates for you. Based on the total requests per hour the rate limit manager attempts to spead out the calls over the hour in 10 second chuncks. For example if the number of requests is limited to 5000 per hour, the number of requests per 10 seconds is 13 (5000 / (3600 / 10)) after rounding down, therefore if you make 13 requests out to the web service in 8 seconds when you attempt to make the 14th request the SDK will wait 2 seconds before trying to call the API.
## Usage
The result of all service calls resturns a generic **Exception Monad** containing the results of the call.
```cs
CardService service = new CardService();
Exceptional<List<Card>> result = service.All();
if (result.IsSuccess)
{
  var value = result.Value;
}
else
{
  var exception = result.Exception;
}
```
Each service call also has an equivalent asynchronous call.
#### Find a card by id
```cs
CardService service = new CardService();
var result = service.Find("f2eb06047a3a8e515bff62b55f29468fcde6332a");
var asyncResult = await service.FindAsync("f2eb06047a3a8e515bff62b55f29468fcde6332a");
```
#### Find a card by multiverse id
```cs
CardService service = new CardService();
var result = service.Find(123);
var asyncResult = await service.FindAsync(123);
```
#### Filter Cards via query parameters
```cs
CardService service = new CardService();
var result = service.Where(x => x.Set, "ktk")
                  .Where(x => x.SubTypes, "warrior,human")
                  .All()
var asyncResult = await service.Where(x => x.Set, "ktk")
                            .Where(x => x.SubTypes, "warrior,human")
                            .AllAsync()                  
```    
#### Find all cards (limited by default page size)
```cs
CardService service = new CardService();
var result = service.All()
var asyncResult = await service.AllAsync()
```      
#### Get all cards with pagination
```cs
CardService service = new CardService();
var result = service.Where(x => x.Page, 5)
                  .Where(x => x.PageSize, 250)
                  .All()
var asyncResult = await service.Where(x => x.Page, 5)
                            .Where(x => x.PageSize, 250)
                            .AllAsync()
```
#### Get all card types
```cs
CardService service = new CardService();
var result = service.GetCardTypes();
var asyncResult = await service.GetCardTypesAsync();
```
#### Get all card supertypes
```cs
CardService service = new CardService();
var result = service.GetCardSuperTypes();
var asyncResult = await service.GetCardSuperTypesAsync();
```
#### Get all card subtypes
```cs
CardService service = new CardService();
var result = service.GetCardSubTypes();
var asyncResult = await service.GetCardSubTypesAsync();
```
#### Find a set by code
```cs
SetService service = new SetService();
var result = service.Find("ktk");
var asyncResult = await service.FindAsync("ktk");
```    
#### Filter sets via query parameters
```cs
SetService service = new SetService();
var result = service.Where(x => x.Name, "khans").All()
var asyncResult = await service.Where(x => x.Name, "khans").AllAsync()
```     
#### Get all Sets
```cs
SetService service = new SetService();
var result = service.All()
var asyncResult = await service.AllAsync()
```
#### Generate booster
```cs
SetService service = new SetService();
var result = service.GenerateBooster("ktk")
var asyncResult = await service.GenerateBoosterAsync("ktk")
```
