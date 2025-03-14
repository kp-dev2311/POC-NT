# POC For User Activity Tracking

SchoolManagementProject:
1.Set up N-Tier Architecture.
⦁	Seperate Layer for each project. e.g.
1.	POC.Api (Entry Point, Controllers)
2.	POC.Application (DTO's, Mappings, Services)
2.	POC.ConsoleUI (Input / Output User Interface)
3.	POC.Domian (Models, Interfaces)
4.	POC.Infrastructure (Data, Migrations, Repositories)
So we set each layers as:
1.	Persentation Layer: Handle user intraction (e.g. Web App or Wen Page)
2.	Service Layer: Contains Business Logic (e.g. Rules for processing Data)
3.	Repository Layer: Handles Data Access (e.g. Fetching Data from Db)
4.	Data Layer: Actual Db or Storage.
KET CONCEPTS :
⦁	IRepository Interface : Define Methods the repository must impliment.
            (e.g. GetAllAsync, GetByIdAsync, CreateAsync, DeleteAsync, UpdateAsync It is like a contract)
⦁	Repository Class : This impliment the IRepository interface and contain actual code to intract with Database.
SERVICE LAYER : It use Repository Layer to fetch to fetch or save data to apply rules and transformation to data.
KEY CONCEPTS : 
⦁	IService Interface : This define methods that Service must impliment (e.g. GetAllAdminAsync, GetAdminByIdAsync, etc)
⦁	Creating Service for each Entity : (e.g, AdminService, TeacherService, StudentService)

WORKFLOW : 
Http Request (User) ====> PRESENTATION LAYER ( Controller recieves req and calls UserService ) 
====> SERVICE LAYER (Service layer calls GetAllAsync on the IUserRepository) 
====> REPOSITORY LAYER (The UsersRepository fetches all Users record from Database and return to UsersService) 
====> SERVICE LAYER AGAIN (UserService transform the data (e.g. maps <Users> to <UsersDto>)) and return to Controller 
====> PERSENTATION LAYER AGAIN <Result in JSON>.

ARCHITECTURE USED:
1.	Repository Pattren.
2.	N-Tier Architecture.
3.	SOLID Priniciples
4.	Class Libraries.
5.	Seperation Of Concern.
 
