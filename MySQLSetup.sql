USE matrixdb;

CREATE TABLE Gebruiker (
    user_id INT AUTO_INCREMENT PRIMARY KEY,
    wachtwoord CHAR(60) NOT NULL,
    actief BOOLEAN NOT NULL DEFAULT TRUE,
    naam VARCHAR(100) NOT NULL,
    adres VARCHAR(255),
    postcode VARCHAR(20),
    woonplaats VARCHAR(100),
    telefoon VARCHAR(20),
    email VARCHAR(100)
);

CREATE TABLE Categorie (
    categorie_id INT AUTO_INCREMENT PRIMARY KEY,
    categorie_naam VARCHAR(100) NOT NULL
);

CREATE TABLE Producten (
    product_id INT AUTO_INCREMENT PRIMARY KEY,
    categorie_id INT,
    naam VARCHAR(100) NOT NULL,
    beschrijving VARCHAR(255),
    prijs DECIMAL(10,2) NOT NULL,
    voorraad INT NOT NULL DEFAULT 0,
    afbeelding BLOB,
    FOREIGN KEY (categorie_id) REFERENCES Categorie(categorie_id)
);

CREATE TABLE Bestelling (
    bestel_id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT NOT NULL,
    datum DATE NOT NULL,
    status VARCHAR(50) NOT NULL,
    FOREIGN KEY (user_id) REFERENCES Gebruiker(user_id)
);

CREATE TABLE Bestelregel (
    bestelregel_id INT AUTO_INCREMENT PRIMARY KEY,
    bestel_id INT NOT NULL,
    product_id INT NOT NULL,
    aantal INT NOT NULL DEFAULT 1,
    prijs DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (bestel_id) REFERENCES Bestelling(bestel_id),
    FOREIGN KEY (product_id) REFERENCES Producten(product_id)
);