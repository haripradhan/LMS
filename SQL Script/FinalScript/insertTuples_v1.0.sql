use LMS
GO

INSERT INTO Student VALUES 
('sid0001',0,'Micheal','S','Sandlin','19 Quaker Ridge Rd.','Bethel','CT',06801),
('sid0002',0,'John','I','Bender','1000 Coney Island Ave.','Brooklyn','NY',11230),
('sid0003',0,'Ryan','P','Bryant','2962 Dunedin Cv','Germantown','TN',38138),
('sid0004',0,'Mark','R','Vincent','1500 Vance Ave','Memphis','TN',38104),
('sid0005',0,'Nathan','','Keith','915 E 7th St. Apt 6L','Brooklyn','NY',11230),
('sid0006',0,'William','J','Smith','2202 S Central Ave','Phoenix','AZ',85004),
('sid0007',0,'Jacob','','Laskowski','6220 S Orange Blossom Trl','Orlando','FL',32809)

INSERT INTO Instructor VALUES 
('iid0001',0,'Todd','K','Diesel','1535 Hawkins Blvd','El Paso','TX',79925),
('iid0002',0,'Ben','S','Yang','102 5th St N','Clanton','AL',35045),
('iid0003',0,'Shannon','','Myers','6927 Old Seward Hwy','Anchorage','AK',99518),
('iid0004',0,'Anthony','','Buff','1000 Monte Sano Blvd Se','Huntsville','AL',35801)

INSERT INTO Department VALUES 
(1,'Computer Science','Technology Hall'),
(2,'Engineering','Shelby Hall'),
(3,'Chemistry','Wilson Hall')

INSERT INTO Course VALUES 
('cid0001','Operating Systems',3.0,1,'FALL',2013,1,'iid0001'),
('cid0002','Database Systems',3.0,1,'FALL',2013,1,'iid0002'),
('cid0003','Computer Architecture',3.0,1,'FALL',2013,1,'iid0002'),
('cid0004','Computer Organization',3.0,1,'FALL',2013,2,'iid0001'),
('cid0005','Intro To Embedded Computer Sys',3.0,1,'FALL',2013,2,'iid0003'),
('cid0006','Smartphone Programming',3.0,1,'FALL',2013,2,'iid0003'),
('cid0007','Structure Materials I',3.0,3,'FALL',2013,3,'iid0004'),
('cid0008','Advanced Chemical Dynamics',3.0,1,'FALL',2013,3,'iid0004')

INSERT INTO ENROLLMENT VALUES 
('sid0001','cid0001'),
('sid0001','cid0002'),
('sid0001','cid0003'),
('sid0002','cid0004'),
('sid0002','cid0005'),
('sid0002','cid0006'),
('sid0003','cid0007'),
('sid0004','cid0007'),
('sid0004','cid0001'),
('sid0005','cid0002'),
('sid0006','cid0001'),
('sid0007','cid0002')