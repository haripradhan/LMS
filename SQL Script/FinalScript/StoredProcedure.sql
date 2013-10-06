USE [LMS]
GO

/****** Object:  StoredProcedure [dbo].[spDeleteAssignment]    Script Date: 4/11/2013 11:14:33 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spDeleteAssignment] 
	@assignmentID int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DELETE from ASSIGNMENT
	where AssignmentID = @assignmentID
	
END
GO


CREATE PROCEDURE [dbo].[spDeleteLectureNote] 
	@lectureID int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DELETE FROM LectureNote
	WHERE LectureID = @lectureID
	
END
GO


CREATE PROCEDURE [dbo].[spGetAssignmentByCourse] 
	@courseID nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT AssignmentID, ATitle , AFileLocation, DueDate, AssignedDate
   FROM Assignment 
   where CourseID = @courseID
END
GO


CREATE PROCEDURE [dbo].[spGetEnrolledCourseListByInstructor] 
	@id nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT c.CName, i.FName, i.MI, i.LName
   FROM Course AS c, Instructor as i
   where c.IID = @id and c.IID = i.IID 
END
GO


CREATE PROCEDURE [dbo].[spGetEnrolledCourseListByStudent] 
		@id nvarchar(50)
	AS
	BEGIN
		-- SET NOCOUNT ON added to prevent extra result sets from
		-- interfering with SELECT statements.
		SET NOCOUNT ON;

	   SELECT  c.CourseID AS CourseID, c.CName AS CNAME, i.FName as FName, i.MI as MI, i.LName as LNAME
	   FROM Course AS c, Enrollment AS e, Instructor as i
	   where e.sid = @id and e.CourseID = c.CourseID and c.IID = i.IID
	END
GO


CREATE PROCEDURE [dbo].[spGetGradeGroupByAssignmentForStudent] 
	@courseID int,
	@sid nvarchar(50)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT a.ATitle, s.Grade
	FROM Submission AS s, Assignment AS a
	WHERE s.SID = @sid AND a.AssignmentID = s.AssignmentID AND a.CourseID = @courseID
	
END
GO


CREATE PROCEDURE [dbo].[spGetGradeGroupByStudent] 
	@courseID int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT s.SID, s.FName, s.MI, s.LName, SUM(su.Grade)
	FROM Student as s, Submission as su, Assignment as a
	where s.SID = su.SID and su.AssignmentID = a.AssignmentID and a.CourseID = @courseID
	Group by s.SID, s.FName,s.MI, s.LName
	order by s.FName
END
GO



CREATE PROCEDURE [dbo].[spGetInstructor] 
	@iid varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT FName, MI, LName from Instructor WHERE IID = @iid
END
GO



CREATE PROCEDURE [dbo].[spGetLectureNoteByCourse] 
	@courseID nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT LectureID, LTitle , LFileLocation
   FROM LectureNote
   where CourseID = @courseID
END
GO



CREATE PROCEDURE [dbo].[spGetStudent]
	-- Add the parameters for the stored procedure here
	@sid varchar(50) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	SELECT Fname,MI,LName FROM Student WHERE SID = @sid
END
GO


CREATE PROCEDURE [dbo].[spInsertUpdateAssignment] 
	@assignmentID int = -1,
	@courseID nvarchar(30),
	@atitle nvarchar(50),
	@afilelocation nvarchar(200),
	@duedate datetime,
	@assigneddate datetime
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @returnvalue INT
	IF(@assignmentID = -1)
	BEGIN
		INSERT INTO ASSIGNMENT(CourseID, ATitle, AFileLocation, DueDate, AssignedDate) 
		VALUES(@courseID,@atitle,@afilelocation,@duedate,@assigneddate)
		SELECT @returnvalue = SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
		UPDATE ASSIGNMENT SET
		CourseID = @courseID,
		ATitle = @atitle,
		AFileLocation = @afilelocation
		WHERE AssignmentID = @assignmentID
		SELECT @returnvalue = @assignmentID
	END
	IF(@@ERROR <> 0)
	BEGIN
		RETURN -1
	END
	ELSE
	BEGIN
		RETURN @returnvalue
	END
	
END
GO



CREATE PROCEDURE [dbo].[spInsertUpdateAssignmentSubmissionGrade] 
	@sid nvarchar(50),
	@assignmentID int,
	@submissiondate  datetime,
	@filelocation nvarchar(200),
	@grade float = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	IF NOT EXISTS(select * from Submission where SID = @sid and AssignmentID = @assignmentID)
	BEGIN
		INSERT INTO Submission(SID,AssignmentID, SubmissionDate, FileLocation, Grade) 
		VALUES (@sid,@assignmentID,@submissiondate,@filelocation,@grade)
	END
	ELSE
	BEGIN
		UPDATE Submission
		SET Grade = @grade 
		WHERE SID = @sid and AssignmentID = @assignmentID
	END
	IF(@@ERROR != 0)
	BEGIN
		RETURN -1
	END
END
GO



CREATE PROCEDURE [dbo].[spInsertUpdateLectureNote] 
	@lectureID int = -1,
	@courseID nvarchar(50),
	@ltitle nvarchar(50),
	@lfilelocation nvarchar(200)
	
AS
	DECLARE @returnvalue int
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	IF(@lectureID = -1)
	BEGIN
		INSERT INTO LectureNote(CourseID, LTitle, LFileLocation) 
		VALUES (@courseID,@ltitle,@lfilelocation)
		SELECT @returnvalue = SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
		UPDATE LECTURENOTE SET
		CourseID = @courseID,
		LTitle = @ltitle,
		LFileLocation = @lfilelocation
		WHERE LectureID = @lectureID
		
	END
	SELECT @returnvalue = @lectureID
	IF(@@ERROR <> 0)
	BEGIN
		RETURN -1
	END
	ELSE
	BEGIN
		RETURN @returnvalue
	END
	END

GO


