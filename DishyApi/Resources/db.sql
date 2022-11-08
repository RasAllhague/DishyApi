CREATE TABLE Users (
    Id INTEGER AUTO_INCREMENT,
    UserName VARCHAR(50) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    CreateDate TIMESTAMP NOT NULL,
    ModifyDate TIMESTAMP,
    PRIMARY KEY(Id),
    UNIQUE(Email)
);

CREATE TABLE Rights (
    Id INTEGER NOT NULL,
    RightName VARCHAR(50) NOT NULL,
    Description VARCHAR(255) NOT NULL,
    CreateDate TIMESTAMP NOT NULL,
    PRIMARY KEY(Id),
    UNIQUE(RightName)
);

CREATE TABLE UserRights(
    Id INTEGER AUTO_INCREMENT,
    UserId INTEGER NOT NULL,
    RightId INTEGER NOT NULL,
    CreateDate TIMESTAMP NOT NULL,
    CreateUserId INTEGER,
    PRIMARY KEY(Id),
    UNIQUE(UserId, RightId),
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (RightId) REFERENCES Rights(Id),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE Images(
    Id INTEGER AUTO_INCREMENT,
    OriginalName VARCHAR(255) NOT NULL,
    InternalName VARCHAR(255) NOT NULL,
    Extension VARCHAR(10) NOT NULL,
    Size BIGINT NOT NULL,
    CreateUserId INTEGER NOT NULL,
    CreateDate TIMESTAMP NOT NULL,
    PRIMARY KEY(Id),
    FOREIGN KEY (CreateUserId) REFERENCES Users(Id) 
);

CREATE TABLE Ingredients(
    Id INTEGER AUTO_INCREMENT,
    Name VARCHAR(255) NOT NULL,
    Description VARCHAR(500),
    Notes VARCHAR(500),
    ImageId INTEGER,
    CreateUserId INTEGER NOT NULL,
    CreateDate TIMESTAMP NOT NULL,
    ModifyUserId INTEGER,
    ModifyDate TIMESTAMP,
    PRIMARY KEY(Id),
    UNIQUE(Name, CreateUserId),
    FOREIGN KEY (ImageId) REFERENCES Images(Id),
    FOREIGN KEY (CreateUserId) REFERENCES Users(Id),
    FOREIGN KEY (ModifyUserId) REFERENCES Users(Id) 
);

CREATE TABLE CategoryTypes(
    Id INTEGER,
    Name VARCHAR(50) NOT NULL,
    CreateDate TIMESTAMP NOT NULL,
    PRIMARY KEY (Id),
    UNIQUE(Name)
);

CREATE TABLE Categories(
    Id INTEGER AUTO_INCREMENT,
    Name VARCHAR(50) NOT NULL,
    Description VARCHAR(255),
    category_type_id INTEGER NOT NULL,
    CreateUserId INTEGER NOT NULL,
    CreateDate TIMESTAMP NOT NULL,
    ModifyUserId INTEGER,
    ModifyDate TIMESTAMP,
    PRIMARY KEY (Id),
    UNIQUE(Name, CreateUserId),
    FOREIGN KEY (category_type_id) REFERENCES CategoryTypes(Id),
    FOREIGN KEY (CreateUserId) REFERENCES Users(Id),
    FOREIGN KEY (ModifyUserId) REFERENCES Users(Id) 
);

CREATE TABLE IngredientCategories(
    Id INTEGER AUTO_INCREMENT,
    IngredientId INTEGER NOT NULL,
    CategoryId INTEGER NOT NULL,
    CreateUserId INTEGER NOT NULL,
    CreateDate TIMESTAMP NOT NULL,
    PRIMARY KEY (Id),
    UNIQUE(IngredientId, CategoryId, CreateUserId),
    FOREIGN KEY (IngredientId) REFERENCES Ingredients(Id),
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id),
    FOREIGN KEY (CreateUserId) REFERENCES Users(Id) 
);

CREATE TABLE Dishes(
    Id INTEGER AUTO_INCREMENT,
    Name VARCHAR(255) NOT NULL,
    Description VARCHAR(255),
    Notes VARCHAR(500),
    ImageId INTEGER,
    CreateUserId INTEGER NOT NULL,
    CreateDate TIMESTAMP NOT NULL,
    ModifyUserId INTEGER,
    ModifyDate TIMESTAMP,
    PRIMARY KEY(Id),
    FOREIGN KEY (ImageId) REFERENCES Images(Id),
    FOREIGN KEY (CreateUserId) REFERENCES Users(Id),
    FOREIGN KEY (ModifyUserId) REFERENCES Users(Id) 
);

CREATE TABLE DishCategories(
    Id INTEGER AUTO_INCREMENT,
    DishId INTEGER NOT NULL,
    CategoryId INTEGER NOT NULL,
    CreateUserId INTEGER NOT NULL,
    CreateDate TIMESTAMP NOT NULL,
    PRIMARY KEY (Id),
    UNIQUE(DishId, CategoryId, CreateUserId),
    FOREIGN KEY (DishId) REFERENCES Dishes(Id),
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id),
    FOREIGN KEY (CreateUserId) REFERENCES Users(Id) 
);

CREATE TABLE MeasurementUnits (
    Id INTEGER NOT NULL,
    Name VARCHAR(50) NOT NULL,
    PRIMARY KEY(Id),
    UNIQUE(Name)
);

CREATE TABLE DishIngredients(
    Id INTEGER AUTO_INCREMENT,
    DishId INTEGER NOT NULL,
    IngredientId INTEGER NOT NULL,
    BaseAmount FLOAT,
    MeasurementUnitId INTEGER,
    CreateUserId INTEGER NOT NULL,
    CreateDate TIMESTAMP NOT NULL,
    PRIMARY KEY (Id),
    UNIQUE(DishId, IngredientId, CreateUserId),
    FOREIGN KEY (DishId) REFERENCES Dishes(Id),
    FOREIGN KEY (IngredientId) REFERENCES Ingredients(Id),
    FOREIGN KEY (MeasurementUnitId) REFERENCES MeasurementUnits(Id),
    FOREIGN KEY (CreateUserId) REFERENCES Users(Id)
);

CREATE TABLE Foodplans (
    Id INTEGER AUTO_INCREMENT,
    Name VARCHAR(50) NOT NULL,
    Description VARCHAR(255) NOT NULL,
    ImageId INTEGER,
    NotifyUsers BOOLEAN NOT NULL,
    Deactivated BOOLEAN NOT NULL,
    OwningUserId INTEGER NOT NULL,
    CreateUserId INTEGER NOT NULL,
    CreateDate TIMESTAMP NOT NULL,
    ModifyUserId INTEGER,
    ModifyDate INTEGER,
    PRIMARY KEY(Id),
    FOREIGN KEY (ImageId) REFERENCES Images(Id),
    FOREIGN KEY (OwningUserId) REFERENCES Users(Id),
    FOREIGN KEY (CreateUserId) REFERENCES Users(Id),
    FOREIGN KEY (ModifyUserId) REFERENCES Users(Id)
);

CREATE TABLE FoodplanDishes(
    Id INTEGER AUTO_INCREMENT,
    FoodplanId INTEGER NOT NULL,
    DishId INTEGER NOT NULL,
    PlannedDate TIMESTAMP NOT NULL,
    AmountMultiplier FLOAT NOT NULL,
    CreateUserId INTEGER NOT NULL,
    CreateDate TIMESTAMP NOT NULL,
    PRIMARY KEY(Id),
    FOREIGN KEY (FoodplanId) REFERENCES Foodplans(Id),
    FOREIGN KEY (DishId) REFERENCES Dishes(Id),
    FOREIGN KEY (CreateUserId) REFERENCES Users(Id)
);

CREATE TABLE FoodplanUserRights(
    Id INTEGER AUTO_INCREMENT,
    FoodplanId INTEGER NOT NULL,
    UserId INTEGER NOT NULL,
    RightId INTEGER NOT NULL,
    ValidFrom TIMESTAMP,
    ValidUntil TIMESTAMP,
    Deactivated BOOLEAN,
    CreateUserId INTEGER NOT NULL,
    CreateDate TIMESTAMP NOT NULL,
    ModifyUserId INTEGER,
    ModifyDate TIMESTAMP,
    PRIMARY KEY(Id),
    UNIQUE(FoodplanId, UserId, RightId),
    FOREIGN KEY (FoodplanId) REFERENCES Foodplans(Id),
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (RightId) REFERENCES Rights(Id),
    FOREIGN KEY (CreateUserId) REFERENCES Users(Id),
    FOREIGN KEY (ModifyUserId) REFERENCES Users(Id)
);

CREATE TABLE DishUserRights(
    Id INTEGER AUTO_INCREMENT,
    DishId INTEGER NOT NULL,
    UserId INTEGER NOT NULL,
    RightId INTEGER NOT NULL,
    ValidFrom TIMESTAMP,
    ValidUntil TIMESTAMP,
    Deactivated BOOLEAN,
    CreateUserId INTEGER NOT NULL,
    CreateDate TIMESTAMP NOT NULL,
    ModifyUserId INTEGER,
    ModifyDate TIMESTAMP,
    PRIMARY KEY(Id),
    UNIQUE(DishId, UserId, RightId),
    FOREIGN KEY (DishId) REFERENCES Dishes(Id),
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (RightId) REFERENCES Rights(Id),
    FOREIGN KEY (CreateUserId) REFERENCES Users(Id),
    FOREIGN KEY (ModifyUserId) REFERENCES Users(Id)
);

/* DROP TABLE SQLs */

DROP TABLE IF EXISTS DishUserRights;
DROP TABLE IF EXISTS FoodplanUserRights;
DROP TABLE IF EXISTS FoodplanDishes;
DROP TABLE IF EXISTS Foodplans;
DROP TABLE IF EXISTS DishIngredients;
DROP TABLE IF EXISTS MeasurementUnits;
DROP TABLE IF EXISTS DishCategories;
DROP TABLE IF EXISTS IngredientCategories;
DROP TABLE IF EXISTS Dishes;
DROP TABLE IF EXISTS IngredientCategories;
DROP TABLE IF EXISTS Categories;
DROP TABLE IF EXISTS CategoryTypes;
DROP TABLE IF EXISTS Ingredients;
DROP TABLE IF EXISTS Images;
DROP TABLE IF EXISTS UserRights;
DROP TABLE IF EXISTS Rights;
DROP TABLE IF EXISTS Users;

/* Sample data creation SQLs */

INSERT INTO Rights 
(Id, RightName, Description, CreateDate)
VALUES
(1, "viewer", "Collectiv right for readonly viewer Users in the current context", NOW()),
(2, "user", "Collectiv right for a normal (reading and editing) user in the current context", NOW()),
(3, "admin", "Collectiv right for admin privilges in the current context.", NOW());
