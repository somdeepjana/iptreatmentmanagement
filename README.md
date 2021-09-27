
# Project Name: International Patient Management
This is a two part application with `ASP.NET Core MVC` and `ASP.NET Core WebApi`.
MVC application calls the webapi for most of the business operations. MVC is mainly
used to server the dynamic front end to the user and from the user browser client 
`ajax` calls are made to the webapi for data retrieval.

# Group Members:- 
| No. | Name             | GitHub                                          |
|-----|------------------|-------------------------------------------------|
| 1.  | Somdeep Jana     | [somdeepjana](https://github.com/somdeepjana)   |
| 2.  | Koustav Mishra   | [KKMishra1999](https://github.com/KKMishra1999) |
| 3.  | Deepjyoti Basu   | [Sherlock2603](https://github.com/Sherlock2603) |
| 4.  | Rahul Chatterjee | [Rahul2698](https://github.com/Rahul2698)       |
	
# System Requierment:-
>Framework requierment: `ASP.NET 5`
	
# Recommended tools:-
>Visual studio 2019

# Running procedure using VS2019:-

## STEPS:-
1. Start .sln solution file with VS2019.
2. Set multistartup project and select API & MVC web.
	>Reason: the MVC application depends on web api.
3. Click on run.

## RUN:-
Default admin credentials are given below. you can change these in the 
`appsettings.json` of the webapi, you will find the file at 
`{BASE_DIRECTORY}/IPTreatmentManagement.Api/appsettings.json`
		
```json
"AdminCredentials": {
	"UserName": "admin",
	"Password": "Pas$w0rd"
}
```
			
## NOTES:-
1. The MVC application depends on the web api so it needs the running api's 
url endpoint. API endpoint URL can be provided to the MVC application through 
`appsettings.json`. look for the below line in 
`{BASE_DIRECTORY}/IPTreatmentManagement.Web/appsettings.json`.
		
```json
"IPTreatmentManagement": {
	"Api": {
		"BaseUrl": "https://localhost:44345"
	}
}
```
			
2. MVC application depends on some js libraries which are not provided in the 
shipping package for smaller distribution size. By default these packages will be 
restored on build if you use the vs2019 build option. we are using libman for managing 
client side packages. you can find the list of client side packages at 
`{BASE_DIRECTORY}/IPTreatmentManagement.Web/libman.json`. The packages are 
installed at `{BASE_DIRECTORY}/IPTreatmentManagement.Web/wwwroot/lib`.
`bootstrap`, `JQuery`, `jquery-validation` and `jquery-validation-unobtrusive` packages 
are provided with the deployment packages.
