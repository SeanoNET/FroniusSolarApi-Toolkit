# FroniusSolarApi-Toolkit
Tools and libraries to help you retrieve/consume and store data from the Fronius solar API.

## Getting Started

These instructions will get your clone of FroniusSolarApi-Toolkit up and running on your local machine for development.

- Download and install [.NET Core 2.2+](https://dotnet.microsoft.com/download) 
- `cd /FroniusSolarApi-Toolkit/`
- `dotnet restore`
- `dotnet build && dotnet run --project FroniusSolarApi.Poller/FroniusSolarApi.Poller.csproj help`



## Components Overview

### FroniusSolarApi.Poller

#### Usage

`poller.exe fetchRealtime -c CumulationInverterData -s Console`

#### Configuration

The [FroniusSolarClient](https://github.com/SeanoNET/FroniusSolarClient) and [FroniusSolarApi.Repository](#FroniusSolarApi.Repository) configuration values are stored in `appsettings.json`

| | Description|
|---|---|
| SolarAPI_URL | The url of your Solar inverter |

Example:
```JSON
{
  "SolarAPI_URL": "192.168.1.102",
  "CsvConfiguration": {
    "FileLocation": "C:\\Temp",
    "FileName": "poller"
  }
}

```
Each data store repository has it's own configuration section see [FroniusSolarApi.Repository](#FroniusSolarApi.Repository) for more details

#### Options

Options are **case insensitive**

|Flag | Description| Example |
|---|---|---|
|-c, --collections | Required. Data collections to retrieve. | CumulationInverterData |
|-s, --store | Required. Where to save the data | Console |
|-p, --scope |  (Default: Device) Query specific device(s) or whole system. | Device |
|-d, --device |  (Default: 1) The device id to query. | 1 |
|-v, --verbose | Set output to verbose messages. | --verbose |
|--help | Display this help screen. | --help |
|--version |  Display version information. | --version |

You can view the poller options by running `poller.exe fetchRealtime help`

```
FroniusSolarApi.Poller 0.0.1
Copyright (C) 2019 FroniusSolarApi.Poller
USAGE:
Fetch CumulationInverterData collection and output to the console:
  poller fetchRealtime

  c, collections    Required. Data collections to retrieve. Valid values:
                    CumulationInverterData, CommonInverterData,
                    MinMaxInverterData

  s, store          Required. Where to save the data. Valid values: Console,
                    Csv, Mssql

  p, scope          (Default: Device) Query specific device(s) or whole system.
                    Valid values: Device, System

  d, device         (Default: 1) The device id to query.

  help              Display more information on a specific command.

  version           Display version information.
```
#### Scheduling

You can setup a [cron](https://en.wikipedia.org/wiki/Cron) schedule to automate the retrieval and storage of inverter data on Linux. For Microsoft Windows you can use [Task Scheduler](https://docs.microsoft.com/en-us/windows/win32/taskschd/task-scheduler-start-page)

#### Logging

By default poller logs will be saved to `logs/pollerlog_{date}.txt`

### FroniusSolarApi.Repository

Handles storing of the inverter data returned from the [FroniusSolarClient](https://github.com/SeanoNET/FroniusSolarClient) fetched using the [FroniusSolarApi.Poller](#FroniusSolarApi.Poller)

Current supported data sources are listed below.

- [Console](#console) - outputs the response to the console
- [Csv](#csv) - saves the response to a csv file
- [Mssql](#mssql) - saves the response to a Microsoft SQL database
- [Custom](#custom) - you can implement your own custom data store or create an issue and i can look at adding support for the given data store in FroniusSolarApi.Repository.

#### Console

Prints the response to the console output.
`poller.exe fetchRealtime -c CommonInverterData -s Console`
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

Saves the response to a csv file. You can configure the csv settings under the `CsvConfiguration` section in `appsettings.json`

| | Description|
|---|---|
| FileLocation | The location to save the csv file |
| FileName | This will trail the name of the csv file after the `collection` name for example `CumulationInverterData_poller.csv` |

```JSON
  "CsvConfiguration": {
    "FileLocation": "C:\\Temp",
    "FileName": "poller"
  }
```
For more information see [Configuration](#configuration)

#### Mssql

Saves the response to a csv file. You can configure the mssql settings under the `MssqlConfiguration` section in `appsettings.json`

For more information see [Configuration](#configuration)

#### Custom

### FroniusSolarClient

The [FroniusSolarClient](https://github.com/SeanoNET/FroniusSolarClient) is a .NET Client wrapper for the Fronius Solar API. Used to connect and invoke REST calls on the inverter.


## Authors

* **Sean O'Loughlin** - [SeanoNET](https://github.com/SeanoNET)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE) file for details