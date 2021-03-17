# The_Pet_Mansion
Proiect in cadrul cursului de Dezvoltare a Aplicatiilor Web (semestrul 1, anul 2, FMI - UB)  
Platforma "Online Shop" implementata pentru un pet shop.

## Membrii echipei
* [Vlad Anghelache](https://github.com/vladanghelache)  
* [Denisa Iordache](https://github.com/denisaiordache)

## Detalii
Realizat in ASP.NET MCV 5  
Au fost utilizate:
* Entity Framework
* Identity Framework
* Bootstrap  

## Cerinta Proiectului
Sa se implementeze o platforma “Online shop” cu urmatoarele functionalitati:
- sa existe 4 tipuri de utilizatori: utilizator neinregistrat, inregistrat, 
colaborator, administrator   
- utilizatorul colaborator poate adauga produse in magazin. (0.5p). Acesta va 
trimite cereri de adaugare administratorului, iar acesta le poate respinge sau 
aproba.  Dupa aprobare produsele vor putea fi vizualizate in magazin
  
- produsele fac parte din categorii (create dinamic – adminul poate face CRUD 
pe categorii)   
- un produs are titlu, descriere, poza, pret, rating(1-5 stele), review-uri din 
partea utilizatorilor   
- utilizatorul partener poate sa editeze si sa stearga produsele adaugate de el
  
- utilizatorul neinregistrat va fi redirectionat sa isi faca un cont atunci cand 
incearca adaugarea unui produs in cos. Atunci cand nu are cont, el poate doar 
sa vizualizeze produsele si comentariile asociate acestora   
- atunci cand un utilizator devine utilizator inregistrat poate sa plaseze 
comenzi (sa adauge produse in cos) si sa lase review-uri, pe care ulterior le 
pot edita sau sterge   
- produsele pot fi cautate dupa denumire prin intermediul unui motor de 
cautare si pot fi sortate crescator, respectiv descrescator in functie de pret si 
numarul de stele   
- administratorul poate sterge si edita atat produse, cat si comentarii. Acesta 
poate, de asemenea, sa activeze sau sa revoce drepturile utilizatorilor.  
