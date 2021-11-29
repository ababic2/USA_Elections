# USA_Elections

## Demo

https://drive.google.com/file/d/157bYBqXzowj389MGM1-Q9IWqEoEhv0Jt/view?usp=sharing

## Implementacija
 - ASP.NET Core MVC

### Database 

- ERD

U budućnosti bi se mogle javiti potrebe za unos novih informacija, npr. info o kandidatu, broj stanovnika grada itd.
Stoga, baza se sastoji od tabela Candidate, Constitency, Vote jer je na ovaj način lakše u budućnosti upisivati nove record-e u bazu.

### Validacija
Netačni unosi se nalaze u posebnom error file-u kako bi se izbjeglo unošenje netačnih record-a u bazu.

- All activities with database(queries) are in DAO classes.


