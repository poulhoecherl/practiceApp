CREATE TABLE Session (
    Id        INTEGER     NOT NULL
                          CONSTRAINT PK_Sessions PRIMARY KEY AUTOINCREMENT,
    StartTime TEXT        NOT NULL,
    EndTime   TEXT        NOT NULL,
    UserId    INTEGER     NOT NULL
                          DEFAULT (1),
    Activity  TEXT (1024),
    Duration  INTEGER     NOT NULL
                          DEFAULT (1),
    Notes     TEXT (2048) 
);
