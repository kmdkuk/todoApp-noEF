CREATE TABLE todoAppTests.dbo.TodoItems
(
    Id CHAR(32) PRIMARY KEY NOT NULL,
    Name NVARCHAR(128),
    IsComplete BIT  
);
