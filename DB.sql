-- ============================
-- DEER Hub Database Schema
-- ============================

-- Drop existing tables if needed (for resets)
DROP TABLE IF EXISTS LeaveRequests, Announcements, Employees, Users, Departments, LeaveTypes, LeaveStatuses;

-- 1. Users Table
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) UNIQUE NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(256) NOT NULL,
    Role NVARCHAR(20) CHECK (Role IN ('Employee', 'Manager', 'Admin')),
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    ModifiedAt DATETIME
);

-- 2. Departments Table
CREATE TABLE Departments (
    DepartmentID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(255)
);

-- 3. Employees Table
CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    FullName NVARCHAR(100) NOT NULL,
    DepartmentID INT NOT NULL FOREIGN KEY REFERENCES Departments(DepartmentID),
    DateOfJoining DATE NOT NULL,
    PhoneNumber NVARCHAR(15),
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    ModifiedAt DATETIME
);

-- 4. LeaveTypes Table
CREATE TABLE LeaveTypes (
    LeaveTypeID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(255)
);

-- 5. LeaveStatuses Table
CREATE TABLE LeaveStatuses (
    StatusID INT PRIMARY KEY IDENTITY(1,1),
    StatusName NVARCHAR(50) NOT NULL UNIQUE
);

-- 6. LeaveRequests Table
CREATE TABLE LeaveRequests (
    LeaveID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeID INT FOREIGN KEY REFERENCES Employees(EmployeeID),
    LeaveTypeID INT FOREIGN KEY REFERENCES LeaveTypes(LeaveTypeID),
    StatusID INT FOREIGN KEY REFERENCES LeaveStatuses(StatusID),
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    Reason NVARCHAR(255),
    Priority NVARCHAR(20) CHECK (Priority IN ('Low', 'Normal', 'High', 'Urgent')) DEFAULT 'Normal',
    DocumentPath NVARCHAR(255), -- stores file path or file name
    RequestedAt DATETIME DEFAULT GETDATE(),
    ApprovedBy INT NULL FOREIGN KEY REFERENCES Employees(EmployeeID),
    IsDeleted BIT DEFAULT 0,
    ModifiedAt DATETIME
);

-- 7. Announcements Table
CREATE TABLE Announcements (
    AnnouncementID INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(100),
    Description NVARCHAR(500),
    PostedBy INT FOREIGN KEY REFERENCES Employees(EmployeeID),
    PostedAt DATETIME DEFAULT GETDATE(),
    IsVisible BIT DEFAULT 1
);


-- Sample Data Insertion 
-- ============================
-- DEER Hub Mock Data Insert
-- ============================

-- 1. Departments
INSERT INTO Departments (Name, Description)
VALUES 
('Human Resources', 'Handles employee relations and policies'),
('IT', 'Maintains company tech and infrastructure'),
('Sales', 'Drives revenue through customer acquisition');

-- 2. Leave Types
INSERT INTO LeaveTypes (Name, Description)
VALUES 
('Sick Leave', 'Leave taken when employee is unwell'),
('Casual Leave', 'Planned personal leave'),
('Maternity Leave', 'Leave for new mothers'),
('Paternity Leave', 'Leave for new fathers');

-- 3. Leave Statuses
INSERT INTO LeaveStatuses (StatusName)
VALUES 
('Pending'), ('Approved'), ('Rejected'), ('Cancelled');

-- 4. Users
INSERT INTO Users (Username, Email, PasswordHash, Role)
VALUES 
('jdoe', 'jdoe@deerhub.com', 'hashed_pwd_1', 'Employee'),
('asmith', 'asmith@deerhub.com', 'hashed_pwd_2', 'Manager'),
('admin1', 'admin@deerhub.com', 'hashed_pwd_3', 'Admin');

-- 5. Employees
INSERT INTO Employees (UserID, FullName, DepartmentID, DateOfJoining, PhoneNumber)
VALUES 
(1, 'John Doe', 1, '2023-01-10', '1234567890'),
(2, 'Alice Smith', 2, '2022-09-01', '9876543210'),
(3, 'System Admin', 2, '2021-06-15', '0000000000');

-- 6. Announcements
INSERT INTO Announcements (Title, Description, PostedBy)
VALUES 
('Holiday Notice', 'Company will be closed on 4th July.', 2),
('Policy Update', 'New WFH policy effective next month.', 3);

-- 7. Leave Requests
INSERT INTO LeaveRequests (EmployeeID, LeaveTypeID, StatusID, StartDate, EndDate, Reason, Priority, DocumentPath, ApprovedBy)
VALUES 
(1, 1, 1, '2024-07-10', '2024-07-12', 'Fever and weakness', 'High', 'medical_note.pdf', NULL),
(1, 5, 2, '2024-06-01', '2024-06-03', 'Family function', 'Normal', NULL, 2),
(2, 1, 3, '2024-05-15', '2024-05-16', 'Sick leave rejected', 'Urgent', NULL, 3);