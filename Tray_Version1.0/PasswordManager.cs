using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tray_Version1._0
{
    class PasswordManager
    {
        public static bool check(string name, string password)
        {
            StreamReader reader = new StreamReader("Passwords.txt");
                        
            ulong hashedPassword = getHash(password);            
            string buffer = "";

            while((buffer = reader.ReadLine()) != null)
            {
                string[] args = buffer.Split(' ');
                if (args[0] == name && Convert.ToUInt64(args[1]) == hashedPassword)
                {
                    reader.Close();
                    return true;
                }
            }            

            reader.Close();
            return false;
        }

        private static ulong getHash(string s)
        {
            const ulong prime = 2017;
            ulong lastPower = 1;
            ulong hash = 0;

            for(int i = 0; i < s.Length; ++i)
            {
                hash += s[i] * lastPower;
                lastPower *= prime;
            }

            return hash;
        }

        public static void GeneratePasswords()
        {
            StreamWriter writer = new StreamWriter("Passwords.txt");

            writer.WriteLine("Bohdan " + getHash("1488"));
            writer.WriteLine("ADMIN " + getHash("PASSWORD"));
            writer.WriteLine("Oleg " + getHash("Kir"));
            writer.WriteLine("User1 " + getHash("Aa123456"));
            writer.WriteLine("Guest119a " + getHash("Pa$$word"));
            writer.WriteLine("BohdanPastuschak " + getHash("bo23da1pa98"));

            string newAdmin = "";
            string newPassword = "";
            int k;
            Random random = new Random();
            for (int i = 0; i < 100; ++i)
            {
                newAdmin = "";
                k = random.Next() % 10 + 1;
                for (int j = 0; j < k; ++j)
                {
                    newAdmin += (char)('0' + random.Next() % ('z' - '0'));
                }

                newPassword = "";
                k = random.Next() % 15;
                for (int j = 0; j < k; ++j)
                {
                    newPassword += (char)('0' + random.Next() % ('z' - '0'));
                }

                writer.WriteLine(newAdmin + ' ' + getHash(newPassword));
            }

            writer.Close();
        }
    }
}
