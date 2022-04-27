Create Table People(
    IdPeople int not null primary key,
    Name varchar(150) not null,
    Created timestamp default current_timestamp
);