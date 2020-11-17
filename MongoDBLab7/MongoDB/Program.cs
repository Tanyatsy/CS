using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

using System.Collections.Generic;
using System.Linq;

namespace MongoDB
{
    class Program
    {
        public static string randomWord = "";
        public static string encryptedString2 = "";
        static void Main(string[] args)
        {
            MongoClient dbClient = new MongoClient("mongodb://tanyatsy:tanyatsy1@clustercs-shard-00-00.2zn1s.mongodb.net:27017,clustercs-shard-00-01.2zn1s.mongodb.net:27017,clustercs-shard-00-02.2zn1s.mongodb.net:27017/ClusterCS?ssl=true&replicaSet=atlas-u6s2we-shard-0&authSource=admin&retryWrites=true&w=majority");
            var database = dbClient.GetDatabase("CS_labs");

            var collection = database.GetCollection<BsonDocument>("employees");

            var document = new BsonDocument();


            BsonDocument firstEmployee =
        new BsonDocument { { "JobPlace", "XOR" }, { "fullName", "Tatiana Tiguliova" }, { "email", "tanyatsy00@gmail.com" }, { "dateBirth", "6/08/2000" }, { "proffesion", "back-end dev." } }
             ;


            BsonDocument secondEmployee =
        new BsonDocument { { "JobPlace", "XOR" }, { "fullName", "Margareta Galaju" }, { "email", "margolu4shaya@gmail.com" }, { "dateBirth", "3/06/2001" }, { "proffesion", "front-end dev." } }
             ;


            BsonDocument thirdEmployee =
        new BsonDocument { { "JobPlace", "XOR" }, { "fullName", "Garciu Eugenia" }, { "email", "Jenikalu4shaya@gmail.com" }, { "dateBirth", "18/11/1999" }, { "proffesion", "back-end dev." } }
             ;


            database.DropCollection("employees");

            collection.InsertMany(new List<BsonDocument> { firstEmployee, secondEmployee, thirdEmployee });


            randomWord = GenerateWord();
            Console.WriteLine(document.ToString());
            var filter = Builders<BsonDocument>.Filter.Eq("JobPlace", "XOR");
            var result = collection.Find(filter).ToList();

            updateDBWithEncryptedValue("email", result, collection);
/*
            updateDBWithDecryptedValue("email", result, collection);*/

        }

        public static void updateDBWithEncryptedValue(string dbField, List<BsonDocument> filtersResult, IMongoCollection<BsonDocument> collection)
        {

            foreach (var doc in filtersResult)
            {


                var value = doc.GetValue(dbField);

                var thisFilter = Builders<BsonDocument>.Filter.Eq("_id", doc.GetValue("_id"));

                Console.WriteLine("Value: " + value.ToString());
                Console.WriteLine("\n**********\n");
                string encryptedString = Encrypt(value.ToString());
                encryptedString2 = Encrypt(encryptedString);
                Console.WriteLine("ENCRYPTED STRING: " + encryptedString2);
                Console.WriteLine("\n**********\n");

                var update = Builders<BsonDocument>.Update.Set(dbField, encryptedString2);

                collection.UpdateOne(thisFilter, update);
            }
        }

        public static void updateDBWithDecryptedValue(string dbField, List<BsonDocument> filtersResult, IMongoCollection<BsonDocument> collection)
        {

            foreach (var doc in filtersResult)
            {
                var value = doc.GetValue(dbField);

                var thisFilter = Builders<BsonDocument>.Filter.Eq("_id", doc.GetValue("_id"));

                string decryptedString = Decrypt(encryptedString2);
                string decryptedString2 = Decrypt(decryptedString);
                Console.WriteLine("DECRYPTED STRING: " + decryptedString2);
                Console.WriteLine("\n**********\n");
                var update = Builders<BsonDocument>.Update.Set(dbField, decryptedString2);

                collection.UpdateOne(thisFilter, update);
            }
        }

        public static string Encrypt(string text)
        {
            string EncryptionKey = "mySecureTTKey";
            byte[] clearBytes = Encoding.Unicode.GetBytes(text);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    text = Convert.ToBase64String(ms.ToArray());
                }
            }
            return text;
        }

        public static string Decrypt(string text)
        {
            string EncryptionKey = "mySecureTTKey";
            text = text.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(text);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    text = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return text;
        }

        public static string GenerateWord()
        {
            string word = "";
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();


            Random rand = new Random();


            for (int i = 1; i <= 1; i++)
            {


                for (int j = 1; j <= 7; j++)
                {

                    int letter_num = rand.Next(0, letters.Length - 1);


                    word += letters[letter_num];
                }


            }
            return word;
        }
    }

}





