USE LMS
GO


CREATE TABLE STUDENT(
    SID         varchar(30) PRIMARY KEY,
    Password    varbinary NOT NULL,
    FName       varchar(30) NOT NULL,
    MI          char(1),
    LName       varchar(30) NOT NULL,
    Street      varchar(30) NOT NULL,
    City        varchar(30) NOT NULL,
    State       varchar(30) NOT NULL,
    Zipcode     int NOT NULL
);

CREATE TABLE INSTRUCTOR(
    IID         varchar(30) PRIMARY KEY,
    Password    varbinary NOT NULL,
    FName       varchar(30) NOT NULL,
    MI          char(1),
    LName       varchar(30) NOT NULL,
    Street      varchar(30) NOT NULL,
    City        varchar(30) NOT NULL,
    State       varchar(30) NOT NULL,
    Zipcode     int NOT NULL
);

CREATE TABLE DEPARTMENT(
    DNo          int PRIMARY KEY,
    DName        varchar(30) NOT NULL UNIQUE,
    DLocation    varchar(30) NOT NULL
);

CREATE TABLE COURSE(
    CourseID       varchar(30) PRIMARY KEY,
    CName          varchar(30) NOT NULL,
    CreditHours    dec(3,1) NOT NULL,
    Level          int NOT NULL,
    Semester       varchar(10) CHECK(Semester IN('FALL', 'SPRING', 'SUMMER')),
    Year           int,
    DNo            int NOT NULL,
    IID            varchar(30),
    FOREIGN KEY(DNo) REFERENCES DEPARTMENT(DNo),
    FOREIGN KEY(IID) REFERENCES INSTRUCTOR(IID)
);

CREATE TABLE LECTURENOTE(
    LectureID         int IDENTITY(1,1),
    CourseID          varchar(30) NOT NULL,
    LTitle            varchar(30) NOT NULL,
    LFileLocation     varchar(100) NOT NULL,
    PRIMARY KEY(LectureID),
    FOREIGN KEY(CourseID) REFERENCES COURSE(CourseID)
);

CREATE TABLE ASSIGNMENT(
	AssignmentID     int IDENTITY(1,1),
    CourseID         varchar(30) NOT NULL,
    ATitle           varchar(30) NOT NULL,
    AFileLocation    varchar(100) NOT NULL,
    DueDate          datetime NOT NULL,
    AssignedDate     datetime NOT NULL,
    PRIMARY KEY(AssignmentID),
    FOREIGN KEY(CourseID) REFERENCES COURSE(CourseID)
);

CREATE TABLE ENROLLMENT(
    SID          varchar(30) NOT NULL,
    CourseID     varchar(30) NOT NULL,
    PRIMARY KEY(SID, CourseID),
    FOREIGN KEY(SID)      REFERENCES STUDENT(SID),
    FOREIGN KEY(CourseID) REFERENCES COURSE(CourseID)
);

CREATE TABLE SUBMISSION(
    SID               varchar(30) NOT NULL,
    AssignmentID      int NOT NULL,
	SubmissionDate    datetime NOT NULL,
    FileLocation      varchar(100) NOT NULL,
    Grade             dec(5,2),
    PRIMARY KEY(SID, AssignmentID),
    FOREIGN KEY(SID)            REFERENCES STUDENT(SID),
    FOREIGN KEY(AssignmentID)   REFERENCES ASSIGNMENT(AssignmentID)
);