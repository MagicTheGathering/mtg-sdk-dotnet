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

#### Find a card by multiverse id
```cs
CardService service = new CardService();
Card result = service.Find(123);
```
#### Filter Cards via query parameters
```cs
CardService service = new CardService();
Card result = service.Where(x => x.Set, "ktk")
                  .Where(x => x.SubTypes, "warrior,human")
                  .All()
```    
#### Find all cards (Will page through all data)
```cs
CardService service = new CardService();
Card result = service.All()
```      
#### Get all cards with pagination
```cs
CardService service = new CardService();
Card result = service.Where(x => x.Page, "5")
                  .Where(x => x.PageSize, "250")
                  .All()
```      
#### Find a set by code
```cs
SetService service = new SetService();
Set result = service.Find("ktk");
```    
#### Filter sets via query parameters
```cs
SetService service = new SetService();
Set result = service.Where(x => x.Name, "khans").All()
```     
#### Get all Sets
```cs
SetService service = new SetService();
Set result = service.All()
```       
#### Get all Types



#### Get all Subtypes



#### Get all Supertypes


