using System;
using System.Xml;
using System.Linq;
using System.Data.Services.Client;
using System.Net;
using System.Xml.Linq;
using Terrasoft.Configuration;

namespace ODataBpmOnlineCRUD
{
    class CRUDOperations
    {
        private static readonly XNamespace ds = "http://schemas.microsoft.com/ado/2007/08/dataservices";
        private static readonly XNamespace dsmd = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata";
        private static readonly XNamespace atom = "http://www.w3.org/2005/Atom";
        private static readonly Uri serverUri = new Uri("http://192.168.225.200:84/0/ServiceModel/EntityDataService.svc/");
        public static void UpdateEntity(Guid accountId, string name, int completness, DateTime createdOn, Guid typeId, string requestMethod)
        {

            var content = new XElement(dsmd + "properties", new XElement(ds + "Name", name), new XElement(ds + "Completeness", completness), new XElement(ds + "CreatedOn", createdOn), new XElement(ds + "TypeId", typeId));
            var entry = new XElement(atom + "entry",
                        new XElement(atom + "content",
                        new XAttribute("type", "application/xml"), content));
            LoginClass.CreateHttpRequest(out HttpWebRequest request, "AccountCollection", accountId, requestMethod);
            using (var writer = XmlWriter.Create(request.GetRequestStream()))
            {
                entry.WriteTo(writer);
            }
            using (WebResponse response = request.GetResponse())
            {

            }
        }
        public static void CreateEntity(string name, int completness, DateTime createdOn, Guid typeId)
        {
            var account = new Account()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Completeness = completness,
                CreatedOn = createdOn,
                TypeId = typeId
            };
            var context = new BPMonline(serverUri);
            context.SendingRequest += new EventHandler<SendingRequestEventArgs>(LoginClass.OnSendingRequestCookie);
            context.AddToAccountCollection(account);
            DataServiceResponse responces = context.SaveChanges(SaveChangesOptions.Batch);
        }

        public static void ReadEntity(string probableName)
        {
            var context = new BPMonline(serverUri);
            context.SendingRequest += new EventHandler<SendingRequestEventArgs>(LoginClass.OnSendingRequestCookie);

            if (context.AccountCollection.Where(c => c.Name.Contains(probableName)).Count() < 1)
                throw new Exception(Localize.NotExistError);
            AccountType accountType = new AccountType();
            var allAccounts = context.AccountCollection.Where(c => c.Name.Contains(probableName));
            foreach (Account account in allAccounts)
            {
                if (account.TypeId != Guid.Empty && account.TypeId != null)
                    accountType = context.AccountTypeCollection.Where((c => c.Id == account.TypeId)).First();
                Console.WriteLine(String.Format(Localize.AccountOutput, account.Name, accountType.Name, account.Zip, account.Completeness, account.CreatedOn));
            }
        }
        public static void DeleteEntity(Guid accountId)
        {
            var context = new BPMonline(serverUri);
            context.SendingRequest += new EventHandler<SendingRequestEventArgs>(LoginClass.OnSendingRequestCookie);
            if (context.AccountCollection.Where(c => c.Id == accountId).Count() == 0)
                throw new Exception(Localize.NotExistError);
            var deleteAccount = context.AccountCollection.Where(c => c.Id == accountId).First();
            context.DeleteObject(deleteAccount);
            var responces = context.SaveChanges(SaveChangesOptions.Batch);
        }
    }
}
