DROP TABLE [dbo].[Question];
DROP TABLE [dbo].[Quiz];
DROP TABLE [dbo].[Exam];
DROP TABLE [dbo].[Result];
DROP TABLE [dbo].[ResetPassword];
DROP TABLE [dbo].[Meeting];
DROP TABLE [dbo].[Attendance];
DROP TABLE [dbo].[Submission];
DROP TABLE [dbo].[Assessment];
DROP TABLE [dbo].[Comment];
DROP TABLE [dbo].[Forum];
DROP TABLE [dbo].[TimetableSubject];
DROP TABLE [dbo].[Teacher_Classroom];
DROP TABLE [dbo].[Subject_Classroom];
DROP TABLE [dbo].[Subject];
DROP TABLE [dbo].[Teacher];
DROP TABLE [dbo].[Student];
DROP TABLE [dbo].[Classroom];
DROP TABLE [dbo].[Timetable];
DROP TABLE [dbo].[Parent];
DROP TABLE [dbo].[Announcement];

CREATE TABLE [dbo].[Announcement] (
    [AnnouncementGUID]  UNIQUEIDENTIFIER NOT NULL,
    [AnnouncementTitle] NVARCHAR (500)   NOT NULL,
    [AnnouncementDesc]  NVARCHAR (1000)   NOT NULL,
    [Status]            NVARCHAR (10)    NOT NULL,
    [CreateDate]        DATETIME         NOT NULL,
    [LastUpdateDate]    DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([AnnouncementGUID] ASC)
);

CREATE TABLE [dbo].[Parent] (
    [ParentGUID]   UNIQUEIDENTIFIER NOT NULL,
    [ParentUserID] NVARCHAR (10)    NOT NULL,
    [Password]     NVARCHAR (100)   NOT NULL,
    [FullName]     NVARCHAR (50)    NOT NULL,
    [Status]       NVARCHAR (10)    NOT NULL,
    [CreateDate ]  DATETIME         NOT NULL,
    [ICNo]         CHAR (12)        NOT NULL,
    PRIMARY KEY CLUSTERED ([ParentGUID] ASC)
);

CREATE TABLE [dbo].[Timetable] (
    [TimetableGUID]  UNIQUEIDENTIFIER NOT NULL,
    [CreateDate ]    DATETIME         NOT NULL,
    [LastUpdateDate] DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([TimetableGUID] ASC)
);

CREATE TABLE [dbo].[Classroom] (
    [ClassroomGUID]  UNIQUEIDENTIFIER NOT NULL,
    [Class]          NVARCHAR (20)    NOT NULL,
    [CreateDate]     DATETIME         NOT NULL,
    [LastUpdateDate] DATETIME         NOT NULL,
    [TimetableGUID]  UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([ClassroomGUID] ASC),
    CONSTRAINT [FK_Classroom_Timetable] FOREIGN KEY ([TimetableGUID]) REFERENCES [dbo].[Timetable] ([TimetableGUID])
);


CREATE TABLE [dbo].[Student] (
    [StudentGUID]    UNIQUEIDENTIFIER NOT NULL,
    [StudentUserID]  NVARCHAR (10)    NOT NULL,
    [Password]       NVARCHAR (100)   NOT NULL,
    [FullName]       NVARCHAR (50)    NOT NULL,
    [ProfilePic]     NVARCHAR (100)   NULL,
    [ICNo]           CHAR (12)        NOT NULL,
    [DateOfBirth]    DATE             NOT NULL,
    [Gender]         NVARCHAR (6)     NOT NULL,
    [PhoneNo]        CHAR (10)        NULL,
    [Email]          NVARCHAR (100)    NULL,
    [Address]        NVARCHAR (100)    NULL,
    [JoinDate]       DATE             NULL,
    [Status]         NVARCHAR (10)    NOT NULL,
    [CreateDate]     DATETIME         NOT NULL,
    [LastUpdateDate] DATETIME         NOT NULL,
    [ParentGUID]     UNIQUEIDENTIFIER NULL,
    [ClassroomGUID]  UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([StudentGUID] ASC),
    CONSTRAINT [FK_Student_Parent] FOREIGN KEY ([ParentGUID]) REFERENCES [dbo].[Parent] ([ParentGUID]),
    CONSTRAINT [FK_Student_Classroom] FOREIGN KEY ([ClassroomGUID]) REFERENCES [dbo].[Classroom] ([ClassroomGUID])
);

CREATE TABLE [dbo].[Teacher] (
    [TeacherGUID]    UNIQUEIDENTIFIER NOT NULL,
    [TeacherUserID]  NVARCHAR (10)    NOT NULL,
    [Password]       NVARCHAR (100)  NOT NULL,
    [FullName]       NVARCHAR (50)   NOT NULL,
    [ProfilePic]     NVARCHAR (100)  NULL,
    [ICNo]           CHAR (12)        NOT NULL,
    [DateOfBirth]    DATE             NOT NULL,
    [Gender]         NVARCHAR (6)     NOT NULL,
    [PhoneNo]        CHAR (11)        NULL,
    [Email]          NVARCHAR (50)   NULL,
    [Address]        NVARCHAR (50)  NULL,
    [JoinDate]       DATE             NULL,
    [Status]         NVARCHAR (10)    NOT NULL,
    [CreateDate]     DATETIME         NOT NULL,
    [LastUpdateDate] DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([TeacherGUID] ASC)
);

CREATE TABLE [dbo].[Subject] (
    [SubjectGUID]    UNIQUEIDENTIFIER NOT NULL,
    [SubjectName]    NVARCHAR (100)   NOT NULL,
    [Status]         NVARCHAR (10)    NOT NULL,
    [CreateDate]     DATETIME         NOT NULL,
    [LastUpdateDate] DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([SubjectGUID] ASC)
);

CREATE TABLE [dbo].[Subject_Classroom] (
    [SubjectGUID]   UNIQUEIDENTIFIER NOT NULL,
    [ClassroomGUID] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [FK_Subject] FOREIGN KEY ([SubjectGUID]) REFERENCES [dbo].[Subject] ([SubjectGUID]),
    CONSTRAINT [FK_Classroom_Subject] FOREIGN KEY ([ClassroomGUID]) REFERENCES [dbo].[Classroom] ([ClassroomGUID])
);

CREATE TABLE [dbo].[Teacher_Classroom] (
    [TeacherGUID]   UNIQUEIDENTIFIER NOT NULL,
    [ClassroomGUID] UNIQUEIDENTIFIER NOT NULL,
    [SubjectTeach]  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [FK_Teacher_Classroom] FOREIGN KEY ([TeacherGUID]) REFERENCES [dbo].[Teacher] ([TeacherGUID]),
    CONSTRAINT [FK_Teacher_Subject] FOREIGN KEY ([SubjectTeach]) REFERENCES [dbo].[Subject] ([SubjectGUID]),
    CONSTRAINT [FK_Classroom_Teacher] FOREIGN KEY ([ClassroomGUID]) REFERENCES [dbo].[Classroom] ([ClassroomGUID])
);

CREATE TABLE [dbo].[TimetableSubject] (
    [TimetableSubjectGUID] UNIQUEIDENTIFIER NOT NULL,
    [TimetableGUID]        UNIQUEIDENTIFIER NOT NULL,
    [Day]                  INT              NOT NULL,
    [TimeSlot1]            NVARCHAR (36)    NULL,
    [TimeSlot2]            NVARCHAR (36)    NULL,
    [TimeSlot3]            NVARCHAR (36)    NULL,
    [TimeSlot4]            NVARCHAR (36)    NULL,
    [TimeSlot5]            NVARCHAR (36)    NULL,
    [TimeSlot6]            NVARCHAR (36)    NULL,
    [TimeSlot7]            NVARCHAR (36)    NULL,
    [TimeSlot8]            NVARCHAR (36)    NULL,
    [TimeSlot9]            NVARCHAR (36)    NULL,
    [TimeSlot10]           NVARCHAR (36)    NULL,
    PRIMARY KEY CLUSTERED ([TimetableSubjectGUID] ASC),
    CONSTRAINT [FK_TimetableSubject_Timetable] FOREIGN KEY ([TimetableGUID]) REFERENCES [dbo].[Timetable] ([TimetableGUID])
);


CREATE TABLE [dbo].[Forum] (
    [ForumGUID]      UNIQUEIDENTIFIER NOT NULL,
    [ClassroomGUID]  UNIQUEIDENTIFIER NOT NULL,
    [AuthorGUID]     UNIQUEIDENTIFIER NOT NULL,
    [CreateDate]     DATETIME         NOT NULL,
    [LastUpdateDate] DATETIME         NOT NULL,
    [Content]        NVARCHAR (1000)  NOT NULL,
    [Title]          NVARCHAR (500)   NOT NULL,
    PRIMARY KEY CLUSTERED ([ForumGUID] ASC),
    CONSTRAINT [FK_Forum_Classroom] FOREIGN KEY ([ClassroomGUID]) REFERENCES [dbo].[Classroom] ([ClassroomGUID])
);

CREATE TABLE [dbo].[Comment] (
    [CommentGUID] UNIQUEIDENTIFIER NOT NULL,
    [ForumGUID]   UNIQUEIDENTIFIER NOT NULL,
    [CreateDate]  DATETIME         NOT NULL,
    [Content]     NVARCHAR (1000)  NOT NULL,
    [CommentBy]   NVARCHAR (30)    NOT NULL,
    PRIMARY KEY CLUSTERED ([CommentGUID] ASC),
    CONSTRAINT [FK_Comment_Forum] FOREIGN KEY ([ForumGUID]) REFERENCES [dbo].[Forum] ([ForumGUID])
);


CREATE TABLE [dbo].[Assessment] (
    [AssessmentGUID] UNIQUEIDENTIFIER NOT NULL,
    [ClassroomGUID]  UNIQUEIDENTIFIER NOT NULL,
    [TeacherGUID]    UNIQUEIDENTIFIER NOT NULL,
    [Title]          NVARCHAR (500)    NOT NULL,
    [Description]    NVARCHAR (1000)   NOT NULL,
    [CreateDate]     DATETIME         NOT NULL,
    [LastUpdateDate] DATETIME         NOT NULL,
    [DueDate]        DATETIME         NULL,
    [File]           NVARCHAR (100)   NULL,
    PRIMARY KEY CLUSTERED ([AssessmentGUID] ASC),
    CONSTRAINT [FK_Assessment_Classroom] FOREIGN KEY ([ClassroomGUID]) REFERENCES [dbo].[Classroom] ([ClassroomGUID])
);

CREATE TABLE [dbo].[Submission] (
    [SubmissionGUID] UNIQUEIDENTIFIER NOT NULL,
    [AssessmentGUID] UNIQUEIDENTIFIER NOT NULL,
    [StudentGUID]    UNIQUEIDENTIFIER NOT NULL,
    [Status]         NVARCHAR (10)    NOT NULL,
    [File]           NVARCHAR (100)   NULL,
    [LastUpdateDate] DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([SubmissionGUID] ASC),
    CONSTRAINT [FK_Submission_Assessment] FOREIGN KEY ([AssessmentGUID]) REFERENCES [dbo].[Assessment] ([AssessmentGUID])
);


CREATE TABLE [dbo].[Attendance] (
    [AttendanceGUID] UNIQUEIDENTIFIER NOT NULL,
    [StudentGUID]    UNIQUEIDENTIFIER NOT NULL,
    [StartTime]      DATETIME         NOT NULL,
    [EndTime]        DATETIME         NOT NULL,
    [TotalTime]      TIME (7)         NOT NULL,
    [Remark]         NVARCHAR (50)    NULL,
    [CreateDate]     DATETIME         NOT NULL,
    [LastUpdateDate] DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([AttendanceGUID] ASC),
    CONSTRAINT [FK_Student_Attendance] FOREIGN KEY ([StudentGUID]) REFERENCES [dbo].[Student] ([StudentGUID])
);

CREATE TABLE [dbo].[Meeting] (
    [MeetingGUID]     UNIQUEIDENTIFIER NOT NULL,
    [TeacherGUID]     UNIQUEIDENTIFIER NOT NULL,
    [ClassroomGUID]   UNIQUEIDENTIFIER NOT NULL,
    [MeetingTopic]    NVARCHAR (900)   NOT NULL,
    [MeetingRoomID]   NVARCHAR (100)   NOT NULL,
    [MeetingRoomPass] NVARCHAR (100)   NOT NULL,
    [MeetingTime]     DATETIME         NOT NULL,
    [Duration]        INT              NOT NULL,
    [Status]          NVARCHAR (50)    NOT NULL,
    [CreateDate]      DATETIME         NOT NULL,
    [LastUpdateDate]  DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([MeetingGUID] ASC),
    CONSTRAINT [FK_Meeting_Teacher] FOREIGN KEY ([TeacherGUID]) REFERENCES [dbo].[Teacher] ([TeacherGUID]),
    CONSTRAINT [FK_Meeting_Classroom] FOREIGN KEY ([ClassroomGUID]) REFERENCES [dbo].[Classroom] ([ClassroomGUID])
);


CREATE TABLE [dbo].[ResetPassword] (
    [ResetPasswordGUID] UNIQUEIDENTIFIER NOT NULL,
    [TeacherGUID]       UNIQUEIDENTIFIER NULL,
    [ParentGUID]        UNIQUEIDENTIFIER NULL,
    [StudentGUID]       UNIQUEIDENTIFIER NULL,
    [Status]            NVARCHAR (10)    NOT NULL,
    PRIMARY KEY CLUSTERED ([ResetPasswordGUID] ASC),
    CONSTRAINT [FK_ResetPassword_Teacher] FOREIGN KEY ([TeacherGUID]) REFERENCES [dbo].[Teacher] ([TeacherGUID]),
    CONSTRAINT [FK_ResetPassword_Parent] FOREIGN KEY ([TeacherGUID]) REFERENCES [dbo].[Parent] ([ParentGUID]),
    CONSTRAINT [FK_ResetPassword_Student] FOREIGN KEY ([TeacherGUID]) REFERENCES [dbo].[Student] ([StudentGUID])
);

CREATE TABLE [dbo].[Result] (
    [ResultGUID]     UNIQUEIDENTIFIER NOT NULL,
    [AverageMark]    DECIMAL (18)     NOT NULL,
    [GPA]            DECIMAL (18)     NOT NULL,
    [CGPA]           DECIMAL (18)     NOT NULL,
    [PlaceClass]     DECIMAL (3)      NOT NULL,
    [PlaceForm]      DECIMAL (5)      NOT NULL,
    [CreateDate]     DATETIME         NOT NULL,
    [LastUpdateDate] DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([ResultGUID] ASC)
);

CREATE TABLE [dbo].[Exam] (
    [ExamGUID]       UNIQUEIDENTIFIER NOT NULL,
    [SubjectGUID]    UNIQUEIDENTIFIER NOT NULL,
    [StudentGUID]    UNIQUEIDENTIFIER NOT NULL,
    [ResultGUID]     UNIQUEIDENTIFIER NOT NULL,
    [ExamName]       NVARCHAR (100)   NOT NULL,
    [Mark]           DECIMAL (18)     NOT NULL,
    [Grade]          CHAR (5)         NOT NULL,
    [DateExam]       DATETIME         NOT NULL,
    [ExamSemester]   NVARCHAR (100)   NOT NULL,
    [CreateDate]     DATETIME         NOT NULL,
    [LastUpdateDate] DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([ExamGUID] ASC),
    CONSTRAINT [FK_Subject_Exam] FOREIGN KEY ([SubjectGUID]) REFERENCES [dbo].[Subject] ([SubjectGUID]),
    CONSTRAINT [FK_Student_Exam] FOREIGN KEY ([StudentGUID]) REFERENCES [dbo].[Student] ([StudentGUID]),
    CONSTRAINT [FK_Result_Exam] FOREIGN KEY ([ResultGUID]) REFERENCES [dbo].[Result] ([ResultGUID])
);

CREATE TABLE [dbo].[Quiz] (
    [QuizGUID]       UNIQUEIDENTIFIER NOT NULL,
    [CreateDate]     DATETIME         NOT NULL,
    [LastUpdateDate] DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([QuizGUID] ASC)
);

CREATE TABLE [dbo].[Question] (
    [QuestionGUID]   UNIQUEIDENTIFIER NOT NULL,
    [QuizGUID]       UNIQUEIDENTIFIER NOT NULL,
    [Question]       NVARCHAR (100)   NOT NULL,
    [CorrectAnswer]  NVARCHAR (100)   NOT NULL,
    [Option1]        NVARCHAR (100)   NOT NULL,
    [Option2]        NVARCHAR (100)   NOT NULL,
    [Option3]        NVARCHAR (100)   NULL,
    [Option4]        NVARCHAR (100)   NULL,
    [CreateDate]     DATETIME         NOT NULL,
    [LastUpdateDate] DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([QuestionGUID] ASC),
    CONSTRAINT [FK_Question_Quiz] FOREIGN KEY ([QuizGUID]) REFERENCES [dbo].[Quiz] ([QuizGUID])
);
