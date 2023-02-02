# Parking (English version below)
Voor een parkeerbedrijf houden we een lijst bij van parkings. Het is aan jou om de functionaliteiten te vervolledigen.

## Domein
Het domein is relatief eenvoudig, er zijn slechts een paar klassen die we bijhouden in de databank, `ParkLocation` en `ParkRecord`. Je mag tijdens het examen geen visibiliteit van de properties aanpassen. Telkens er iemand een parkeerticket neemt, houden we een historiek bij van welke nummerplaat (`LicensePlate`) op welke datum in welke `ParkLocation` parkeerde.

## Puntenverdeling
De punten staan naast de vragen, indien je solution niet compileert (0/20), code in commentaar wordt niet bekeken.

## Functionaliteiten
Er zijn twee primaire functionaliteiten: het nemen van een parkeerticket, en het aanmaken van een `ParkLocation`. Daarover straks meer.

## Packages
Alle packages zitten reeds in de projecten, je dient geen extra packages via NuGet toe te voegen, mogelijks wel te gebruiken of te implementeren.

## Vraag 1 - Domein (10)
Als er een parkeerticket wordt genomen via `ParkLocation.TakeTicket`, wordt de functie `ParkLocation.AddRecord` opgeroepen. Zorg dat de volgende unit testen slagen:
- `ParkRecord_Should.ParkLocation_has_one_record_after_creating`
- `ParkRecord_Should.Customer_cannot_park_twice_in_the_same_ParkLocation_at_the_same_time`
- `ParkRecord_Should.Customer_can_park_again_after_stopping_previous_session`
- `ParkRecord_Should.Customer_cannot_park_in_full_parking`
> Verander niets aan setters, getters of parameters, implementeer alleen de methode `ParkLocation.AddRecord` op basis van de tests.


## Vraag 2 - Unit Test (10)
Wanneer de `name` parameter van de `ParkRecord` constructor niet is ingevuld of uit spaties bestaat, zou er een `ArgumentException` moeten worden gegooid. Valideer dit door de theoretische `ParkLocation_Should.Not_be_created_with_invalid_name` unit test te implementeren **en** de test te laten slagen door een `Guard` clausule uit het Ardalis pakket te gebruiken in de setter van `ParkRecord.Name`.

Wanneer de nummerplaat niet geldig is, moet er een `ArgumentException` worden gegooid. Gebruik hiervoor deze regex: "[1-9]-[A-Z]{3}-[0-9]{3}" en gebruik hiervoor een `Guard` clausule uit het Ardalis pakket in de setter van `ParkRecord.LicensePlate`.
Valideer dit door de `ParkLocation_Should.Have_a_valid_license_plate` unit test te laten slagen.


## Vraag 3 - Configurations (10)
In de database zijn er nog enkele problemen die opgelost moeten worden door de `ParkLocationConfiguration` correct te implementeren:
- De tabel krijgt als naam `ParkLocation`
- De `Fee` van een `ParkLocation` heeft een precisie van 4,2: vier cijfers voor de komma, twee na de komma.
- De `Name` moet uniek zijn, uit maximaal 100 characters bestaan en mag niet `null` zijn.
- De `Capacity` mag niet null zijn.
- Wanneer een `ParkLocation` verwijderd wordt, moeten de gelinkte `ParkRecord`s verwijderd worden.

Maak de mapping expliciet, en steun niet op de nullable project setting en conventies.

Je kan nakijken wat er gebeurt aan de hand van volgende queries:
```sql
-- delete in ParkRecord een enkele rij
delete from ParkRecord where Id=1;
-- delete in ParkRecord alle rijen met ParkLocationId = 1
delete from ParkRecord where Id=1;
```


## Vraag 4 - Filter (20)
Momenteel worden de parkings opgehaald zonder enige filterfunctionaliteit in de Index pagina.

Implementeer de filterfunctionaliteit zodat een oproep naar de server wordt uitgevoerd en een lijst van parkings wordt teruggestuurd op basis van de zoekterm in het filter. De aanroep wordt getriggerd bij het verliezen van de focus.

Implementeer het filter in de database en niet in de back-end / front-end. Alleen de parkings waarbij `Name` de zoekterm bevat worden teruggegeven in een alfabetische volgorde op basis van de `Name`. Filter ook op basis van de `Max fee`, zodat gebruikers te dure parkings kunnen wegfilteren.

Het aantal vrije parkeerplaatsen wordt weergegeven in `Available`. Gebruik hiervoor de juiste `Eager loading` vorm om het aantal bezette parkeerplaatsen uit `Records` te halen. Doe dit ook in dezelfde query, en dus niet in back-end / front-end.


## Vraag 5 - Auth (10)
De `Create` knop bovenaan de `/parking` pagina zou alleen beschikbaar mogen zijn voor `Administrator`s. **Daarnaast** mogen enkel `Administrator`s de `/parking/create` pagina zien.
> Je hoeft je geen zorgen te maken dat de API call niet afgeschermd is voor deze vraag, omdat we gebruik maken van de `FakeAuthenticationProvider`.


## Vraag 6 - Create (15)
Het aanmaken van `ParkLocation`s is momenteel niet erg functioneel, het is enkel gestyled met behulp van BULMA, hergebruik deze elementen. Implementeer het formulier met behulp van `ParkLocationDto.Create`, `Parkings.Create.razor`, `ParkLocationController` en `ParkLocationService.CreateAsync` (back-/frontend). Gebruik een `EditForm` met `FluentValidation` om er zeker van te zijn dat er geen ongeldige parkings kunnen worden aangemaakt. Controleer de database regels en maak ze consistent (de regels in de databank gelden ook voor de validatie). De `Validator` is een geneste klasse binnen `ParkLocationDto.Create`.

Zorg ervoor dat de gebruiker per invoerveld de gepaste feedback krijgt, en maak dus geen gebruik van `ValidationSummary`.

Daarnaast dien je ook de Server / API te beschermen tegen ongeldige materialen, doe ook dit aan de hand van het `FluentValidation.AspNetCore` package en middleware.

Er zijn enkele bijkomstige validaties:
- `Capacity` moet groter zijn dan 0
- `Fee` mag niet groter zijn dan 20 en niet kleiner dan 0
- `MaxHeight` mag niet leeg zijn en moet minstens 1.7 zijn
- `LpgAllowed` en `HasToilets` mag niet leeg zijn
- `Description` mag niet leeg zijn en maximum 1000 characters zijn

> Je hoeft geen extra eigenschappen toe te voegen aan de `ParkLocationDto.Create`.


## Vraag 7 - Notificaties (15)
Wanneer een ParkLocation is aangemaakt wordt de gebruiker terug genavigeerd naar de `Parking.Index.razor` pagina, ook moet er een notificatie worden getoond aan de gebruiker. Gebruik [Blazored.Toast](https://github.com/Blazored/Toast) om een succes notifcatie te tonen "Parking was added!", gebruik de README.md van het project om de functionaliteit te implementeren.

Als er een parking wordt aangemaakt met een naam die al bestaat, moet je een foutboodschap krijgen, en op dezelfde pagina blijven. Gebruik hiervoor de `ShowError` functie van Blazored.Toast, met de tekst van de fout uit de backend.
Om dit te implementeren, gebruik je de `StatusCode` van de `response` in `ParkLocationService`.


## Vraag 8 - Theorie (10)
Beantwoord de volgende vragen in-line in deze README.md:
1. We hebben op drie niveaus validatie: de databank, het domein en in de controller. Waarom volstaat één niveau niet? Verklaar de functie van de validatie op elk niveau.
   - Antwoord:
2. Leg uit waarom we het keyword `partial` nodig hebben.
   - Antwoord:

---

# Parking lot (English Version)
For a business managing different parking lots, we keep a list of parkings. It is up to you to further complete these functionalities.

## Domain
The domain is relatively simple, there are only 2 classes that we keep in the database, namely `ParkLocation` and `ParkRecord`. You are not allowed to change the visibility of the properties during the exam. Whenever someone takes a ticket, we keep a record of which license plate parked in which `ParkLocation` and on what date.

## Points distribution
Points are next to the questions, if your solution does not compile (0/20), code in comments does not count for scoring.

## Functionalities
There are two primary functionalities, taking a parking ticket, and creating an instance of `ParkLocation`. More on that later.

## Packages
All packages are already in the projects, you don't need to add any additional packages through NuGet, possibly use or implement them.

## Question 1 - Domain (10)
If someone takes a parking ticket via `ParkLocation.TakeTicket`, the function `ParkLocation.AddRecord` is called. Make sure the following tests pass:
- `ParkRecord_Should.ParkLocation_has_one_record_after_creating`
- `ParkRecord_Should.Customer_cannot_park_twice_in_the_same_ParkLocation_at_the_same_time`
- `ParkRecord_Should.Customer_can_park_again_after_stopping_previous_session`
- `ParkRecord_Should.Customer_cannot_park_in_full_parking`
> Don't change anything about setters, getters or parameters, just implement the method `ParkLocation.AddRecord` based on the tests.

## Question 2 - Unit Test (10)
When the `name` parameter of the constructor of `ParkRecord` isn't filled in or exists of spaces, an `ArgumentException` should be thrown. Validate this by implementing the fictitious `ParkLocation_Should.Not_be_created_with_invalid_name` unit test **and** passing the test by adding a `Guard` clause from the `Ardalis` package in the setter of `ParkLocation.Name`.

When the license plate isn't valid, an `ArgumentException` should be thrown. Use this regular expression: "[1-9]-[A-Z]{3}-[0-9]{3}" and use a `Guard` clause from the Ardalis package in the setter of `ParkRecord.LicensePlate`.
Validate your implementation by making unit test `ParkRecord_Should.Have_a_valid_license_plate` pass.

## Question 3 - Configurations (10)
In the database, there are still some problems that need to be solved by implementing the `ParkLocationConfiguration` correctly:
- The table should be named `ParkLocation`
- The `Fee` of `ParkLocation` gets a precision of 4,2: four digits before the comma, two after the comma.
- The `Name` of a `ParkLocation` must be unique, have a maximum length of 100, and can't be null.
- When a `ParkLocation` is removed from the database, the linked `ParkRecords` must be automatically removed.

Make the mapping explicitly, and don't make use of the nullable project setting or conventions.

You can use these queries to verify this works as desired:
```sql
-- delete a single row from ParkRecord
delete from ParkRecord where Id=1;
-- delete all rows from ParkRecord with ParkLocationId = 1
delete from ParkRecord where Id=1;
```

## Question 4 - Filter (20)
Currently the parkings are retrieved without any filtering in the Index page.

Implement the filter functionality so that the call to the server is executed and a list of parking locations is returned based on the search term in the filter. The call is triggered by losing focus.

Implement the filter in the database, and not in the backend or frontend. Only parkings where the search term is part of `Name` should be returned in alphabetical order based on the `Name` column. Also filter based on `Max fee`, so that users can filter out expensive parking spaces.

The total number of free spaces is returned in `Available`. Use the right `Eager loading` technique to fetch the number of used spaces from the `Records` property. Do this in the same query, so not in the backend or the frontend.

## Question 5 - Auth (10)
The create button at the top of the `/parking` page should only be available to `Administrators`. **In addition** only `Administrator`s should see the `/parking/create` page.
> You don't have to worry that the API calls is not protected for this query, because we are using the `FakeAuthenticationProvider`.

## Question 6 - Create (15)
Creating `ParkLocation`s is not very functional at this time, it's only styled using BULMA, reuse these elements. Implement the form using `ParkLocationDto.Create`, `Parkings.Create.razor`, `ParkLocationController` and `ParkLocationService.CreateAsync` (back-/frontend). Use an `EditForm` with `FluentValidation` to make sure no invalid `ParkLocation` can be created. Check the database rules and make them consistent (rules in the database also apply to validation). `Validator` is a nested class within `ParkLocationDto.Create`.

Make sure that the user gets feedback for each input field, and don't use `ValidationSummary`.

In addition, you also need to protect the Server / API from invalid materials being created, do this using the `FluentValidation.AspNetCore` package and middleware.

There are some additional validations:
- `Capacity` must be greater than 0
- `Fee` can't be larger than 20 or smaller than 0
- `MaxHeight` must not be empty and must be at least 1.7
- `LpgAllowed` and `HasToilets` can't be empty
- `Description` can't be empty and cannot be longer than 1000 characters

> You do not need to add any additional properties to the `ParkLocationDto.Create`.


## Question 7 - Notifications (15)
When a `ParkLocation` is created, the user is navigated back to the `Parking.Index.razor` page. A notification should be shown to the user. Use [Blazored.Toast](https://github.com/Blazored/Toast) to display a success notification "Parking was added!", use the README.md of the project to implement the functionality.

When a parking is created with a name that already exists, you should get an error message and stay on the same page. Use the `ShowError` function of Blazored.Toast for this, with the error message from the backend.
To implement this, use the `StatusCode` of the `response` in `ParkLocationService`.

## Question 8 - Theory (10)
Answer the following questions in-line in this README.md:
1. We have validation on three different levels: the database, the domain and in the controller. Why doesn't one level suffice? Explain the function of the validation on each level.
   - Answer:
2. Explain why we need the `partial` keyword.
   - Answer: