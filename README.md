# Recruitment-Task
Streszczenie:
Aplikacja zakłada używanie bazy MySQL w celu przechowywania informacji na temat poszczególnych kontaktów oraz MSSQL dla danych logowania użytkowników. 
W celu włączenia funkcjonalności logowania należy przeprowadzić migrację w konsoli menedżera pakietów: PM> update-database

Klasy:
Person - Klasa bazowa, której pola pokrywają się z kolumnami klientów z bazy danych. Klasa ta została użyta w celu tworzenia obiektów kontaków, których dane pochodzą z serwera. 

Metody:

public IActionResult OnPost(): Metoda, która wywoływana jest za każdym razem gdy przycisk "submit" zostaje wciśnięty a dane z formularza wypełniają "pusty" obiekt NewPerson. Tutaj zostaje podjęta decyzja czy obiekt ma zostać dodany do bazy danych czy też z niej usunięty 
zwraca:  RedirectToPage("Index") w celu odświeżenia strony

private void ConnectDB(): Metoda, która łączy się z lokalną bazą danych przy każdym przeładowaniu strony w celu pobrania najważniejszych informacji. Automatycznie dodaje odebrane wyniki do listy, która jest użyta do wyświetlenia informacji na głównej stronie.
private void AddNewPersonDB(Person person): Metoda ta pozwala dodać nowy obiekt klasy "Person" do bazy danych. Wszystkie sprecyzowane w formularzu wartości dla tego obiektu zostają dodane w odpowiedniej formie do "query". person.BirthDay zostaje poddane formatowi: .ToString("O"), aby baza mogła poprawnie przyjąć datę urodzenia.

private void DeletePersonDB(NewPerson.Id): Metoda ta zostaje wywołana w OnPost. Jeśli użytkownik sprecyzował Id obiektu w formularzu do usunięcia, wtedy zostaje podjęta decyzja o wywołaniu tej metody. Przekazywane jest tylko id w celu sprecyzowania "query" do bazy danych.

Biblioteki:
MySql.Data: Obsługa bazy MySQL
EntityFrameworkCore: Migracja i utworzenie w MSSQL tabeli użytkowników

 # Rework Branch is available!
 - [x] Cleaned and Fixed code
 - [x] User Accounts moved to MySql
 - [x] Secured Connection string

Biblioteki: 
* Pomelo.EntityFrameworkCore.MySql: MySql Migration
