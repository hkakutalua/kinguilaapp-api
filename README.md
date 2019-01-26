# KinguilaApp API
[![Build Status](https://travis-ci.com/Henry-Keys/kinguilaapp-api.svg?branch=master)]

An HTTP REST API that provides exchange rates for Kwanza-Euro and Kwanza-US Dollars both from the official market (Banco Nacional de Angola), as well from the informal market, also known as Kinguilas.

## Getting Started
Read carefully the following instructions to build and run this project in your local machine.

### Prerequisites
To build and run this project you'll need an installation of .NET Core SDK in your machine. You can get .NET Core SDK and instructions to install in your operating system [here](https://dotnet.microsoft.com/download).

If you prefer to use an IDE, and therefore run the project from there, you can get Visual Studio Code [here](https://code.visualstudio.com/).

### Build and Run
To build this project from the command-line, go to the root directory of the project and type the following commands:
> dotnet restore

> dotnet build

To run the project, you simply need to go to KinguilaAppApi subdirectory, where the Startup.cs file is and type:
> dotnet run

The project will run by default at 5000 and 5001 ports, for HTTP and HTTPS respectively.

## Documentation
This request below will return the exchange rate for all supported currencies in our API:
https://kinguilaapp.herokuapp.com/api/v1/exchanges/all

You can check for more endpoints [accessing the documentation](https://kinguilaapp.herokuapp.com/).

## Running the Tests
To run all the tests (both the unit and integration tests), go to the root folder of the project with the command-line and type:
> dotnet test

## Deployment
TODO: Add instructions to deploy in Heroku
TODO: Add instructions to deploy in Azure

## License
This project is licensed under the MIT license, see the LICENSE.md file for details.

## Built With
- ASP.NET Core: the web framework
- ScrapySharp: the webscrapping library

## Acknowledgements
All the exchange rate data comes from [Kinguila Hoje
](kinguilahoje.com). Thank you very much for your daily effort in providing us with the informal market exchange rate.

