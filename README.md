# SchoolManagementSystem

SchoolManagementProject:
1.Set up N-Tier Architecture.
⦁	Seperate Layer for each project. e.g.
1.	SMS.Api (Entry Point, Controllers)
2.	SMS.Application (DTO's, Mappings, Services)
3.	SMS.Domian (Models, Interfaces)
4.	SMS.Infrastructure (Data, Migrations, Repositories)
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
Http Request (Admin) ====> PRESENTATION LAYER ( Controller recieves req and calls AdminService ) ====> SERVICE LAYER (Service layer calls GetAllAsync on the IAdminRepository) ====>  REPOSITORY LAYER (The AdminRepository fetches all admins record from Database and return to AdminService) ====> SERVICE LAYER AGAIN (AdminService transform the data (e.g. maps <Admin> to <AdminDto>)) and return to Controller ====> PERSENTATION LAYER AGAIN <Result in JSON>.

ARCHITECTURE USED:
1.	Repository Pattren.
2.	N-Tier Architecture.
3.	SOLID Priniciples
4.	Class Libraries.
5.	Seperation Of Concern.
 
