CREATE TABLE notification
(
   Id serial,
   Message varchar(240),
   CreatedAt timestamp not null,
   IsSend boolean,
   CardId integer
   
);

CREATE TABLE card
(
    id serial,
    phone varchar(20) not null,
    createdAt timestamp not null
);
