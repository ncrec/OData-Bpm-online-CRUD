using System;
using System.Resources;
using System.Xml.Linq;

namespace ODataBpmOnlineCRUD
{
    class Program
    {
        private static void Create(string entityName)
        {
            string nameParseResult = TryParseProperty.TryParseStringProperty(Localize.PropertyName, entityName);
            int completenessParseResult = TryParseProperty.TryParseIntProperty(Localize.PropertyCompleteness, entityName);
            DateTime createdOnParseResult = TryParseProperty.TryParseDateTimeProperty(Localize.PropertyCreatedOn, entityName);
            Guid typeIdParseResult = TryParseProperty.TryParseGuidProperty(Localize.PropertyTypeId, entityName);
            Console.WriteLine(Localize.Creating + "...");
            try
            {
                CRUDOperations.CreateEntity(nameParseResult, completenessParseResult, createdOnParseResult, typeIdParseResult);
                Console.WriteLine(String.Format(Localize.Done, entityName, Localize.Created));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void Delete(string entityName)
        {

            Guid typeIdParseResult = TryParseProperty.TryParseGuidProperty(Localize.PropertyTypeId, entityName);
            Console.WriteLine(Localize.Deleting + "...");
            try
            {
                CRUDOperations.DeleteEntity(typeIdParseResult);
                Console.WriteLine(String.Format(Localize.Done, entityName, Localize.Deleted));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void Update(string entityName)
        {
            Guid accountIdParseResult = TryParseProperty.TryParseGuidProperty(Localize.PropertyId, entityName);
            string nameParseResult = TryParseProperty.TryParseStringProperty(Localize.PropertyName, entityName);
            int completenessParseResult = TryParseProperty.TryParseIntProperty(Localize.PropertyCompleteness, entityName);
            DateTime createdOnParseResult = TryParseProperty.TryParseDateTimeProperty(Localize.PropertyCreatedOn, entityName);
            Guid typeIdParseResult = TryParseProperty.TryParseGuidProperty(Localize.PropertyTypeId, entityName);
            Console.WriteLine(Localize.Updating + "...");
            try
            {
                CRUDOperations.UpdateEntity(accountIdParseResult, nameParseResult, completenessParseResult, createdOnParseResult, typeIdParseResult, ReqestMethod.Patch);
                Console.WriteLine(String.Format(Localize.Done, entityName, Localize.Updated));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void Read(string entityName)
        {

            string nameParseResult = TryParseProperty.TryParseStringProperty(Localize.PropertyName, entityName);
            Console.WriteLine(Localize.Reading + "...");
            try
            {
                CRUDOperations.ReadEntity(nameParseResult);
                Console.WriteLine(String.Format(Localize.Done, entityName, Localize.Read));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private static void ExecuteCommand(string Command)
        {
            switch (Command)
            {
                case "1":
                    Console.WriteLine(String.Format(Localize.SelectedCommand, Localize.Create));
                    Create(Localize.Account);
                    break;
                case "2":
                    Console.WriteLine(String.Format(Localize.SelectedCommand, Localize.Read));
                    Read(Localize.Account);
                    break;
                case "3":
                    Console.WriteLine(String.Format(Localize.SelectedCommand, Localize.Update));
                    Update(Localize.Account);
                    break;
                case "4":
                    Console.WriteLine(String.Format(Localize.SelectedCommand, Localize.Delete));
                    Delete(Localize.Account);
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine(Localize.WrongCommand);
                    break;
            }
        }

        static void Main()
        {
            if (!LoginClass.TryLogin("Supervisor", "Supervisor"))
            {
                Console.WriteLine(Localize.AuthFailed);
                return;
            }
            Console.WriteLine(Localize.AuthPassed);
            string Command;
            while (true)
            {
                Console.WriteLine(Localize.ChooseCommand);
                Command = Console.ReadLine();
                ExecuteCommand(Command);
            }
        }
    }

}
