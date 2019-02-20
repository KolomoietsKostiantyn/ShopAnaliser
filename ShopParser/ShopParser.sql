-- ShopAnalizer
CREATE TABLE Currency
(
	Id Int PRIMARY KEY NOT NULL IDENTITY(0, 1),
	Name VARCHAR(5) NOT NULL UNIQUE,
);

CREATE TABLE Shops
(
	Id Int PRIMARY KEY NOT NULL IDENTITY(0, 1),
	Name VARCHAR(20) NOT NULL UNIQUE,
);

CREATE TABLE Categories
(
	Id Int PRIMARY KEY NOT NULL IDENTITY(0, 1),
	Name VARCHAR(100)NOT NULL,
	ShopsId Int NOT NULL REFERENCES Shops(Id),
);

CREATE TABLE CategoriesPath
(
	CategoriesId Int NOT NULL REFERENCES Categories (Id),
	PathToCategory VARCHAR(500)
)

CREATE TABLE AllGoods
(
	Id Int PRIMARY KEY NOT NULL IDENTITY(0, 1),
	ShopId Int NOT NULL REFERENCES Shops(Id),
	Price float NOT NULL,
	ImagePath VARCHAR(50),
	ProductName VARCHAR(500) NOT NULL,
	IdOnSite VARCHAR(50) NOT NULL,
	ReferenceToTheOriginal VARCHAR(500),
	CurrencyId Int NOT NULL REFERENCES Currency (Id),
	CategoriesId Int NOT NULL REFERENCES Categories (Id),

);

CREATE TABLE ChangeDynamics
(
	Id int PRIMARY KEY NOT NULL IDENTITY(0, 1),
	AllGoodsId Int NOT NULL REFERENCES AllGoods(Id),
	OldPrice float NOT NULL,
	NewPrice float NOT NULL,
	UpdateTime DATETIME NOT NULL
);

CREATE TABLE GoodsDescription
(
	Id int PRIMARY KEY NOT NULL IDENTITY(0, 1),
	AllGoodsId Int NOT NULL REFERENCES AllGoods(Id),
	Specification NVARCHAR(MAX)
)

