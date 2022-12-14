# Congestion.Tax
A dotnet webapi can be used to calculate Congestion Tax in Gothenburg (can be modified to suit other cities with their city rules see section below).

## Rules Applied

- Maximum daily charge 60 sek
- During 60 mins interval, only the highest tax is charged.
- The tax is not charged on weekends (Saturdays and Sundays), public holidays, days before a public holiday and during the month of July.

### Hours and amounts for congestion tax in Gothenburg

| Time        | Amount |
| ----------- | :----: |
| 06:00–06:29 | SEK 8  |
| 06:30–06:59 | SEK 13 |
| 07:00–07:59 | SEK 18 |
| 08:00–08:29 | SEK 13 |
| 08:30–14:59 | SEK 8  |
| 15:00–15:29 | SEK 13 |
| 15:30–16:59 | SEK 18 |
| 17:00–17:59 | SEK 13 |
| 18:00–18:29 | SEK 8  |
| 18:30–05:59 | SEK 0  |

### Tax Exempt vehicles

- Emergency vehicles
- Busses
- Tractors
- Diplomat vehicles
- Motorcycles
- Military vehicles
- Foreign vehicles

### Changing cities

- Default city is "Gothenburg", but it can be changed to other cities by modify the constant varible on Line 12 of CongestionTaxCalculator.cs file. 

- Hours and amounts rule can be set on GetTollFee function according to city choices.

## How to use the the webapi

### endpoint 
 //hostAddress/CongestionTaxCalculator
 
### method
 [HttpPost]

### Request body 
{
  "vehicleType": "string",
  "dates": [
    "string"
  ]
}

e.g. 
{
  "vehicleType": "Car",
  "dates": [
    "2013-11-03 09:04:27",
    "2013-11-05 09:04:27"
  ]
}

### Expected result type 
  
int

### Swagger

The webapi can be tested on localhost with Swagger UI by calling the following url after implement it locally.
$ dotnet run
https://localhost:5001/swagger/index.html

## Note

This is a small application for testing purposes and developed in very short time.
Therefore, functions relate to validation, error handling and status code were not fully implemented. 
Due to time restrictions errors may occure.

