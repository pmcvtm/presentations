-- These are already set up in the included fish.sqlite.
-- If you want to use a different empty file, use this script to initialize the tables we need.

CREATE TABLE IF NOT EXISTS Aquariums (
  Id INTEGER PRIMARY KEY AUTOINCREMENT,
  Name TEXT NOT NULL,
  Location TEXT NOT NULL,
  Capacity INTEGER NOT NULL,
  Cleanliness INTEGER NOT NULL
);

CREATE TABLE IF NOT EXISTS Fish (
  Id INTEGER PRIMARY KEY AUTOINCREMENT,
  Name TEXT NOT NULL,
  Variety TEXT NOT NULL,
  Status INTEGER NOT NULL,
  AquariumId INTEGER NULL,
  FOREIGN KEY(AquariumId) REFERENCES Aquarium(Id)
);

CREATE TABLE IF NOT EXISTS Decorations (
  Id INTEGER PRIMARY KEY AUTOINCREMENT,
  Name TEXT NOT NULL,
  Description TEXT NULL,
  AquariumId INTEGER NULL,
  FOREIGN KEY(AquariumId) REFERENCES Aquarium(Id)
);
