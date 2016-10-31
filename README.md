# Magic: The Gathering SDK

[![Build status](https://ci.appveyor.com/api/projects/status/94qmxtk914w36xxr?svg=true)](https://ci.appveyor.com/project/jregnier/mtgapimanager)
[![codecov](https://codecov.io/gh/jregnier/MtgApiManager/branch/master/graph/badge.svg)](https://codecov.io/gh/jregnier/MtgApiManager)
[![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg)](https://www.nuget.org/packages/MtgApiManager.Lib/)

This is the Magic: The Gathering SDK C# .NET implementation. It is a wrapper around the MTG API of [magicthegathering.io](http://magicthegathering.io/).

(Still in Development)

## Installation

MtgApiManager is available on the **[NuGet Gallery]**

Use the following command in the **Package Manager Console**.
```
PM> Install-Package MtgApiManager.Lib
```
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
