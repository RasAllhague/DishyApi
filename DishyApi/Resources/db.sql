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
DROP TABLE IF EXISTS Categories;
DROP TABLE IF EXISTS CategoryTypes;
DROP TABLE IF EXISTS Ingredients;
DROP TABLE IF EXISTS Images;
DROP TABLE IF EXISTS UserRights;
DROP TABLE IF EXISTS Rights;
DROP TABLE IF EXISTS Users;

CREATE TABLE IF NOT EXISTS Users(
    Id INTEGER AUTO_INCREMENT,
    UserName VARCHAR(50) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    CreateDate TIMESTAMP NOT NULL,
    ModifyDate TIMESTAMP,
    PRIMARY KEY(Id),
    UNIQUE(Email)
);

CREATE TABLE IF NOT EXISTS Rights(
    Id INTEGER NOT NULL,
    RightName VARCHAR(50) NOT NULL,
    Description VARCHAR(255) NOT NULL,
    CreateDate TIMESTAMP NOT NULL,
    PRIMARY KEY(Id),
    UNIQUE(RightName)
);

CREATE TABLE IF NOT EXISTS UserRights(
    Id INTEGER AUTO_INCREMENT,
    UserId INTEGER NOT NULL,
    RightId INTEGER NOT NULL,
    CreateDate TIMESTAMP NOT NULL,
    CreateUserId INTEGER,
    PRIMARY KEY(Id),
    UNIQUE(UserId, RightId),
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (RightId) REFERENCES Rights(Id)
);

CREATE TABLE IF NOT EXISTS Images(
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

CREATE TABLE IF NOT EXISTS Ingredients(
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
    FOREIGN KEY (CreateUserId) REFERENCES Users(Id)
);

CREATE TABLE IF NOT EXISTS CategoryTypes(
    Id INTEGER,
    Name VARCHAR(50) NOT NULL,
    CreateDate TIMESTAMP NOT NULL,
    PRIMARY KEY (Id),
    UNIQUE(Name)
);

CREATE TABLE IF NOT EXISTS Categories(
    Id INTEGER AUTO_INCREMENT,
    Name VARCHAR(50) NOT NULL,
    Description VARCHAR(255),
    CategoryTypeId INTEGER NOT NULL,
    CreateUserId INTEGER NOT NULL,
    CreateDate TIMESTAMP NOT NULL,
    ModifyUserId INTEGER,
    ModifyDate TIMESTAMP,
    PRIMARY KEY (Id),
    UNIQUE(Name, CategoryTypeId, CreateUserId),
    FOREIGN KEY (CategoryTypeId) REFERENCES CategoryTypes(Id),
    FOREIGN KEY (CreateUserId) REFERENCES Users(Id)
);

CREATE TABLE IF NOT EXISTS IngredientCategories(
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

CREATE TABLE IF NOT EXISTS Dishes(
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
    FOREIGN KEY (CreateUserId) REFERENCES Users(Id)
);

CREATE TABLE IF NOT EXISTS DishCategories(
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

CREATE TABLE IF NOT EXISTS MeasurementUnits(
    Id INTEGER NOT NULL,
    Name VARCHAR(50) NOT NULL,
    PRIMARY KEY(Id),
    UNIQUE(Name)
);

CREATE TABLE IF NOT EXISTS DishIngredients(
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
    FOREIGN KEY (CreateUserId) REFERENCES Users(Id)
);

CREATE TABLE IF NOT EXISTS Foodplans(
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
    FOREIGN KEY (OwningUserId) REFERENCES Users(Id),
    FOREIGN KEY (CreateUserId) REFERENCES Users(Id)
);

CREATE TABLE IF NOT EXISTS FoodplanDishes(
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

CREATE TABLE IF NOT EXISTS FoodplanUserRights(
    Id INTEGER AUTO_INCREMENT,
    FoodplanId INTEGER NOT NULL,
    UserId INTEGER NOT NULL,
    RightId INTEGER NOT NULL,
    ValidFrom TIMESTAMP,
    ValidUntil TIMESTAMP,
    Deactivated BOOLEAN,
    CreateUserId INTEGER NOT NULL,
    CreateDate TIMESTAMP NOT NULL,
    ModifyDate TIMESTAMP,
    PRIMARY KEY(Id),
    UNIQUE(FoodplanId, UserId, RightId),
    FOREIGN KEY (FoodplanId) REFERENCES Foodplans(Id),
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (RightId) REFERENCES Rights(Id),
    FOREIGN KEY (CreateUserId) REFERENCES Users(Id)
);

CREATE TABLE IF NOT EXISTS DishUserRights(
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
    FOREIGN KEY (CreateUserId) REFERENCES Users(Id)
);

/* Sample data creation SQLs */

INSERT INTO Rights 
(Id, RightName, Description, CreateDate)
VALUES
(1, "viewer", "Collectiv right for readonly viewer Users in the current context", NOW()),
(2, "user", "Collectiv right for a normal (reading and editing) user in the current context", NOW()),
(3, "admin", "Collectiv right for admin privilges in the current context.", NOW());

INSERT INTO `Users` 
VALUES 
(1, 'Test', 'test@test.test', 'AQAAAAEAACcQAAAAEDXOF8wc49Hk4uqcuk5UpDQe1ZnI6MEbN3WozyY9YBV7QzbFk8GR0Wy1TaG8TCG0pw==', NOW(), NULL);

INSERT INTO `CategoryTypes`
VALUES
(1, 'Dish', NOW()),
(2, 'Ingredient', NOW()),
(3, 'Foodplan', NOW());