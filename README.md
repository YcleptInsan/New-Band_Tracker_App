# World Tour Band Tracker

#### An Epicodus project in C# using MSSQL, 06.16.17

#### **By Nick Wise***

## Description

This web application will allows a user to track bands and the venues where they've played concerts, the user can edit and delete a Venue. The user can create bands and link a venue to that band. When a user is viewing a single concert venue, the program will list out all of the bands that have played at that venue so far and allow the user to add a band to that venue. 

| World Tour Band Tracker behavior | input  | output  |
|---|---|---|
| Program will allow User to see list of venues | World Venue, Wood Stock | World Venue, Wood Stock| - Need a page that displays all venues.
| Users can look under a specific venue to see the bands that have played at it |Venue Name: WoodStock | Band Name: "Jimi Hendrix" | - on click route to id of selected Venue
| User can Add a new Venue| "LittleShop" | "LittleShop" | - form that gets the id and name of the new venue and routing within our save and find methods so they can be stored in database.
| User can add Bands to specific Venues| add band: "Bobs Marlies" to Venue: "LittleShop" |Band: "Bobs Marlies", Venue: "LittleShop"| 
| Venues can be updated to change the name| update Venue: "LittleShop"| Update "Little Shop"| - Update and Patch methods allow us to update user information.
| Venues can be deleted by user| delete Venue: "Little Shop, Wood Stock" | Venues : "Wood Stock"| 

## Gh-pages

## Setup/Installation Requirements

https://github.com/YcleptInsan/New-Band_Tracker_App
Click the "download or clone" button and copy the link.
In your computers terminal type "git clone" & paste the copied link.
Once downloaded you can open the index.html file in the browser of your choice.
You can view the code using the text editor of your choice as well.

Next, SQLCMD: > CREATE DATABASE band_tracker; > GO > USE band_tracker; > GO > CREATE TABLE band (id INT IDENTITY(1,1), name VARCHAR(255)); > CREATE TABLE venue (id INT IDENTITY(1,1), name VARCHAR(255)); > GO ; CREATE TABLE bands_venues (id INT IDENITY(1,1), band_id INT, venue_id INT); > GO ;

## Known Bugs

* No known bugs


## Support and contact details

If you have any issues or have questions, ideas, concerns, or contributions please contact any of the contributors through Github.

## Technologies Used

* HTML
* JSON
* C#
* Nancy
* Razor
* xUnit
* SQL LocalDb
* SQL Server management 2016
* ADO.NET

### License
This software is licensed under the MIT license.

Copyright (c) 2017 **Nick Wise**
