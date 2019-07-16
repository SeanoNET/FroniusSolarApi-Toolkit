# FroniusSolarApi-Toolkit
Tools and libraries to help you retrieve/consume and store data from the Fronius solar API.

## Getting Started

These instructions will get your clone of FroniusSolarApi-Toolkit up and running on your local machine for development.

## Components Overview

### FroniusSolarApi.Poller

#### Usage

`poller.exe fetch -c CumulationInverterData -s Console`

#### Options

|Flag | Description| Example |
|---|---|---|
|-c, --collections | Required. Data collections to retrieve. | CumulationInverterData |
|-s, --store | Required. Where to save the data | Console |
|-v, --verbose | Set output to verbose messages. | --verbose |
|--help | Display this help screen. | --help |
|--version |  Display version information. | --version |

`poller.exe fetch help`

```
FroniusSolarApi.Poller 1.0.0
Copyright (C) 2019 FroniusSolarApi.Poller

USAGE:
Fetch CumulationInverterData collection and output to the console:
  poller fetch

  -c, --collections    Required. Data collections to retrieve.

  -s, --store          Required. Where to save the data

  -v, --verbose        Set output to verbose messages.

  --help               Display this help screen.

  --version            Display version information.
```

### FroniusSolarApi.Repository

Handles storing of the inverter data returned from the [FroniusSolarClient](https://github.com/SeanoNET/FroniusSolarClient) fetched using the [FroniusSolarApi.Poller](#FroniusSolarApi.Poller)

Current supported data sources are;

- [Console](#console) - outputs the response to the console
- [Csv](#csv) - saves the response to a csv file
- [Mssql](#mssql) - saves the response to a MSSQL database
- [Custom](#custom) - you can implement your own custom data store or create an issue and i can look at adding support for the given data store in FroniusSolarApi.Repository.

#### Console

Prints the response to the console output.
`poller.exe fetch -c commoninverterdata -s console`
```
{
  "DAY_ENERGY": {
    "Unit": "Wh",
    "Value": 10349.0
  },
  "YEAR_ENERGY": {
    "Unit": "Wh",
    "Value": 195414.41
  },
  "TOTAL_ENERGY": {
    "Unit": "Wh",
    "Value": 195414.01999999999
  },
  "DeviceStatus": {
    "StatusCode": 2,
    "MgmtTimerRemainingTime": -1,
    "ErrorCode": 522,
    "LEDColor": 3,
    "LEDState": 0,
    "StateToReset": true
  }
}
```

#### Csv

Saves the response to a csv file. You can configure the `CsvConfiguration` in `appsettings.json`

#### Mssql

Saves the response to a csv file. You can configure the `MssqlConfiguration` in `appsettings.json`

#### Custom

### FroniusSolarClient

The [FroniusSolarClient](https://github.com/SeanoNET/FroniusSolarClient) is a .NET Client wrapper for the Fronius Solar API.


## Authors

* **Sean O'Loughlin** - [SeanoNET](https://github.com/SeanoNET)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE) file for details