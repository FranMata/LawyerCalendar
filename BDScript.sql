USE LawyerCalendar
GO

CREATE TABLE Users(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	IdDocument INT,
	BirthDate VARCHAR(20),
	Name VARCHAR(150),
	PaymentMethodId INT,
	PaymentMethodData VARCHAR(200),

	FOREIGN KEY (PaymentMethodId) REFERENCES PaymentMethod(Id)
)

CREATE TABLE PaymentMethod(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(10)
)

CREATE TABLE Appointment(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	UserId INT,
	Date VARCHAR(20),
	SpecialtyId INT,

	FOREIGN KEY (UserId) REFERENCES Users(Id),
	FOREIGN KEY (SpecialtyId) REFERENCES Specialties(Id)
)

CREATE TABLE Specialties(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(20)
)

INSERT INTO Specialties(Name) VALUES
('Laboral'),
('Penal'),
('Familia'),
('Civil'),
('Comercio'),
('Notariado')

INSERT INTO PaymentMethod(Name) VALUES
('Tarjeta'),
('Depósito'),
('SINPE')
