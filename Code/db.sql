-- 创建数据库
CREATE DATABASE WorkflowManagerDB;
USE WorkflowManagerDB;

-- 创建员工表
CREATE TABLE Employees (
    EmployeeId INT AUTO_INCREMENT PRIMARY KEY, -- 员工ID，主键
    Name VARCHAR(50) NOT NULL,                 -- 员工姓名
    Position VARCHAR(50),                      -- 员工职位
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP -- 创建时间
);

-- 创建任务表
CREATE TABLE Tasks (
    TaskId INT AUTO_INCREMENT PRIMARY KEY,     -- 任务ID，主键
    Title VARCHAR(100) NOT NULL,               -- 任务名称
    Description TEXT,                          -- 任务描述
    Deadline DATETIME NOT NULL,                -- 任务截止时间
    Urgency INT NOT NULL CHECK (Urgency BETWEEN 1 AND 5),  -- 紧急度，范围1到5
    Type ENUM('Normal', 'Paid', 'Defect') NOT NULL,  -- 任务类型
    Status ENUM('Pending', 'InProgress', 'Review', 'Testing') NOT NULL DEFAULT 'Pending', -- 当前状态
    AssignedTo INT,                            -- 分配的员工ID
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP, -- 创建时间
    FOREIGN KEY (AssignedTo) REFERENCES Employees(EmployeeId) -- 关联员工表
);

-- 插入测试员工数据
INSERT INTO Employees (Name, Position) VALUES ('Eden', 'Developer');
INSERT INTO Employees (Name, Position) VALUES ('Alice', 'Tester');
INSERT INTO Employees (Name, Position) VALUES ('Bob', 'Project Manager');
INSERT INTO Employees (Name, Position) VALUES ('Charlie', 'Designer');
INSERT INTO Employees (Name, Position) VALUES ('David', 'QA Engineer');

-- 生成30条随机的任务记录插入到Tasks表中
INSERT INTO Tasks (Title, Description, Deadline, Urgency, Type, Status, AssignedTo) VALUES
('Fix UI glitch in dashboard', 'Resolve the issue causing the dashboard to misalign on certain resolutions.', '2024-10-20 10:00:00', 3, 'Defect', 'Pending', 1), -- Assigned to Eden
('Prepare user onboarding materials', 'Create content and slides for the user onboarding session.', '2024-10-21 09:00:00', 2, 'Normal', 'Pending', NULL), 
('Enhance search filtering', 'Add more filter options to the existing search functionality.', '2024-10-22 13:00:00', 4, 'Paid', 'InProgress', 2), -- Assigned to Alice
('Test payment module regression', 'Verify if the recent updates caused any regression in the payment module.', '2024-10-20 15:30:00', 5, 'Defect', 'Testing', 5), -- Assigned to David
('Refactor code for performance', 'Optimize the codebase to improve system performance.', '2024-10-23 11:00:00', 3, 'Normal', 'Pending', 3), -- Assigned to Bob
('Conduct user acceptance testing', 'Perform UAT for the new feature release.', '2024-10-24 17:00:00', 2, 'Paid', 'Review', 4), -- Assigned to Charlie
('Create wireframes for new module', 'Design wireframes for the new system module.', '2024-10-25 10:30:00', 3, 'Normal', 'Pending', NULL),
('Update deployment scripts', 'Revise deployment scripts to accommodate recent changes.', '2024-10-21 12:00:00', 4, 'Defect', 'Pending', 1), -- Assigned to Eden
('Fix broken links on website', 'Identify and correct all broken links across the website.', '2024-10-22 08:00:00', 2, 'Normal', 'InProgress', 2), -- Assigned to Alice
('Improve API documentation', 'Add examples and improve clarity in the API documentation.', '2024-10-23 14:00:00', 3, 'Paid', 'Pending', 3), -- Assigned to Bob
('Set up monitoring tools', 'Configure monitoring tools to track server health.', '2024-10-24 09:30:00', 5, 'Normal', 'Review', 5), -- Assigned to David
('Test email notification system', 'Ensure that email notifications are working as intended.', '2024-10-25 13:45:00', 4, 'Normal', 'Testing', 4), -- Assigned to Charlie
('Add accessibility features', 'Implement additional accessibility options for the platform.', '2024-10-20 11:00:00', 3, 'Normal', 'Pending', NULL),
('Resolve checkout process issues', 'Fix bugs affecting the checkout process.', '2024-10-21 18:00:00', 5, 'Defect', 'InProgress', 1), -- Assigned to Eden
('Upgrade server hardware', 'Plan and execute the server hardware upgrade.', '2024-10-22 16:30:00', 2, 'Paid', 'Pending', 5), -- Assigned to David
('Prepare presentation for stakeholders', 'Create slides and content for the upcoming stakeholder meeting.', '2024-10-23 10:00:00', 3, 'Normal', 'Review', 3), -- Assigned to Bob
('Optimize frontend performance', 'Reduce page load times by optimizing frontend code.', '2024-10-24 14:30:00', 4, 'Normal', 'InProgress', 2), -- Assigned to Alice
('Perform security audit', 'Conduct a security audit of the database and API endpoints.', '2024-10-25 11:15:00', 5, 'Defect', 'Testing', 4), -- Assigned to Charlie
('Document new API endpoints', 'Create documentation for the newly developed API endpoints.', '2024-10-20 13:00:00', 2, 'Normal', 'Pending', NULL),
('Fix issue with file uploads', 'Resolve the issue where certain file formats cannot be uploaded.', '2024-10-21 14:30:00', 4, 'Defect', 'InProgress', 2), -- Assigned to Alice
('Implement logging for audit trail', 'Add logging functionality to track user actions.', '2024-10-22 12:30:00', 3, 'Paid', 'Pending', 1), -- Assigned to Eden
('Upgrade software dependencies', 'Update software dependencies to the latest versions.', '2024-10-23 16:00:00', 2, 'Normal', 'Pending', 3), -- Assigned to Bob
('Review and merge pull requests', 'Review pending pull requests and merge them into the main branch.', '2024-10-24 10:45:00', 4, 'Normal', 'Review', 5), -- Assigned to David
('Optimize image loading times', 'Improve the process for loading images on the website.', '2024-10-25 15:00:00', 3, 'Defect', 'Testing', 4), -- Assigned to Charlie
('Configure caching', 'Set up caching to improve website performance.', '2024-10-20 17:30:00', 5, 'Paid', 'Pending', 1), -- Assigned to Eden
('Enhance mobile responsiveness', 'Improve the responsiveness of the platform on mobile devices.', '2024-10-21 08:30:00', 3, 'Normal', 'InProgress', 2), -- Assigned to Alice
('Set up continuous integration', 'Configure CI for automated builds and tests.', '2024-10-22 19:00:00', 4, 'Paid', 'Review', 3), -- Assigned to Bob
('Perform stress testing', 'Conduct stress testing on the server to assess load handling.', '2024-10-23 09:15:00', 5, 'Normal', 'Testing', 5), -- Assigned to David
('Refactor database schema', 'Make changes to the database schema to improve efficiency.', '2024-10-24 18:45:00', 4, 'Defect', 'InProgress', 4), -- Assigned to Charlie
('Fix layout issues on homepage', 'Correct the layout problems found on the homepage.', '2024-10-25 16:30:00', 2, 'Normal', 'Pending', NULL);

