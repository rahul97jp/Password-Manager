# Password-Manager
Is a HTTP REST API application built using Asp.Net and C# for password management.<br/>
I have used Entity Framework Core In-Memory Database to perform CRUD (Create, Read, Update and Delete) operations as we are building ASP.NET Web API.<br/>
5 main methods:<br/>
GetAllPasswords() => retrieves all account with encrypted password.<br/>
GetSinglePassword() => retrieves single specified account with decrypted password.<br/>
AddPassword() => Add account to database with password encrypted<br/>
UpdatePassword() => Updates specified password into the database.<br/>
DeletePassword() => Deletes specified password.<br/>
