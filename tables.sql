--------------------------------------------------
-- Удаляем существующие таблицы, если они есть
--------------------------------------------------
IF OBJECT_ID('dbo.BuildComponents', 'U') IS NOT NULL DROP TABLE dbo.BuildComponents;
IF OBJECT_ID('dbo.SavedBuilds', 'U') IS NOT NULL DROP TABLE dbo.SavedBuilds;
IF OBJECT_ID('dbo.OrderItems', 'U') IS NOT NULL DROP TABLE dbo.OrderItems;
IF OBJECT_ID('dbo.Orders', 'U') IS NOT NULL DROP TABLE dbo.Orders;
IF OBJECT_ID('dbo.Reviews', 'U') IS NOT NULL DROP TABLE dbo.Reviews;
IF OBJECT_ID('dbo.Announcements', 'U') IS NOT NULL DROP TABLE dbo.Announcements;
IF OBJECT_ID('dbo.Specifications', 'U') IS NOT NULL DROP TABLE dbo.Specifications;
IF OBJECT_ID('dbo.UserFavorites', 'U') IS NOT NULL DROP TABLE dbo.UserFavorites;
IF OBJECT_ID('dbo.Builds', 'U') IS NOT NULL DROP TABLE dbo.Builds;
IF OBJECT_ID('dbo.Components', 'U') IS NOT NULL DROP TABLE dbo.Components;
IF OBJECT_ID('dbo.Categories', 'U') IS NOT NULL DROP TABLE dbo.Categories;
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE dbo.Users;

--------------------------------------------------
-- Создаем таблицу пользователей (Users)
--------------------------------------------------
CREATE TABLE dbo.Users
(
    UserId        INT           IDENTITY(1,1) PRIMARY KEY,
    Username      NVARCHAR(50)  NOT NULL,
    Password      NVARCHAR(100) NOT NULL,
    Email         NVARCHAR(100),
    Phone         NVARCHAR(MAX),
    Address       NVARCHAR(MAX),
    IsAdmin       BIT           NOT NULL DEFAULT 0,
    IsBlocked     BIT           NOT NULL DEFAULT 0,
    Name          NVARCHAR(MAX) NOT NULL DEFAULT 'User',
    AvatarPath    NVARCHAR(MAX) NOT NULL DEFAULT 'default-avatar.png'
);
  
--------------------------------------------------
-- Создаем таблицу категорий (Categories)
--------------------------------------------------
CREATE TABLE dbo.Categories
(
    CategoryID   INT           IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(MAX) NOT NULL
);
  
--------------------------------------------------
-- Создаем таблицу компонентов (Components)
--------------------------------------------------
CREATE TABLE dbo.Components
(
    ComponentID   INT           IDENTITY(1,1) PRIMARY KEY,
    Name          NVARCHAR(100) NOT NULL,
    Description   NVARCHAR(1000),
    Price         DECIMAL(18,2) NOT NULL,
    Stock         INT           NOT NULL,
    Manufacturer  NVARCHAR(100) NOT NULL,
    ImagePath     NVARCHAR(255),
    CategoryID    INT           NOT NULL,
    CONSTRAINT FK_Components_Categories FOREIGN KEY (CategoryID)
         REFERENCES dbo.Categories(CategoryID)
);
  
--------------------------------------------------
-- Создаем таблицу спецификаций (Specifications)
--------------------------------------------------
CREATE TABLE dbo.Specifications
(
    SpecificationID INT           IDENTITY(1,1) PRIMARY KEY,
    ComponentID     INT           NOT NULL,
    SpecText        NVARCHAR(MAX) NOT NULL,
    CONSTRAINT FK_Specifications_Components FOREIGN KEY (ComponentID)
         REFERENCES dbo.Components(ComponentID)
);
  
--------------------------------------------------
-- Создаем таблицу отзывов (Reviews)
--------------------------------------------------
CREATE TABLE Reviews (
    ReviewId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    ComponentId INT NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    Rating INT NOT NULL,
    ReviewDate DATETIME NOT NULL,
    IsApproved BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (ComponentId) REFERENCES Components(ComponentID)
);
  
--------------------------------------------------
-- Создаем таблицу избранного (UserFavorites)
--------------------------------------------------
CREATE TABLE dbo.UserFavorites
(
    UserId      INT           NOT NULL,
    ComponentID INT           NOT NULL,
    CONSTRAINT PK_UserFavorites PRIMARY KEY (UserId, ComponentID),
    CONSTRAINT FK_UserFavorites_Users FOREIGN KEY (UserId)
         REFERENCES dbo.Users(UserId)
         ON DELETE CASCADE,
    CONSTRAINT FK_UserFavorites_Components FOREIGN KEY (ComponentID)
         REFERENCES dbo.Components(ComponentID)
         ON DELETE CASCADE
);
  
--------------------------------------------------
-- Создаем таблицу сборок (Builds)
--------------------------------------------------
CREATE TABLE dbo.Builds
(
    BuildId      INT           IDENTITY(1,1) PRIMARY KEY,
    UserId       INT           NOT NULL,
    BuildSummary NVARCHAR(MAX),
    TotalPrice   DECIMAL(18,2) NOT NULL,
    CreatedDate  DATETIME      NOT NULL,
    CONSTRAINT FK_Builds_Users FOREIGN KEY (UserId)
         REFERENCES dbo.Users(UserId)
         ON DELETE CASCADE
);
  
--------------------------------------------------
-- Создаем таблицу заказов (Orders)
--------------------------------------------------
CREATE TABLE dbo.Orders
(
    OrderId         INT           IDENTITY(1,1) PRIMARY KEY,
    UserId          INT           NOT NULL,
    OrderDate       DATETIME      NOT NULL,
    TotalAmount     DECIMAL(18,2) NOT NULL,
    Status          NVARCHAR(50)  NOT NULL DEFAULT 'New',
    DeliveryAddress NVARCHAR(MAX),
    ContactPhone    NVARCHAR(MAX),
    OrderDetails    NVARCHAR(MAX),
    CONSTRAINT FK_Orders_Users FOREIGN KEY (UserId)
         REFERENCES dbo.Users(UserId)
         ON DELETE CASCADE
);
  
--------------------------------------------------
-- Создаем таблицу элементов заказа (OrderItems)
--------------------------------------------------
CREATE TABLE dbo.OrderItems
(
    OrderItemId INT           IDENTITY(1,1) PRIMARY KEY,
    OrderId     INT           NOT NULL,
    ComponentId INT           NOT NULL,
    Quantity    INT           NOT NULL,
    Price       DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_OrderItems_Orders FOREIGN KEY (OrderId)
         REFERENCES dbo.Orders(OrderId)
         ON DELETE CASCADE,
    CONSTRAINT FK_OrderItems_Components FOREIGN KEY (ComponentId)
         REFERENCES dbo.Components(ComponentID)
         ON DELETE CASCADE
);

--------------------------------------------------
-- Создаем таблицу объявлений (Announcements)
--------------------------------------------------
CREATE TABLE dbo.Announcements
(
    AnnouncementId INT           IDENTITY(1,1) PRIMARY KEY,
    Title         NVARCHAR(200)  NOT NULL,
    Content       NVARCHAR(MAX)  NOT NULL,
    CreatedDate   DATETIME       NOT NULL DEFAULT GETDATE(),
    IsActive      BIT            NOT NULL DEFAULT 1,
    CreatedByUserId INT          NOT NULL,
    CONSTRAINT FK_Announcements_Users FOREIGN KEY (CreatedByUserId)
         REFERENCES dbo.Users(UserId)
         ON DELETE CASCADE
);

--------------------------------------------------
-- Создаем таблицу сохраненных сборок (SavedBuilds)
--------------------------------------------------
CREATE TABLE dbo.SavedBuilds
(
    BuildId      INT           IDENTITY(1,1) PRIMARY KEY,
    UserId       INT           NOT NULL,
    BuildName    NVARCHAR(100) NOT NULL,
    CreatedDate  DATETIME      NOT NULL DEFAULT GETDATE(),
    TotalPrice   DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_SavedBuilds_Users FOREIGN KEY (UserId)
         REFERENCES dbo.Users(UserId)
         ON DELETE CASCADE
);

--------------------------------------------------
-- Создаем таблицу компонентов в сохраненных сборках (BuildComponents)
--------------------------------------------------
CREATE TABLE dbo.BuildComponents
(
    BuildComponentId INT IDENTITY(1,1) PRIMARY KEY,
    BuildId         INT NOT NULL,
    ComponentId     INT NOT NULL,
    CONSTRAINT FK_BuildComponents_SavedBuilds FOREIGN KEY (BuildId)
         REFERENCES dbo.SavedBuilds(BuildId)
         ON DELETE CASCADE,
    CONSTRAINT FK_BuildComponents_Components FOREIGN KEY (ComponentId)
         REFERENCES dbo.Components(ComponentID)
         ON DELETE CASCADE
);


CREATE TABLE Orders (
    OrderId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    OrderSummary NVARCHAR(MAX),
    TotalPrice DECIMAL(18,2) NOT NULL,
    Status INT NOT NULL, -- хранит значение enum OrderStatus
    CreatedDate DATETIME NOT NULL,
    DeliveryAddress NVARCHAR(MAX),
    ContactPhone NVARCHAR(MAX),
    OrderDetails NVARCHAR(MAX),
    OrderDate DATETIME NOT NULL,
    TotalAmount DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_Orders_Users FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE TABLE OrderItems (
    OrderItemId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    ComponentID INT NOT NULL,
    Quantity INT NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_OrderItems_Orders FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    CONSTRAINT FK_OrderItems_Components FOREIGN KEY (ComponentID) REFERENCES Components(ComponentID)
);