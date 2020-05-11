USE EscapeFromTheWoods;

CREATE TABLE WoodRecords (
recordId int primary key,
woodID int,
treeID int,
x int,
y int);

CREATE TABLE MonkeyRecords(
recordId int primary key,
monkeyId int,
monkeyName nVarchar(255),
woodId int ,
seqnr int,
treeID int,
x int ,
y int);

CREATE TABLE Logs(
Id int primary key,
woodID int,
monkeyID int,
[message] nVarchar(255));

