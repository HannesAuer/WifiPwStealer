# <img src="https://user-images.githubusercontent.com/62036141/227040593-70997ac6-f6a8-4d9f-97dc-1a24e9c6c28a.png" width="25" height="25"/> WifiPwStealer
C# class for getting all saved wifi names and passwords

## Description
This is a C# class that provides methods for getting a list of all saved **wifi profiles** and all saved **wifi passwords**.

## Getting Started
### Prerequisites
- A C# project to which you want to add this class.

### Installation
- Download WifiPwStealer.cs to your project folder.
- Or copy all code from WifiPwStealer.cs to your project.

Now you can call all methods with **WifiPwStealer.\[METHOD\](\[PARAMS\]\)**.

## How to use WifiPwStealer.cs?
_Call the methods in your project with **WifiPwStealer.\[METHOD\]\(\[PARAMS\]\)**_

### Following Methods are available:
- ListAllSavedWifi()
- GetAllWifiPw()
- GetSingleWifiPw(string profileName)

### Methods example:
**ListAllSavedWifi:**
```
string[] output = WifiPwStealer.ListAllSavedWifi();
Array.ForEach(output, Console.WriteLine);
```
Output:
> Wifi_1<br>
> Wifi_2<br>
> Wifi_3

**GetAllWifiPw:**
```
string output = WifiPwStealer.GetAllWifiPw();
Console.WriteLine(output);
```
Output:
> Network Name: Wifi_1<br>
> Password: Wifi_Pw_1
>
> Network Name: Wifi_2 <br>
> Password: Wifi_Pw_2
>
> Network Name: Wifi_3<br>
> Password: Wifi_Pw_3

**GetSingleWifiPw:**
```
string output = WifiPwStealer.GetSingleWifiPw("Wifi_1"); // Change with your wifi profile name
Console.WriteLine(output);
```
Output:
> Network Name: Wifi_1<br>
> Password: Wifi_Pw_1

## Licence
This project is licensed under the MIT License - see the [LICENSE.md](LICENSE) file for details.
