# Recruitment-Task
## Summary
Aplikacja zakłada używanie bazy MySQL w celu przechowywania informacji na temat poszczególnych kontaktów oraz danych logowania użytkowników.

*W celu włączenia funkcjonalności logowania należy przeprowadzić migrację w konsoli menedżera pakietów: 1)PM>add-migration 2)PM> update-database*

## Klasy:
Contact - Klasa bazowa, której pola pokrywają się z kolumnami klientów z bazy danych. Klasa ta została użyta w celu tworzenia obiektów kontaków, których dane pochodzą z serwera. 

## Metody:

TODO

Biblioteki:
* Pomelo.EntityFrameworkCore.MySql: MySql Migration
MySql.Data: Obsługa bazy MySQL
EntityFrameworkCore: Migracja i utworzenie w MSSQL tabeli użytkowników

 # Rework Branch is available!
 - [x] Cleaned and Fixed code
 - [x] User Accounts moved to MySql
 - [x] Secured Connection string
 - [x] Data Validation
 
 **Rework Branch has been merged!**

Biblioteki: 
* Pomelo.EntityFrameworkCore.MySql: MySql Migration
