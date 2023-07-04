using Dapper.Console;

AppConfiguration.Initialize();


//Tests

//Test.GettAll_Should_Return_6_Result();
//var id = Test.Create_Should_Assign_Identity_ToNewEntity();
//Test.Find_Should_Retrive_Existing_Entity(id);
//Test.Update_Should_Modify_Existing_Contact(id);
//Test.Delete_Should_Remove_A_Contact(id);

//Test.GetFullContact_Should_Retrive_FullContact_Info(1);
//Test.Create_Should_Insert_Addresses_And_Assign_Identity_ToNewEntity();

/*/--| Stored Procedure |--/*/
//Test.GetFullContact_SP_Should_Retrive_FullContact_Info(6);
//Test.Find_SP_Should_Retrive_Existing_Entity(6);
//Test.Create_SP_Should_Insert_Addresses_And_Assign_Identity_ToNewEntity();
//Test.Delete_SP_Should_Remove_A_Contact(11);

//Test.GetContactsById_Should_Return_List();
//Test.GetDynamicContactById_Should_Return_List();
Test.BulkInsert_Should_Save_Multiple_Contacts();
