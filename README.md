# CinemaBooking
Acest proiect are ca tematică rezervarea de bilete la cinema plus funcționalitatea de a adăuga filme într-o listă de favorite.<br/>
Diagrama bazei de date:
![image](https://github.com/user-attachments/assets/4d0fdced-0fa7-4369-b0b8-9e7df297fd0b)<br/>
Structura bazei de date permite gestionarea eficientă a informațiilor despre cinematografe, săli, filme, proiecții, rezervări și utilizatori.<br/>
<h1>Structura bazei de date:</h1>
<ol>
  <li>Tabelul Cinemas</li>
  Acest tabel stochează informații despre cinematografe. Fiecare cinematograf are următoarele câmpuri:<br/>
  <ul>
    <li>CinemaID(cheie primară): Identificator unic pentru un cinematograf.</li>
    <li>Name: Numele cinematografului.</li>
    <li>Address: Adresa cinematografului.</li>
    <li>City: Orașul în care se află cinematograful.</li>
  </ul>
  <li>Tabelul Halls</li>
  Tabelul Halls reprezintă sălile dintr-un cinematograf. Fiecare sală este asociată unui cinematograf specific.<br/>
  <ul>
    <li>HallID (cheie primară): Identificator unic pentru o sală.</li>
    <li>CinemaID (cheie străină): Referință către cinematograful căruia îi aparține sala.</li>
    <li>HallNumber: Numărul sălii.</li>
    <li>Capacity: Capacitatea totală a sălii (numărul maxim de locuri).</li>
  </ul>
  <li>Tabelul Movies</li>
  Acest tabel stochează informații despre filmele disponibile pentru proiecții.<br/>
  <ul>
    <li>MovieID (cheie primară): Identificator unic pentru un film.</li>
    <li>Title: Titlul filmului.</li>
    <li>ImgURL: URL-ul unei imagini reprezentative pentru film.</li>
    <li>Description: O scurtă descriere a filmului.</li>
    <li>DurationMinutes: Durata filmului în minute.</li>
    <li>ReleaseDate: Data lansării filmului.</li>
  </ul>
  <li>Tabelul Screenings</li>
  Tabelul Screenings reprezintă proiecțiile programate ale filmelor.<br/>
  <ul>
    <li>ScreeningID (cheie primară): Identificator unic pentru o proiecție.</li>
    <li>MovieID (cheie străină): Referință către filmul proiectat.</li>
    <li>HallID (cheie străină): Referință către sala în care are loc proiecția.</li>
    <li>ScreeningTime: Ora și data proiecției.</li>
    <li>Price: Prețul biletului pentru proiecție.</li>
  </ul>
  <li>Tabelul Reservations</li>
  Tabelul Reservations permite gestionarea rezervărilor efectuate de utilizatori.<br/>
  <ul>
    <li>ReservationID (cheie primară): Identificator unic pentru o rezervare.</li>
    <li>ScreeningID (cheie străină): Referință către proiecția rezervată.</li>
    <li>UserID: Identificatorul utilizatorului care a făcut rezervarea.</li>
    <li>NumberOfSeats: Numărul de locuri rezervate.</li>
  </ul>
  <li>Tabelul ReservedSeats</li>
  Acest tabel oferă detalii despre locurile rezervate individual.<br/>
  <ul>
    <li>ReservedSeatID (cheie primară): Identificator unic pentru un loc rezervat.</li>
    <li>ReservationID (cheie străină): Referință către rezervarea asociată.</li>
    <li>SeatNumber: Numărul locului rezervat în sală.</li>
  </ul>
  <li>Tabelul FavouriteMovies</li>
  Acest tabel stochează lista de filme favorite pentru fiecare utilizator.<br/>
  <ul>
    <li>FavouriteID (cheie primară): Identificator unic pentru o intrare din lista de favorite.</li>
    <li>MovieID (cheie străină): Referință către filmul favorit.</li>
    <li>UserID: Identificatorul utilizatorului care a adăugat filmul în lista de favorite.</li>
  </ul>
</ol>
<ol>
  <h1><li>Roluri în Aplicație</li></h1>
  Aplicația suportă două roluri principale:<br/>
  <ul>
    <li>User (Utilizator): Accesează aplicația pentru a vizualiza filmele disponibile, programa rezervări sau gestiona lista de filme favorite.</li>
    <li>Admin: Gestionează datele aplicației, precum adăugarea/editarea filmelor, proiecțiilor, sălilor și cinematografelor.</li>
  </ul>
  <h1><li>Fluxul de Funcționare pentru User</li></h1>
  <ol>
    <li>Autentificare și Înregistrare:
    <ul>
      <li>Utilizatorul accesează pagina principală.</li>
      <li>Se poate înregistra (creare cont) sau autentifica pentru a accesa funcționalitățile personalizate (rezervări, favorite).</li>
    </ul>
    </li>
    <li>Vizualizare Filme Disponibile:
    <ul>
      <li>Utilizatorul poate vizualiza o listă a filmelor disponibile din baza de date (Movies).</li>
      <li>Filmele pot fi afișate împreună cu detalii precum descrierea, durata, data lansării și imaginea.</li>
    </ul>
    </li>
    <li>Programare Rezervări:
    <ul>
      <li>După selectarea unui film, utilizatorul poate vizualiza proiecțiile disponibile din tabelul Screenings (informații despre ora, sala, preț etc.).</li>
      <li>Utilizatorul selectează numărul de locuri dorit și finalizează rezervarea. Datele sunt salvate în tabelele Reservations și ReservedSeats.</li>
    </ul>
    </li>
    <li>Gestionarea Filmelor Favorite:
    <ul>
      <li>Utilizatorul poate marca un film drept favorit, salvând intrarea în tabelul FavouriteMovies.</li>
      <li>Poate gestiona lista sa de filme favorite, vizualizând și ștergând titluri.</li>
    </ul>
    </li>
    <li>Vizualizare Rezervări:
    <ul>
      <li>Utilizatorul poate accesa secțiunea "Rezervări", unde sunt afișate toate rezervările efectuate, inclusiv detalii despre proiecții și locuri.</li>
    </ul>
    </li>
  </ol>
  <h1><li>Fluxul de Funcționare pentru Admin</li></h1>
  <ol>
    <li>Autentificare:
    <ul>
      <li>Administratorul se autentifică folosind un cont special cu privilegii de admin.</li>
    </ul>
    </li>
    <li>Gestionarea Cinematografelor și Sălilor:
    <ul>
      <li>Adminul poate adăuga, edita sau șterge informații despre cinematografe (Cinemas) și sălile lor (Halls).</li>
    </ul>
    </li>
    <li>Gestionarea Filmelor:
    <ul>
      <li>Adminul poate adăuga noi filme în baza de date (Movies), inclusiv detalii precum titlu, descriere, imagine, durată și data lansării.</li>
      <li>Poate edita sau șterge informații despre filmele existente.</li>
    </ul>
    </li>
    <li>Programarea Proiecțiilor:
    <ul>
      <li>Adminul configurează proiecțiile filmelor (Screenings), precizând sala, ora proiecției și prețul biletului.</li>
    </ul>
    </li>
    <li>Monitorizarea Rezervărilor:
    <ul>
      <li>Adminul poate accesa toate rezervările efectuate de utilizatori (Reservations) pentru a monitoriza activitatea.</li>
    </ul>
    </li>
  </ol>
</ol>
